using irsdkSharp.Models;
using irsdkSharp.Serialization.Models.Data;
using irsdkSharp.Serialization.Models.Session;
using System;
using System.Collections.Generic;
using System.Text;

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
                    data[racingSDK.Header.Buffer..(racingSDK.Header.Buffer + racingSDK.Header.BufferLength)],
                    racingSDK.VarHeaders);
            }
            return null;
        }

        public static List<CarModel> GetPositions(this IRacingSDK racingSDK)
        {
            var tick = (double)racingSDK.GetData("SessionTime");
            //var CarIdx = (int[])racingSDK.GetData("CarIdx");
            var CarIdxBestLapNum = (int[])racingSDK.GetData("CarIdxBestLapNum");
            var CarIdxBestLapTime = (float[])racingSDK.GetData("CarIdxBestLapTime");
            var CarIdxClassPosition = (int[])racingSDK.GetData("CarIdxClassPosition");
            var CarIdxEstTime = (float[])racingSDK.GetData("CarIdxEstTime");
            var CarIdxF2Time = (float[])racingSDK.GetData("CarIdxF2Time");
            var CarIdxGear = (int[])racingSDK.GetData("CarIdxGear");
            var CarIdxLap = (int[])racingSDK.GetData("CarIdxLap");
            var CarIdxLapCompleted = (int[])racingSDK.GetData("CarIdxLapCompleted");
            var CarIdxLapDistPct = (float[])racingSDK.GetData("CarIdxLapDistPct");
            var CarIdxLastLapTime = (float[])racingSDK.GetData("CarIdxLastLapTime");
            var CarIdxOnPitRoad = (bool[])racingSDK.GetData("CarIdxOnPitRoad");
            var CarIdxP2P_Count = (int[])racingSDK.GetData("CarIdxP2P_Count");
            var CarIdxP2P_Status = (bool[])racingSDK.GetData("CarIdxP2P_Status");
            var CarIdxPosition = (int[])racingSDK.GetData("CarIdxPosition");
            var CarIdxRPM = (float[])racingSDK.GetData("CarIdxRPM");
            var CarIdxSteer = (float[])racingSDK.GetData("CarIdxSteer");
            var CarIdxTrackSurface = (int[])racingSDK.GetData("CarIdxTrackSurface");
            var CarIdxTrackSurfaceMaterial = (int[])racingSDK.GetData("CarIdxTrackSurfaceMaterial");

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
