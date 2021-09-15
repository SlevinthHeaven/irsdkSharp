using irsdkSharp.Models;
using irsdkSharp.Serialization.Models.Data;
using irsdkSharp.Serialization.Models.Session;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                var fileView = IRacingSDK.GetFileMapView(racingSDK);
                var data = new byte[racingSDK.Header.BufferLength];
                fileView.ReadArray(racingSDK.Header.Offset, data, 0, racingSDK.Header.BufferLength);
                return IRacingDataModel.Serialize(data, racingSDK.VarHeaders);
            }
            return null;
        }

        public static List<CarModel> GetPositions(this IRacingSDK racingSDK, out double sessionTime)
        {
            if (racingSDK.IsInitialized && racingSDK.Header != null)
            {
                var fileView = IRacingSDK.GetFileMapView(racingSDK);
                var data = new byte[racingSDK.Header.BufferLength];
                fileView.ReadArray(racingSDK.Header.Offset, data, 0, racingSDK.Header.BufferLength);
                sessionTime = (double)racingSDK.GetData("SessionTime");
                return IRacingDataModel.SerializeCars(data, racingSDK.VarHeaders);
            }
            sessionTime = 0;
            return null;
        }

    }
}
