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

            var trackLength = float.Parse(sessionModel.WeekendInfo.TrackLength.Replace("km","").Replace("mi","").Trim());
            if(sessionModel.WeekendInfo.TrackLength.Contains("km"))
            {
                trackLength *= 1000;
            } else
            {
                trackLength = (trackLength * (5 / 8)) * 1000;
            }



            //Get Current Session
            var currentSessionNumber = dataModel.Data.SessionNum;

            var currentSession = sessionModel.SessionInfo.Sessions
                .Where(x => x.SessionNum == currentSessionNumber)
                .FirstOrDefault();


            //All drivers ordered 

            var orderedDrivers = dataModel.Data.Cars
                .Where(x => x.CarIdxLapDistPct != -1)
                .OrderByDescending(x => x.CarIdxLap)
                .ThenByDescending(x => x.CarIdxLapDistPct).ToList();

            //find car in first
            var leader = orderedDrivers.FirstOrDefault();
            var leaderSession = currentSession.ResultsPositions.Where(ses => ses.CarIdx == leader.CarIdx).FirstOrDefault();
            var leaderDriver = sessionModel.DriverInfo.Drivers.Where(ses => ses.CarIdx == leader.CarIdx).FirstOrDefault();

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
                ClassInterval = TimeSpan.Zero,
                ClassIntervalLapDifference = 0,
                Gap = TimeSpan.Zero,
                Interval = TimeSpan.Zero,
                IntervalLapDifference = 0
            });

            for (int i = 1; i < orderedDrivers.Count(); i++)
            {
                var car = orderedDrivers[i];
                var carInFront = orderedDrivers[i-1];
                var currentDriver = drivers.Where(driver => driver.CarIdx == car.CarIdx).FirstOrDefault();

                if (currentDriver == null) continue;
                
                //Create model for current car
                var carModel = new CarGapIntervalModel
                {
                    CarIdx = car.CarIdx
                };

                //Interval to Leader
                var times = BetweenCars(leader, car, trackLength);
                carModel.Interval = times.Item1;
                carModel.IntervalLapDifference = times.Item2;

                //Gap between driver in front of current car
                times = BetweenCars(carInFront, car, trackLength);
                carModel.Gap = times.Item1;
               

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
                    carModel.ClassInterval = TimeSpan.Zero;
                    carModel.ClassIntervalLapDifference = 0;
                }
                else
                {
                    //Interval to Class Leader
                    times = BetweenCars(currentClassLeader, car, trackLength);
                    carModel.ClassInterval = times.Item1;
                    carModel.ClassIntervalLapDifference = times.Item2;

                    //Gap between class driver in front of current car
                    times = BetweenCars(lastCarUsed, car, trackLength);
                    carModel.ClassGap = times.Item1;

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

        private static (TimeSpan, int) BetweenCars(CarModel leader, CarModel car, float trackLength)
        {
            TimeSpan time;
            int lap;

            if (car.CarIdxLapDistPct > leader.CarIdxLapDistPct)
            {
                var remainingThisLap = car.CarIdxLastLapTime * (1 - car.CarIdxLapDistPct);
                time = TimeSpan.FromSeconds(leader.CarIdxEstTime + remainingThisLap);
                lap = leader.CarIdxLap - car.CarIdxLap - 1;
            }
            else
            {
                if (leader.CarIdxLapDistPct > 0.5 && leader.CarIdxEstTime < 2)
                {
                    //leader has crossed the timing beam but not the start finish
                    //this seems to be a long standing issue
                    var remainingThisLap = car.CarIdxLastLapTime * (1 - car.CarIdxLapDistPct);
                    time = TimeSpan.FromSeconds(leader.CarIdxEstTime + remainingThisLap);
                }
                else
                {
                    time = TimeSpan.FromSeconds(leader.CarIdxEstTime - car.CarIdxEstTime);

                    // if the time is -ve we take their last lap
                    if (time.TotalMilliseconds < 0)
                    {
                        var timeInToLap = car.CarIdxLastLapTime * car.CarIdxLapDistPct;
                        time = TimeSpan.FromSeconds(leader.CarIdxEstTime - timeInToLap);
                    }

                    //// if the time is STILL -ve we use distance % and current speed
                    //if (time.TotalMilliseconds < 0)
                    //{
                    //    var driverAvgSpeed = trackLength / car.CarIdxLastLapTime;
                    //    var distanceBetween = (leader.CarIdxLapDistPct - car.CarIdxLapDistPct) * trackLength;
                    //    var timeGap = distanceBetween / driverAvgSpeed;
                    //    time = TimeSpan.FromSeconds(timeGap);
                    //}
                }
                lap = leader.CarIdxLap - car.CarIdxLap;
            }

            return (time, lap);
        }
    }
}
