using irsdkSharp.Calculation.Models;
using irsdkSharp.Serialization;
using irsdkSharp.Serialization.Models.Data;
using irsdkSharp.Serialization.Models.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using irsdkSharp.Models;

namespace irsdkSharp.Calculation
{
    public static class RelativeExtensions
    {
        public static Dictionary<int, CarRelativeModel> CalculateRelatives(Session dataModel, IRacingSessionModel sessionModel)
        {
            if (sessionModel == null) return null;
            if (dataModel == null) return null;

            var relatives = new Dictionary<int, CarRelativeModel>();

            var currentCar = sessionModel.DriverInfo.Drivers.FirstOrDefault(x => x.CarIdx == dataModel.PlayerCarIdx);
            
            if (currentCar == null || currentCar.IsSpectator != 0)
            {
                currentCar = sessionModel.DriverInfo.Drivers.FirstOrDefault(x => x.CarIdx == dataModel.CamCarIdx);
            }
            if (currentCar == null || currentCar.IsSpectator != 0)
            {
                currentCar = sessionModel.DriverInfo.Drivers.FirstOrDefault(x => x.CarIsPaceCar == "0" && x.CarIsAI == "0");
            }

            var currentCarData = new CarModel();// dataModel.Data.Cars.FirstOrDefault(x => x.CarIdx == currentCar.CarIdx);

            foreach (var car in Enumerable.Empty<CarModel>())//dataModel.Data.Cars)
            {
                if (car.CarIdx == currentCar.CarIdx)
                {
                    relatives.Add(car.CarIdx, new CarRelativeModel
                    {
                        Ahead = TimeSpan.FromSeconds(0),
                        Behind = TimeSpan.FromSeconds(0)
                    });
                }
                else if (car.CarIdxLapDistPct > currentCarData.CarIdxLapDistPct)
                {
                    //time remaining for car (ahead) to finish the lap they are on
                    var remainingThisLap = car.CarIdxLastLapTime * (1 - car.CarIdxLapDistPct);

                    relatives.Add(car.CarIdx, new CarRelativeModel
                    {
                        Ahead = TimeSpan.FromSeconds(car.CarIdxEstTime - currentCarData.CarIdxEstTime),
                        //the time the currentCar is into the lap + the remaining time for the car (ahead)
                        Behind = TimeSpan.FromSeconds(currentCarData.CarIdxEstTime + remainingThisLap),
                    });
                }
                else if (car.CarIdxLapDistPct < currentCarData.CarIdxLapDistPct)
                {
                    //time remaining for currentCar to finish the lap they are on
                    var remainingThisLap = currentCarData.CarIdxLastLapTime * (1 - currentCarData.CarIdxLapDistPct);
                    relatives.Add(car.CarIdx, new CarRelativeModel
                    {
                        //the time for the currentCar to finish the lap + the time the car behind is into their current lap.
                        Ahead = TimeSpan.FromSeconds(remainingThisLap + car.CarIdxEstTime),
                        Behind = TimeSpan.FromSeconds(currentCarData.CarIdxEstTime - car.CarIdxEstTime),
                    });
                }
            }

            return relatives;
        }

        public static Dictionary<int, CarRelativeModel> CalculateRelatives(this IRacingSDK racingSDK)
        {
            var sessionModel = racingSDK.GetSerializedSessionInfo();

            if(sessionModel == null) return null;

            return CalculateRelatives(racingSDK.Session, sessionModel);
        }
    }
}
