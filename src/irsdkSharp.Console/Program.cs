using irsdkSharp.Serialization;
using irsdkSharp.Serialization.Models.Session;
using System;
using System.Linq;

namespace irsdkSharp.ConsoleTest
{
    class Program
    {
        private static readonly IRacingSDK Sdk = new();
        
        private static IRacingSessionModel _session;
        private static int _DriverId = -1;
        private static int _lastUpdate = -1;
        static void Main(string[] args)
        {
            Sdk.OnDataChanged += Sdk_OnDataChanged;
            Sdk.OnDisconnected += Sdk_OnDisconnected;
            Sdk.OnConnected += Sdk_OnConnected;
            
            Console.WriteLine("Starting...");
            Sdk.Start();
            Console.WriteLine("Started");

            Console.ReadLine();
            
            Sdk.Stop();
            Console.WriteLine("Stopped");
            
            Console.ReadLine();
        }

        private static void Sdk_OnConnected(object sender, EventArgs eventArgs)
        {
            Console.WriteLine("Connected");
        }

        private static void Sdk_OnDisconnected(object sender, EventArgs eventArgs)
        {
            Console.WriteLine("Disconnected");
        }

        private static void Sdk_OnDataChanged(object sender, EventArgs eventArgs)
        {
            var currentlyConnected = Sdk.IsConnected;

            if (currentlyConnected)
            {
                // Is the session info updated?
                int newUpdate = Sdk.Header.SessionInfoUpdate;
                if (newUpdate != _lastUpdate)
                {
                    _lastUpdate = newUpdate;
                    _session = Sdk.GetSerializedSessionInfo();
                }

                if (_DriverId == -1)
                {
                    _DriverId = (int)Sdk.GetData("PlayerCarIdx");
                    Console.WriteLine(_DriverId);
                }

                var data = Sdk.GetSerializedData();

                if (data != null && _session != null)
                {
                    Console.SetCursorPosition(0, 0);


                    foreach (var car in data.Data.Cars.OrderByDescending(x => x.CarIdxLap).ThenByDescending(x => x.CarIdxLapDistPct))
                    {
                        var currentData = _session.DriverInfo.Drivers.Where(y => y.CarIdx == car.CarIdx).FirstOrDefault();
                        if (currentData != null && car.CarIdxEstTime != 0)
                        {
                            Console.WriteLine($"{currentData.CarNumber}\t{string.Format("{0:0.00}", car.CarIdxEstTime)}\t{string.Format("{0:0.00}", car.CarIdxLapDistPct * 100)}");
                        }

                    }
                }
            }
        }

    }
}