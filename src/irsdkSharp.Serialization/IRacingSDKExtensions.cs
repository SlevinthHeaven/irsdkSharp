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
                    data[racingSDK.Header.Offset..(racingSDK.Header.Offset + racingSDK.Header.BufferLength)],
                    racingSDK.VarHeaders);
            }
            return null;
        }

        public static List<CarModel> GetPositions(this IRacingSDK racingSDK, out double sessionTime)
        {
            if (racingSDK.IsInitialized && racingSDK.Header != null)
            {
                var length = (int)IRacingSDK.GetFileMapView(racingSDK).Capacity;
                var data = new byte[length];
                IRacingSDK.GetFileMapView(racingSDK).ReadArray(0, data, 0, length);
                //Serialise the string into objects, tada!
                sessionTime = (double)racingSDK.GetData("SessionTime");
                return IRacingDataModel.SerializeCars(
                    data[racingSDK.Header.Offset..(racingSDK.Header.Offset + racingSDK.Header.BufferLength)],
                    racingSDK.VarHeaders);
            }
            sessionTime = 0;
            return null;
        }
    }
}
