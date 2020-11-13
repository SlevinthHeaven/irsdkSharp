using irsdkSharp.Serialization.Models.Data;
using irsdkSharp.Serialization.Models.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace irsdkSharp.Serialization
{
    public class IRacingSDK : irsdkSharp.IRacingSDK
    {
        public IRacingSessionModel GetSessionInformation()
        {
            if (IsInitialized && Header != null)
            {
                byte[] data = new byte[Header.SessionInfoLength];
                FileMapView.ReadArray(Header.SessionInfoOffset, data, 0, Header.SessionInfoLength);

                //Serialise the string into objects, tada!
                return IRacingSessionModel.Serialize(Encoding.Default.GetString(data).TrimEnd(new char[] { '\0' }));
            }
            return null;
        }

        public DataModel GetData()
        {
            return new DataModel();
        }
    }
}
