using irsdkSharp.Calculation.Models;
using irsdkSharp.Serialization;
using irsdkSharp.Serialization.Models.Data;
using irsdkSharp.Serialization.Models.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace irsdkSharp.Calculation
{
    public static class RelativeExtensions
    {
        public static List<CarRelativeModel> CalculateRelatives(IRacingDataModel dataModel, IRacingSessionModel sessionModel)
        {
            if (sessionModel == null) return null;
            if (dataModel == null) return null;

            var orderedDrivers = dataModel.Data.Cars
                .Where(x => x.CarIdxLapDistPct != -1)
                .OrderByDescending(x => x.CarIdxLapDistPct).ToList();

            var relatives = new List<CarRelativeModel>();

            var currentCar = sessionModel.DriverInfo.Drivers.FirstOrDefault(x => x.CarIdx == dataModel.Data.PlayerCarIdx);
            
            if (currentCar == null || currentCar.IsSpectator != 0)
            {
                currentCar = sessionModel.DriverInfo.Drivers.FirstOrDefault(x => x.CarIdx == dataModel.Data.CamCarIdx);
            }
            if (currentCar == null || currentCar.IsSpectator != 0)
            {
                currentCar = sessionModel.DriverInfo.Drivers.FirstOrDefault(x => x.CarIsPaceCar == 0 && x.CarIsAI == 0);
            }
            var currentCarData = dataModel.Data.Cars.FirstOrDefault(x => x.CarIdx == currentCar.CarIdx);

            for (var carIdx = 0; carIdx < dataModel.Data.Cars.Count(); carIdx++)
            {
                var car = dataModel.Data.Cars[carIdx];

                if (car.CarIdx == currentCar.CarIdx)
                {
                    relatives.Add(new CarRelativeModel
                    {
                        CarIdx = car.CarIdx,
                        Ahead = TimeSpan.FromSeconds(0),
                        Behind = TimeSpan.FromSeconds(0),
                        Lap = car.CarIdxLap,
                        Selected = true,
                        Position = carIdx + 1
                    });
                    continue;
                }
                
                if (car.CarIdxLapDistPct > currentCarData.CarIdxLapDistPct)
                {
                    //time remaining for car (ahead) to finish the lap they are on
                    var remainingThisLap = currentCarData.CarIdxLastLapTime * (1 - car.CarIdxLapDistPct);

                    relatives.Add(new CarRelativeModel
                    {
                        CarIdx = car.CarIdx,
                        Ahead = TimeSpan.FromSeconds(car.CarIdxEstTime - currentCarData.CarIdxEstTime),
                        //the time the currentCar is into the lap + the remaining time for the car (ahead)
                        Behind = TimeSpan.FromSeconds(currentCarData.CarIdxEstTime + remainingThisLap),
                        Lap = car.CarIdxLap,
                        Selected = false,
                        Position = carIdx + 1
                    });
                }
                
                if (car.CarIdxLapDistPct < currentCarData.CarIdxLapDistPct)
                {
                    //time remaining for currentCar to finish the lap they are on
                    var remainingThisLap = car.CarIdxLastLapTime * (1 - currentCarData.CarIdxLapDistPct);
                    relatives.Add(new CarRelativeModel
                    {
                        CarIdx = car.CarIdx,
                        //the time for the currentCar to finish the lap + the time the car behind is into their current lap.
                        Ahead = TimeSpan.FromSeconds(remainingThisLap + car.CarIdxEstTime),
                        Behind = TimeSpan.FromSeconds(currentCarData.CarIdxEstTime - car.CarIdxEstTime),
                        Lap = car.CarIdxLap,
                        Selected = false,
                        Position = carIdx + 1
                    });
                }
            }

            return relatives;
        }

        public static List<CarRelativeModel> CalculateRelatives(this IRacingSDK racingSDK)
        {
            var sessionModel = racingSDK.GetSerializedSessionInfo();

            if (sessionModel == null) return null;

            var dataModel = racingSDK.GetSerializedData();

            if (dataModel == null) return null;

            return CalculateRelatives(dataModel, sessionModel);
        }
    }
}