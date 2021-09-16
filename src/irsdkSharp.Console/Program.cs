using irsdkSharp.Calculation;
using irsdkSharp.Serialization;
using irsdkSharp.Serialization.Models.Session;
using System;
using System.IO;
using System.Linq;
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
        private static IRacingSDK sdk;
        private static bool _IsConnected = false;
        private static IRacingSessionModel _session;

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


            sdk = new IRacingSDK();
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

                    var data = sdk.GetSerializedData();

                    // Raise the TelemetryUpdated event and pass along the lap info and session time
                    //var telArgs = new TelemetryUpdatedEventArgs(new TelemetryInfo(sdk), time);
                    // this.RaiseEvent(OnTelemetryUpdated, telArgs);

                    // Is the session info updated?
                    int newUpdate = sdk.Header.SessionInfoUpdate;
                    if (newUpdate != lastUpdate)
                    {
                        lastUpdate = newUpdate;
                        _session = sdk.GetSerializedSessionInfo();
                    }

                    if(data != null && _session != null)
                    {
                        Console.SetCursorPosition(0,0);


                        foreach (var car in data.Data.Cars.OrderByDescending(x => x.CarIdxLap).ThenByDescending(x => x.CarIdxLapDistPct))
                        {
                            var currentData = _session.DriverInfo.Drivers.Where(y => y.CarIdx == car.CarIdx).FirstOrDefault();
                            if (currentData != null && car.CarIdxEstTime != 0)
                            {
                                Console.WriteLine($"{currentData.CarNumber}\t{string.Format("{0:0.00}", car.CarIdxEstTime)}\t{string.Format("{0:0.00}", car.CarIdxLapDistPct * 100)}");
                            }

                        }

                        //foreach (var driver in _session.DriverInfo.Drivers.Where(x => x.IsSpectator == 0).Where(x => x.CarIsPaceCar == "0").Where(x => x.CarIsAI == "0"))
                        //{
                        //    var currentData = data.Data.Cars.Where(x => x.CarIdx == driver.CarIdx).FirstOrDefault();
                        //    if (currentData.CarIdxLap != 0 && currentData.CarIdxLapDistPct == -1)
                        //    {
                        //        Console.WriteLine($"{driver.CarNumber} {string.Format("{0:0.00}", currentData.CarIdxLapDistPct * 100)}");
                        //    } 
                        //    else
                        //    {
                        //        var a = "";
                        //    }
                        //}
                    }

                }
                else if (_hasConnected)
                {
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
                    if (waitTime <= 0 || waitTime > 1000) waitTime = 250;
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