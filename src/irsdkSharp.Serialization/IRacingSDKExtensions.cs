using irsdkSharp.Models;
using irsdkSharp.Serialization.Models.Data;
using irsdkSharp.Serialization.Models.Fastest;
using irsdkSharp.Serialization.Models.Session;
using System.Collections.Generic;
using System.Diagnostics;

namespace irsdkSharp.Serialization
{
    public static class IRacingSDKExtensions
    {
        public static Data GetData(this IrSdk irSdk) 
        {
            return new Data(irSdk);
        }

        public static IRacingSessionModel GetSerializedSessionInfo(this IrSdk irSdk)
        {
            var sessionInfo = irSdk.GetSessionInfo();

            if (sessionInfo == null)
            {
                return null;
            }

            return IRacingSessionModel.Serialize(sessionInfo);
        }

        public static IRacingDataModel GetSerializedData(this IrSdk irSdk)
        {
            if (irSdk.IsConnected())
            {
                var data = new byte[irSdk.Header.BufferLength];
                irSdk.FileMapView.ReadArray(irSdk.Header.Offset, data, 0, irSdk.Header.BufferLength);
                return IRacingDataModel.Serialize(data, irSdk.VarHeaders);
            }
            return null;
        }

        public static List<PositionModel> GetPositionsNew(this IrSdk irSdk)
        {
            if (irSdk.IsConnected())
            {
                var data = new Data(irSdk);
                var tick = data.SessionTick;
                var CarIdxBestLapNum = data.CarIdxBestLapNum;
                var CarIdxBestLapTime = data.CarIdxBestLapTime;
                var CarIdxClassPosition = data.CarIdxClassPosition;
                var CarIdxEstTime = data.CarIdxEstTime;
                var CarIdxF2Time = data.CarIdxF2Time;
                var CarIdxGear = data.CarIdxGear;
                var CarIdxLap = data.CarIdxLap;
                var CarIdxLapCompleted = data.CarIdxLapCompleted;
                var CarIdxLapDistPct = data.CarIdxLapDistPct;
                var CarIdxLastLapTime = data.CarIdxLastLapTime;
                var CarIdxOnPitRoad = data.CarIdxOnPitRoad;
                var CarIdxP2P_Count = data.CarIdxP2P_Count;
                var CarIdxP2P_Status = data.CarIdxP2P_Status;
                var CarIdxPosition = data.CarIdxPosition;
                var CarIdxRPM = data.CarIdxRPM;
                var CarIdxSteer = data.CarIdxSteer;
                var CarIdxTrackSurface = data.CarIdxTrackSurface;
                var CarIdxTrackSurfaceMaterial = data.CarIdxTrackSurfaceMaterial;

                var results = new List<PositionModel>();
                for (var i = 0; i < 64; i++)
                {
                    results.Add(new PositionModel
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
            return null;
        }
      
        public static List<CarModel> GetPositions(this IrSdk irSdk, out double sessionTime)
        {
            if (irSdk.IsConnected())
            {
                var data = new byte[irSdk.Header.BufferLength];
                irSdk.FileMapView.ReadArray(irSdk.Header.Offset, data, 0, irSdk.Header.BufferLength);
                sessionTime = (double)irSdk.GetData("SessionTime");
                return IRacingDataModel.SerializeCars(data, irSdk.VarHeaders);
            }
            sessionTime = 0;
            return null;
        }
    }
}
