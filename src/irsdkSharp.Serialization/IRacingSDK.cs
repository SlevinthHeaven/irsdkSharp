using irsdkSharp.Models;
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

        public IRacingDataModel GetData()
        {
            if (IsInitialized)
            {
                var length = (int)FileMapView.Capacity;
                var data = new byte[length];
                FileMapView.ReadArray(0, data, 0, length);

                //Get header
                var header = new IRacingSdkHeader(data);
                var headers = GetVarHeaders(header, data);

                //Serialise the string into objects, tada!
                return IRacingDataModel.Serialize(data[header.Buffer..(header.Buffer + header.BufferLength)], headers);
            }
            return null;
        }

        private List<VarHeader> GetVarHeaders(IRacingSdkHeader header, Span<byte> span)
        {
            var nullChar = new char[] { '\0' };
            var headers = new List<VarHeader>();
            for (int i = 0; i < header.VarCount; i++)
            {
                int type = BitConverter.ToInt32(span.Slice(header.VarHeaderOffset + (i * VarHeader.Size)));
                int offset = BitConverter.ToInt32(span.Slice(header.VarHeaderOffset + (i * VarHeader.Size) + VarOffsetOffset));
                int count = BitConverter.ToInt32(span.Slice(header.VarHeaderOffset + (i * VarHeader.Size) + VarCountOffset));
                string nameStr = Encoding.Default
                    .GetString(span.Slice(header.VarHeaderOffset + (i * VarHeader.Size) + VarNameOffset, Constants.MaxString))
                    .TrimEnd(nullChar);
                string descStr = Encoding.Default
                    .GetString(span.Slice(header.VarHeaderOffset + (i * VarHeader.Size) + VarDescOffset, Constants.MaxDesc))
                    .TrimEnd(nullChar);
                string unitStr = Encoding.Default
                    .GetString(span.Slice(header.VarHeaderOffset + (i * VarHeader.Size) + VarUnitOffset, Constants.MaxString))
                    .TrimEnd(nullChar);
                headers.Add(new VarHeader(type, offset, count, nameStr, descStr, unitStr));
            }
            return headers;
        }
    }

}
