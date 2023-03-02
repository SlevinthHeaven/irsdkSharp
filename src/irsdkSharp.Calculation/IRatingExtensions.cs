﻿using irsdkSharp.Serialization;
using irsdkSharp.Serialization.Models.Data;
using irsdkSharp.Serialization.Models.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace irsdkSharp.Calculation
{
    public static class IRatingExtensions
    {
        private static readonly double _initialConstant = 1600 / Math.Log(2);
              
        public static Dictionary<int, double> CalculateIRatingGains(IRacingDataModel dataModel, IRacingSessionModel sessionModel)
        {
            if (sessionModel == null) return null;

            if (dataModel == null) return null;
            var currentSessionId = dataModel.Data.SessionNum;
            var currentSession = sessionModel.SessionInfo.Sessions.Where(x=>x.SessionNum == currentSessionId).FirstOrDefault();

            if (currentSession == null) return null;

            if (currentSession.ResultsPositions == null) return null;

            var drivers = sessionModel.DriverInfo.Drivers;

            var result = new Dictionary<int, double>();
            var classes = drivers
                .Where(x => x.IsSpectator == 0)
                .Where(x => x.CarIsPaceCar == "0")
                .Where(x => x.CarIsAI == "0")
                .Select(x => x.CarClassID)
                .GroupBy(x => x)
                .Select(x => x.Key)
                .ToList();

            classes.ForEach(carClass =>
            {

                var driversInClass = drivers
                    .Where(x => x.IsSpectator == 0)
                    .Where(x => x.CarIsPaceCar == "0")
                    .Where(x => x.CarIsAI == "0")
                    .Where(x => x.CarClassID == carClass).ToList();

                var fieldSize = driversInClass.Count();

                var dns = currentSession.ResultsPositions
                    .Where(x => x.ReasonOutId == 1)
                    .Where(x => driversInClass.Any(y => y.CarIdx == x.CarIdx))
                    .Count();

                var exponentials = new Dictionary<int, double>();
                var probabilities = new Dictionary<int, List<double>>();
                var expectedScore = new Dictionary<int, double>();
                var change = new Dictionary<int, double>();
                var expectedDNS = new Dictionary<int, double>();
                var changeDNS = new Dictionary<int, double>();
                var fudgeFactor = new Dictionary<int, double>();

                //Calculate exponentials
                driversInClass.ForEach(x => exponentials.Add(x.CarIdx, Math.Exp((x.IRating * -1) / _initialConstant)));

                driversInClass.ForEach(x =>
                {
                    //calculate _probabilities
                    probabilities.Add(x.CarIdx, new List<double>());
                    driversInClass.ForEach(y =>
                    {
                        probabilities[x.CarIdx].Add(
                            (1 - exponentials[x.CarIdx]) * exponentials[y.CarIdx] /
                            (
                                (1 - exponentials[y.CarIdx]) * exponentials[x.CarIdx] +
                                (1 - exponentials[x.CarIdx]) * exponentials[y.CarIdx])
                            );
                    });

                    //calculate expected score
                    expectedScore.Add(x.CarIdx, probabilities[x.CarIdx].Sum(y => y) - 0.5);

                });

                driversInClass.ForEach(x =>
                {
                    var currentPosition = currentSession.ResultsPositions.Where(y => y.CarIdx == x.CarIdx).FirstOrDefault();
                    if (currentPosition != null && currentPosition.ReasonOutId != 1)
                    {
                        fudgeFactor.Add(x.CarIdx, ((fieldSize - ((double)dns / 2)) / 2 - ((double)currentPosition.ClassPosition + 1)) / 100);
                    }
                });

                driversInClass.ForEach(x =>
                {
                    var currentPosition = currentSession.ResultsPositions.Where(y => y.CarIdx == x.CarIdx).FirstOrDefault();
                    if (currentPosition != null)
                    {
                        if (currentPosition.ReasonOutId != 1)
                        {
                            change.Add(x.CarIdx, (fieldSize - (currentPosition.ClassPosition + 1) - expectedScore[x.CarIdx] - fudgeFactor[x.CarIdx]) * 200 / (fieldSize - dns));
                        }
                    }
                });

                driversInClass.ForEach(x =>
                {
                    var currentPosition = currentSession.ResultsPositions.Where(y => y.CarIdx == x.CarIdx).FirstOrDefault();
                    if (currentPosition != null)
                    {
                        if (currentPosition.ReasonOutId == 1)
                        {
                            expectedDNS.Add(x.CarIdx, expectedScore[x.CarIdx]);
                        }
                    }
                });

                if (dns > 0)
                {
                    var sumOfChangeStarters = change.Values.Sum(x => x);
                    var avgOfExpectedNonStarters = expectedDNS.Values.Average();
                    //based on current position calculate the change in iRating for those that did not start
                    driversInClass.ForEach(x =>
                    {
                        var currentPosition = currentSession.ResultsPositions.Where(y => y.CarIdx == x.CarIdx).FirstOrDefault();
                        if (currentPosition != null)
                        {
                            if (currentPosition.ReasonOutId == 1)
                            {
                                fudgeFactor.Add(x.CarIdx, 0);
                                changeDNS.Add(x.CarIdx, (sumOfChangeStarters / dns * expectedScore[x.CarIdx] / avgOfExpectedNonStarters) * -1);
                            }
                        }
                    });

                    foreach (var key in changeDNS.Keys)
                    {
                        change.Add(key, changeDNS[key]);
                    }
                }
                foreach (var key in change.Keys)
                {
                    result.Add(key, change[key]);
                }
            });

            return result;
        }

        public static Dictionary<int, double> CalculateIRatingGains(this IRacingSdk racingSdk)
        {
            var sessionModel = racingSdk.GetSerializedSessionInfo();

            var dataModel = racingSdk.GetSerializedData();

            return CalculateIRatingGains(dataModel, sessionModel);
        }


        public static Dictionary<int, int> CalculateSOFs(IRacingDataModel dataModel, IRacingSessionModel sessionModel)
        {
            if (sessionModel == null) return null;

            if (dataModel == null) return null;
          
            var drivers = sessionModel.DriverInfo.Drivers;

            var result = new Dictionary<int, int>();
            var classes = drivers
                .Where(x => x.IsSpectator == 0)
                .Where(x => x.CarIsPaceCar == "0")
                .Where(x => x.CarIsAI == "0")
                .Select(x => x.CarClassID)
                .GroupBy(x => x)
                .Select(x => x.Key)
                .ToList();

            classes.ForEach(carClass =>
            {
                var driversInClass = drivers
                    .Where(x => x.IsSpectator == 0)
                    .Where(x => x.CarIsPaceCar == "0")
                    .Where(x => x.CarIsAI == "0")
                    .Where(x => x.CarClassID == carClass).ToList();

                var fieldSize = driversInClass.Count();

                var exponentials = new List<double>();

                //Calculate exponentials
                driversInClass.ForEach(x => exponentials.Add(Math.Exp((x.IRating * -1) / _initialConstant)));

                result.Add(carClass, Convert.ToInt32(_initialConstant * Math.Log(driversInClass.Count / exponentials.Sum())));

            });

            return result;
        }
    }
}