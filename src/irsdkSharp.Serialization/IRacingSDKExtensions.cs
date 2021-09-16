using irsdkSharp.Models;
using irsdkSharp.Serialization.Models.Data;
using irsdkSharp.Serialization.Models.Session;
using System.Collections.Generic;

namespace irsdkSharp.Serialization
{
    public static class IRacingSDKExtensions
    {
        public static Data GetData(this IRacingSDK racingSDK) 
        {
            return new Data(racingSDK);
        }
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
            var tick = racingSDK.Data.SessionTick;
            var CarIdxBestLapNum = racingSDK.Data.CarIdxBestLapNum;
            var CarIdxBestLapTime = racingSDK.Data.CarIdxBestLapTime;
            var CarIdxClassPosition = racingSDK.Data.CarIdxClassPosition;
            var CarIdxEstTime = racingSDK.Data.CarIdxEstTime;
            var CarIdxF2Time = racingSDK.Data.CarIdxF2Time;
            var CarIdxGear = racingSDK.Data.CarIdxGear;
            var CarIdxLap = racingSDK.Data.CarIdxLap;
            var CarIdxLapCompleted = racingSDK.Data.CarIdxLapCompleted;
            var CarIdxLapDistPct = racingSDK.Data.CarIdxLapDistPct;
            var CarIdxLastLapTime = racingSDK.Data.CarIdxLastLapTime;
            var CarIdxOnPitRoad = racingSDK.Data.CarIdxOnPitRoad;
            var CarIdxP2P_Count = racingSDK.Data.CarIdxP2P_Count;
            var CarIdxP2P_Status = racingSDK.Data.CarIdxP2P_Status;
            var CarIdxPosition = racingSDK.Data.CarIdxPosition;
            var CarIdxRPM = racingSDK.Data.CarIdxRPM;
            var CarIdxSteer = racingSDK.Data.CarIdxSteer;
            var CarIdxTrackSurface = racingSDK.Data.CarIdxTrackSurface;
            var CarIdxTrackSurfaceMaterial = racingSDK.Data.CarIdxTrackSurfaceMaterial;

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