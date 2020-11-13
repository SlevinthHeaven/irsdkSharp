using irsdkSharp.Serialization.Models.Session;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace irsdkSharp.ConsoleTest
{
    class Program
    {
        private static bool _hasConnected;
        private static int waitTime;
        private static Thread _looper;
        private static System.Timers.Timer timer;
        private static Serialization.IRacingSDK sdk;
        private static bool _IsConnected = false;
        private static irsdkSharp.Calculation.IRating _rating = new irsdkSharp.Calculation.IRating();

        private static double _TelemetryUpdateFrequency;
        /// <summary>
        /// Gets or sets the number of times the telemetry info is updated per second. The default and maximum is 60 times per second.
        /// </summary>
        public static double TelemetryUpdateFrequency
        {
            get { return _TelemetryUpdateFrequency; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("TelemetryUpdateFrequency must be at least 1.");
                if (value > 60)
                    throw new ArgumentOutOfRangeException("TelemetryUpdateFrequency cannot be more than 60.");

                _TelemetryUpdateFrequency = value;

                waitTime = (int)Math.Floor(1000f / value) - 1;
            }
        }

        /// <summary>
        /// The time in milliseconds between each check if iRacing is running. Use a low value (hundreds of milliseconds) to respond quickly to iRacing startup.
        /// Use a high value (several seconds) to conserve resources if an immediate response to startup is not required.
        /// </summary>
        public static int ConnectSleepTime
        {
            get; set;
        }

        private static int _DriverId;
        /// <summary>
        /// Gets the Id (CarIdx) of yourself (the driver running this application).
        /// </summary>
        public static int DriverId { get { return _DriverId; } }


        static void Main(string[] args)
        {
            //var testYml = File.ReadAllText("session.yml");
            //var model = IRacingSessionModel.Serialize(testYml);
            //var race = model.SessionInfo.Sessions.FirstOrDefault(x => x.SessionType.ToLower() == "race");
            //var gains = _rating.CalculateGains(race, model.DriverInfo.Drivers);


            sdk = new Serialization.IRacingSDK();
            ConnectSleepTime = 1000;
            Task.Run(() => Loop());

            System.Console.ReadLine();
        }

        private static void Loop()
        {
            int lastUpdate = -1;

            while (true)
            {
                // Check if we can find the sim
                if (sdk.IsConnected())
                {
                    if (!_IsConnected)
                    {
                        // If this is the first time, raise the Connected event
                        //this.RaiseEvent(OnConnected, EventArgs.Empty);
                    }

                    _hasConnected = true;
                    _IsConnected = true;

                    //readMutex.WaitOne(8);

                    int attempts = 0;
                    const int maxAttempts = 99;

                    object sessionnum = TryGetSessionNum();
                    while (sessionnum == null && attempts <= maxAttempts)
                    {
                        attempts++;
                        sessionnum = TryGetSessionNum();
                    }
                    if (attempts >= maxAttempts)
                    {
                        System.Console.WriteLine("Session num too many attempts");
                        continue;
                    }

                    // Parse out your own driver Id
                    if (DriverId == -1)
                    {
                        _DriverId = (int)sdk.GetData("PlayerCarIdx");
                    }

                    // Get the session time (in seconds) of this update
                    var time = (double)sdk.GetData("SessionTime");

                    // Raise the TelemetryUpdated event and pass along the lap info and session time
                    //var telArgs = new TelemetryUpdatedEventArgs(new TelemetryInfo(sdk), time);
                    // this.RaiseEvent(OnTelemetryUpdated, telArgs);

                    // Is the session info updated?
                    int newUpdate = sdk.Header.SessionInfoUpdate;
                    if (newUpdate != lastUpdate)
                    {
                        lastUpdate = newUpdate;

                        // Get the session info string
                        var sessionInfo = sdk.GetSessionInformation(); //.GetSessionInfo();
                        var race = sessionInfo.SessionInfo.Sessions.FirstOrDefault(x => x.SessionType.ToLower() == "race");
                        if (race != null && race.ResultsLapsComplete >= 0 )
                        {
                            var gains = _rating.CalculateGains(race, sessionInfo.DriverInfo.Drivers);

                            Console.Clear();
                            var classes = sessionInfo.DriverInfo.Drivers
                                .Where(x => x.IsSpectator == 0)
                                .Select(x => (x.CarClassID, x.CarIdx))
                                .GroupBy(x => x.CarClassID)
                                .ToDictionary(g => g.Key, g => g.ToList());
                            
                            foreach (var key in classes.Keys)
                            {
                                foreach (var car in race.ResultsPositions.Where(x => classes[key].Any(y => y.CarIdx == x.CarIdx)).OrderBy(x => x.ClassPosition))
                                {
                                    var driver = sessionInfo.DriverInfo.Drivers.Where(x => x.CarIdx == car.CarIdx).FirstOrDefault();
                                    var gain = gains[car.CarIdx];
                                    Console.WriteLine($"{key}: {driver.UserName} - {driver.IRating}({gain})");
                                }
                            }
                        }
                        // Raise the SessionInfoUpdated event and pass along the session info and session time.
                        //var sessionArgs = new SessionInfoUpdatedEventArgs(sessionInfo, time);
                        //this.RaiseEvent(OnSessionInfoUpdated, sessionArgs);
                    }


                }
                else if (_hasConnected)
                {
                    // We have already been initialized before, so the sim is closing
                    //this.RaiseEvent(OnDisconnected, EventArgs.Empty);

                    sdk.Shutdown();
                    _DriverId = -1;
                    lastUpdate = -1;
                    _IsConnected = false;
                    _hasConnected = false;
                }
                else
                {
                    _IsConnected = false;
                    _hasConnected = false;
                    _DriverId = -1;

                    //Try to find the sim
                    sdk.Startup();
                }

                // Sleep for a short amount of time until the next update is available
                if (_IsConnected)
                {
                    if (waitTime <= 0 || waitTime > 1000) waitTime = 15;
                    Thread.Sleep(waitTime);
                }
                else
                {
                    // Not connected yet, no need to check every 16 ms, let's try again in some time
                    Thread.Sleep(ConnectSleepTime);
                }
            }

        }
        private static object TryGetSessionNum()
        {
            try
            {
                var sessionnum = sdk.GetData("SessionNum");
                return sessionnum;
            }
            catch
            {
                return null;
            }
        }
    }
}
