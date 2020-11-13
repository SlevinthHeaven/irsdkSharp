using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace irsdkSharp.Models
{
    internal class VarBuf
    {
        //VarBuf offsets
        private const int VarBufOffset = 48;
        private const int VarTickCountOffset = 0;
        private const int VarBufOffsetOffset = 4;

        private const int Size = 16;

        private readonly MemoryMappedViewAccessor _fileMapView = null;
        private readonly IRacingSdkHeader _header = null;

        public VarBuf(MemoryMappedViewAccessor mapView, IRacingSdkHeader header)
        {
            _fileMapView = mapView;
            _header = header;
        }

        public int OffsetLatest
        {
            get
            {
                int bufCount = _header.BufferCount;
                int[] ticks = new int[_header.BufferCount];
                for (int i = 0; i < bufCount; i++)
                {
                    ticks[i] = _fileMapView.ReadInt32(VarBufOffset + ((i * Size) + VarTickCountOffset));
                }
                int latestTick = ticks[0];
                int latest = 0;
                for (int i = 0; i < bufCount; i++)
                {
                    if (latestTick < ticks[i])
                    {
                        latest = i;
                    }
                }
                return _fileMapView.ReadInt32(VarBufOffset + ((latest * Size) + VarBufOffsetOffset));
            }
        }
    }
}
