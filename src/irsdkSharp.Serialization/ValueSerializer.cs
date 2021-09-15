using irsdkSharp.Models;
using System;
using System.Collections.Generic;

namespace irsdkSharp.Serialization
{
    public static class ValueSerializer
    {
        public static float GetFloatValue(string propertyName, byte[] data, Dictionary<string, VarHeader> headers)
        {
            return headers.TryGetValue(propertyName, out var header)
                ? BitConverter.ToSingle(data, header.Offset)
                : default;
        }

        public static int GetIntValue(string propertyName, byte[] data, Dictionary<string, VarHeader> headers)
        {
            return headers.TryGetValue(propertyName, out var header)
                ? BitConverter.ToInt32(data, header.Offset)
                : default;
        }

        public static double GetDoubleValue(string propertyName, byte[] data, Dictionary<string, VarHeader> headers)
        {
            return headers.TryGetValue(propertyName, out var header)
                ? BitConverter.ToDouble(data, header.Offset)
                : default;
        }

        public static bool GetBoolValue(string propertyName, byte[] data, Dictionary<string, VarHeader> headers)
        {
            return headers.TryGetValue(propertyName, out var header)
                ? BitConverter.ToBoolean(data, header.Offset)
                : default;
        }

        public static float[] GetFloatArrayValue(string propertyName, byte[] data, Dictionary<string, VarHeader> headers)
        {
            if (headers.TryGetValue(propertyName, out var header))
            {
                var output = new float[header.Count];
                for (var i = 0; i < header.Count; i++)
                {
                    BitConverter.ToSingle(data, header.Offset + (4 * i));
                }
                return output;
            }
            return default;
        }

    }
}
