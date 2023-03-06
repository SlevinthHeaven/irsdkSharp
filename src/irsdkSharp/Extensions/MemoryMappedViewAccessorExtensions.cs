using System;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace irsdkSharp.Extensions
{
    public static class MemoryMappedViewAccessorExtensions
    {
        public static string ReadString(this MemoryMappedViewAccessor accessor, int offset, int maxLength, int minLength = 0)
        {
            StringBuilder sb;
            if (minLength > 0)
            {
                sb = new(minLength);
            }
            else
            {
                sb = new();
            }
            char c;
            for (int i = 0; i < maxLength; i++)
            {
                c = (char)accessor.ReadByte(offset + i);
                if (c == '\0')
                {
                    break;
                }
                sb.Append(c);
            }
            return sb.ToString();
        }
        
        public static T[] ReadArray<T>(this MemoryMappedViewAccessor accessor, int position, int count) 
            where T : struct
        {
            var value = new T[count];
            accessor.ReadArray(position, value, 0, count);
            return value;
        }
    }
}
