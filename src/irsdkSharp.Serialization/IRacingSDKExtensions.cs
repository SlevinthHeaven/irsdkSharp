using irsdkSharp.Models;
using irsdkSharp.Serialization.Models.Data;
using irsdkSharp.Serialization.Models.Session;
using System;
using System.Collections.Generic;
using System.Text;
using irsdkSharp.Enums;

namespace irsdkSharp.Serialization
{
    public static class IRacingSDKExtensions
    {
        public static IRacingSessionModel GetSerializedSessionInfo(this IRacingSDK racingSDK)
        {
            var sessionInfo = racingSDK.GetSessionInfo();

            if (sessionInfo == null)
            {
                return null;
            }

            return IRacingSessionModel.Serialize(sessionInfo);
        }

        public static IRacingDataModel GetSerializedData(this IRacingSDK racingSDK)
        {
            if (racingSDK.IsInitialized && racingSDK.Header != null)
            {
                var length = (int)IRacingSDK.GetFileMapView(racingSDK).Capacity;
                var data = new byte[length];
                IRacingSDK.GetFileMapView(racingSDK).ReadArray(0, data, 0, length);
                //Serialise the string into objects, tada!
                return IRacingDataModel.Serialize(
                    data[racingSDK.Header.Offset..(racingSDK.Header.Offset + racingSDK.Header.BufferLength)],
                    racingSDK.Headers.Values);
            }
            return null;
        }

        public static List<CarModel> GetPositions(this IRacingSDK racingSDK)
        {
            var tick = racingSDK.Session.SessionTick;
            var CarIdxBestLapNum = racingSDK.Session.CarIdxBestLapNum;
            var CarIdxBestLapTime = racingSDK.Session.CarIdxBestLapTime;
            var CarIdxClassPosition = racingSDK.Session.CarIdxClassPosition;
            var CarIdxEstTime = racingSDK.Session.CarIdxEstTime;
            var CarIdxF2Time = racingSDK.Session.CarIdxF2Time;
            var CarIdxGear = racingSDK.Session.CarIdxGear;
            var CarIdxLap = racingSDK.Session.CarIdxLap;
            var CarIdxLapCompleted = racingSDK.Session.CarIdxLapCompleted;
            var CarIdxLapDistPct = racingSDK.Session.CarIdxLapDistPct;
            var CarIdxLastLapTime = racingSDK.Session.CarIdxLastLapTime;
            var CarIdxOnPitRoad = racingSDK.Session.CarIdxOnPitRoad;
            var CarIdxP2P_Count = racingSDK.Session.CarIdxP2P_Count;
            var CarIdxP2P_Status = racingSDK.Session.CarIdxP2P_Status;
            var CarIdxPosition = racingSDK.Session.CarIdxPosition;
            var CarIdxRPM = racingSDK.Session.CarIdxRPM;
            var CarIdxSteer = racingSDK.Session.CarIdxSteer;
            var CarIdxTrackSurface = racingSDK.Session.CarIdxTrackSurface;
            var CarIdxTrackSurfaceMaterial = racingSDK.Session.CarIdxTrackSurfaceMaterial;

            var results = new List<CarModel>();
            for (var i = 0; i< 64; i++)
            {
                results.Add(new CarModel
                {
                    CarIdx = i,
                    CarIdxBestLapNum = CarIdxBestLapNum[i],
                    CarIdxBestLapTime = CarIdxBestLapTime[i],
                    CarIdxClassPosition = CarIdxClassPosition[i],
                    CarIdxEstTime = CarIdxEstTime[i],
                    CarIdxF2Time = CarIdxF2Time[i],
                    CarIdxGear = CarIdxGear[i],
                    CarIdxLap = CarIdxLap[i],
                    CarIdxLapCompleted = CarIdxLapCompleted[i],
                    CarIdxLapDistPct = CarIdxLapDistPct[i],
                    CarIdxLastLapTime = CarIdxLastLapTime[i],
                    CarIdxOnPitRoad = CarIdxOnPitRoad[i],
                    CarIdxP2P_Count = CarIdxP2P_Count[i],
                    CarIdxP2P_Status = CarIdxP2P_Status[i],
                    CarIdxPosition = CarIdxPosition[i],
                    CarIdxRPM = CarIdxRPM[i],
                    CarIdxSteer = CarIdxSteer[i],
                    CarIdxTrackSurface = CarIdxTrackSurface[i],
                    CarIdxTrackSurfaceMaterial = CarIdxTrackSurfaceMaterial[i],
                    SessionTime = tick
                });
            }
            return results;
        }
    }
}