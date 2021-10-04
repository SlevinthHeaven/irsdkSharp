using irsdkSharp.Serialization;
using irsdkSharp.Serialization.Models.Session;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace irsdkSharp.ConsoleTest
{
    class Program
    {
        private static IRacingSDK sdk;
        private static IRacingSessionModel _session;
        private static int _DriverId;

        /// <summary>
        /// Gets the Id (CarIdx) of yourself (the driver running this application).
        /// </summary>
        public static int DriverId { get { return _DriverId; } }


        static void Main(string[] args)
        {
            sdk = new IRacingSDK();
            Task.Run(() => Loop());
            Console.ReadLine();
        }

        private static void Loop()
        {
            int lastUpdate = -1;

            while (true)
            {
                var currentlyConnected = sdk.IsConnected();
                
                // Check if we can find the sim
                if (currentlyConnected)
                {
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
                    }

                    Thread.Sleep(15);
                }
                else
                {
                    Thread.Sleep(1000);
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