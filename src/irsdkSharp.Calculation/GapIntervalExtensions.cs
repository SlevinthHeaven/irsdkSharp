using irsdkSharp.Calculation.Models;
using irsdkSharp.Serialization;
using irsdkSharp.Serialization.Models.Data;
using irsdkSharp.Serialization.Models.Session;
using System;
using System.Collections.Generic;
using System.Linq;

namespace irsdkSharp.Calculation
{
    public static class GapIntervalExtensions
    {
        /// <summary>
        /// Calculates Gaps and Intervals for all cars and classes in the current race
        /// 
        /// This uses Session Info and Data models to calculate positions on the fly rather than using
        /// the official values in the session info model. This means the Gaps and Intervals are as 
        /// close to real-time as we can get based on the data passed in.
        /// 
        /// </summary>
        /// <param name="dataModel"></param>
        /// <param name="sessionModel"></param>
        /// <returns></returns>
        public static List<CarGapIntervalModel> CalculateGapsAndIntervals(IRacingDataModel dataModel, IRacingSessionModel sessionModel)
        {
            if (sessionModel == null) return null;
            if (dataModel == null) return null;

            var results = new List<CarGapIntervalModel>();

            //All drivers ordered 

            var orderedDrivers = dataModel.Data.Cars
                .OrderByDescending(x => x.CarIdxLapDistPct)
                .ThenByDescending(x => x.CarIdxLap).ToList();


            //find car in first
            var leader = orderedDrivers.FirstOrDefault();

            var drivers = sessionModel.DriverInfo.Drivers
                    .Where(x => x.IsSpectator == 0)
                    .Where(x => x.CarIsPaceCar == "0")
                    .Where(x => x.CarIsAI == "0")
                    .ToList();

            //get the classes
            var classes = drivers
                .Select(x => x.CarClassID)
                .GroupBy(x => x)
                .Select(x => x.Key)
                .ToList();

            var classLeaders = new Dictionary<int, CarModel>();
            classes.ForEach(classId =>
            {
                var driversInClass = drivers
                    .Where(x => x.CarClassID == classId)
                    .Select(x=>x.CarIdx)
                    .ToList();

                classLeaders.Add(classId, orderedDrivers
                        .Where(x => driversInClass.Any(driver => driver ==  x.CarIdx))
                        .FirstOrDefault()
                    );
            });

            var lastClassCar = new Dictionary<int, CarModel>(classLeaders);

            //create Leader result model
            results.Add(new CarGapIntervalModel
            {
                CarIdx = leader.CarIdx,
                ClassGap = TimeSpan.Zero,
                ClassGapLapDifference = 0,
                ClassInterval = TimeSpan.Zero,
                ClassIntervalLapDifference = 0,
                Gap = TimeSpan.Zero,
                GapLapDifference = 0,
                Interval = TimeSpan.Zero,
                IntervalLapDifference = 0
            });

            for (int i = 1; i < orderedDrivers.Count(); i++)
            {
                var car = orderedDrivers[i];
                var carInFront = orderedDrivers[i-1];
                var currentDriver = drivers.Where(driver => driver.CarIdx == car.CarIdx).FirstOrDefault();

                if (currentDriver == null) throw new NullReferenceException("Driver should not be null here, they are in the data not the session.. odd");
                
                //Create model for current car
                var carModel = new CarGapIntervalModel
                {
                    CarIdx = car.CarIdx
                };

                //Interval to Leader
                if (car.CarIdxLapDistPct > leader.CarIdxLapDistPct)
                {
                    var remainingThisLap = car.CarIdxLastLapTime * (1 - car.CarIdxLapDistPct);
                    carModel.Interval = TimeSpan.FromSeconds(leader.CarIdxEstTime + remainingThisLap);
                    carModel.IntervalLapDifference = leader.CarIdxLap - car.CarIdxLap - 1;
                }
                else
                {
                    carModel.Interval = TimeSpan.FromSeconds(leader.CarIdxEstTime - car.CarIdxEstTime);
                    carModel.IntervalLapDifference = leader.CarIdxLap - car.CarIdxLap;
                }

                //Gap between driver in front of current car
                if (car.CarIdxLapDistPct > carInFront.CarIdxLapDistPct)
                {
                    var remainingThisLap = car.CarIdxLastLapTime * (1 - car.CarIdxLapDistPct);
                    carModel.Gap = TimeSpan.FromSeconds(carInFront.CarIdxEstTime + remainingThisLap);
                    carModel.GapLapDifference = carInFront.CarIdxLap - car.CarIdxLap - 1;
                }
                else
                {
                    carModel.Gap = TimeSpan.FromSeconds(carInFront.CarIdxEstTime - car.CarIdxEstTime);
                    carModel.GapLapDifference = carInFront.CarIdxLap - car.CarIdxLap;
                }


                //grab current class leader
                CarModel currentClassLeader = null;
                if (classLeaders.ContainsKey(currentDriver.CarClassID))
                {
                    currentClassLeader = classLeaders[currentDriver.CarClassID];
                }
                if (currentClassLeader == null) throw new NullReferenceException("There should always be a class leader.");
               
                //grab last class car used
                CarModel lastCarUsed = null;
                if (lastClassCar.ContainsKey(currentDriver.CarClassID))
                {
                    lastCarUsed = lastClassCar[currentDriver.CarClassID];
                }
                if (lastCarUsed == null) throw new NullReferenceException("There should always be a class leader.");

                if (currentClassLeader.CarIdx == car.CarIdx)
                {
                    carModel.ClassGap = TimeSpan.Zero;
                    carModel.ClassGapLapDifference = 0;
                    carModel.ClassInterval = TimeSpan.Zero;
                    carModel.ClassIntervalLapDifference = 0;
                }
                else
                {
                    //Interval to Class Leader
                    if (car.CarIdxLapDistPct > currentClassLeader.CarIdxLapDistPct)
                    {
                        var remainingThisLap = car.CarIdxLastLapTime * (1 - car.CarIdxLapDistPct);

                        carModel.ClassInterval = TimeSpan.FromSeconds(currentClassLeader.CarIdxEstTime + remainingThisLap);
                        carModel.ClassIntervalLapDifference = currentClassLeader.CarIdxLap - car.CarIdxLap - 1;
                    }
                    else
                    {
                        carModel.ClassInterval = TimeSpan.FromSeconds(currentClassLeader.CarIdxEstTime - car.CarIdxEstTime);
                        carModel.ClassIntervalLapDifference = currentClassLeader.CarIdxLap - car.CarIdxLap;
                    }

                    //Interval to Class Leader
                    if (car.CarIdxLapDistPct > lastCarUsed.CarIdxLapDistPct)
                    {
                        var remainingThisLap = car.CarIdxLastLapTime * (1 - car.CarIdxLapDistPct);

                        carModel.ClassGap = TimeSpan.FromSeconds(lastCarUsed.CarIdxEstTime + remainingThisLap);
                        carModel.ClassGapLapDifference = lastCarUsed.CarIdxLap - car.CarIdxLap - 1;
                    }
                    else
                    {
                        carModel.ClassGap = TimeSpan.FromSeconds(lastCarUsed.CarIdxEstTime - car.CarIdxEstTime);
                        carModel.ClassGapLapDifference = lastCarUsed.CarIdxLap - car.CarIdxLap;
                    }

                    lastClassCar[currentDriver.CarClassID] = car;
                }

                results.Add(carModel);

            }

            return results;
        }

        public static List<CarGapIntervalModel> CalculateGapsAndIntervals(this IRacingSDK racingSDK)
        {
            var sessionModel = racingSDK.GetSerializedSessionInfo();

            if(sessionModel == null) return null;

            var dataModel = racingSDK.GetSerializedData();

            if(dataModel == null) return null;

            return CalculateGapsAndIntervals(dataModel, sessionModel);
        }
    }
}
