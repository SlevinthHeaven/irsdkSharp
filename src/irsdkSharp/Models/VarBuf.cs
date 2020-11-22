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

        //public int OffsetLatest
        //{
        //    get
        //    {
        //        int bufCount = _header.BufferCount;
        //        int[] ticks = new int[_header.BufferCount];
        //        for (int i = 0; i < bufCount; i++)
        //        {
        //            ticks[i] = _fileMapView.ReadInt32(VarBufOffset + ((i * Size) + VarTickCountOffset));
        //        }
        //        int latestTick = ticks[0];
        //        int latest = 0;
        //        for (int i = 0; i < bufCount; i++)
        //        {
        //            if (latestTick < ticks[i])
        //            {
        //                latest = i;
        //            }
        //        }
        //        return _fileMapView.ReadInt32(VarBufOffset + ((latest * Size) + VarBufOffsetOffset));
        //    }
        //}
    }
}
