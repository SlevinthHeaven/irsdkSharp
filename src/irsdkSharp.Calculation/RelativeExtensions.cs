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
        public static Dictionary<int, CarRelativeModel> CalculateRelatives(IRacingDataModel dataModel, IRacingSessionModel sessionModel)
        {
            if (sessionModel == null) return null;
            if (dataModel == null) return null;

            var relatives = new Dictionary<int, CarRelativeModel>();

            var currentCarIdx = dataModel.Data.PlayerCarIdx;
            if (sessionModel.DriverInfo.Drivers[currentCarIdx].IsSpectator != 0)
            {
                currentCarIdx = dataModel.Data.CamCarIdx;
            }

            var currentCar = dataModel.Data.Cars[currentCarIdx];
            foreach (var car in dataModel.Data.Cars)
            {
                if (car.CarIdx == currentCar.CarIdx)
                {
                    relatives.Add(car.CarIdx, new CarRelativeModel
                    {
                        Ahead = TimeSpan.FromSeconds(0),
                        Behind = TimeSpan.FromSeconds(0)
                    });
                }
                else if (car.CarIdxLapDistPct > currentCar.CarIdxLapDistPct)
                {
                    //time remaining for car (ahead) to finish the lap they are on
                    var remainingThisLap = car.CarIdxLastLapTime * (1 - car.CarIdxLapDistPct);

                    relatives.Add(car.CarIdx, new CarRelativeModel
                    {
                        Ahead = TimeSpan.FromSeconds(car.CarIdxEstTime - currentCar.CarIdxEstTime),
                        //the time the currentCar is into the lap + the remaining time for the car (ahead)
                        Behind = TimeSpan.FromSeconds(currentCar.CarIdxEstTime + remainingThisLap),
                    });
                }
                else if (car.CarIdxLapDistPct < currentCar.CarIdxLapDistPct)
                {
                    //time remaining for currentCar to finish the lap they are on
                    var remainingThisLap = currentCar.CarIdxLastLapTime * (1 - currentCar.CarIdxLapDistPct);
                    relatives.Add(car.CarIdx, new CarRelativeModel
                    {
                        //the time for the currentCar to finish the lap + the time the car behind is into their current lap.
                        Ahead = TimeSpan.FromSeconds(remainingThisLap + car.CarIdxEstTime),
                        Behind = TimeSpan.FromSeconds(currentCar.CarIdxEstTime - car.CarIdxEstTime),
                    });
                }
            }

            return relatives;
        }

        public static Dictionary<int, CarRelativeModel> CalculateRelatives(this IRacingSDK racingSDK)
        {
            var sessionModel = racingSDK.GetSerializedSessionInfo();

            if(sessionModel == null) return null;

            var dataModel = racingSDK.GetSerializedData();

            if(dataModel == null) return null;

            return CalculateRelatives(dataModel, sessionModel);
        }
    }
}
