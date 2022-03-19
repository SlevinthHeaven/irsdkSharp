using irsdkSharp.Models;
using System;
using System.Collections.Generic;

namespace irsdkSharp.Serialization
{
    public static class ValueSerializer
    {
        public static float GetFloatValue(string propertyName, byte[] data, Dictionary<string, VarHeader> headers, int index = 0)
        {
            return headers.TryGetValue(propertyName, out var header)
                ? BitConverter.ToSingle(data, header.Offset + (4 * index))
                : default;
        }

        public static int GetIntValue(string propertyName, byte[] data, Dictionary<string, VarHeader> headers, int index = 0)
        {
            return headers.TryGetValue(propertyName, out var header)
                ? BitConverter.ToInt32(data, header.Offset + (4 * index))
                : default;
        }

        public static double GetDoubleValue(string propertyName, byte[] data, Dictionary<string, VarHeader> headers, int index = 0)
        {
            return headers.TryGetValue(propertyName, out var header)
                ? BitConverter.ToDouble(data, header.Offset + (8 * index))
                : default;
        }

        public static bool GetBoolValue(string propertyName, byte[] data, Dictionary<string, VarHeader> headers, int index = 0)
        {
            return headers.TryGetValue(propertyName, out var header) && BitConverter.ToBoolean(data, header.Offset + index);
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
