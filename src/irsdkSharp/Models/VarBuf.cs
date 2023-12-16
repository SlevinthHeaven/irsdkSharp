using System;

namespace irsdkSharp.Models
{
    internal class VarBuf
    {
        public VarBuf(Span<byte> span)
        {
            TickCount = BitConverter.ToInt32(span[..]);
            BufOffset = BitConverter.ToInt32(span[4..]);
        }
        public int TickCount { get; }
        public int BufOffset { get; }
    }
}
