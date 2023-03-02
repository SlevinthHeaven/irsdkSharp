using System.IO.MemoryMappedFiles;
using System.Text;

namespace irsdkSharp.Extensions
{
    internal static class MemoryMappedViewAccessorExtensions
    {
        public static string ReadString(this MemoryMappedViewAccessor accessor, int offset, int maxLength, int minLength = 0)
        {
            StringBuilder sb = (minLength > 0) ? new(minLength) : new();

            for (var i = 0; i < maxLength; i++)
            {
                var c = (char)accessor.ReadByte(offset + i);
                
                if (c == IrSdkConstants.EndChar)
                    break;
                
                sb.Append(c);
            }
            
            return sb.ToString();
        }
    }
}
