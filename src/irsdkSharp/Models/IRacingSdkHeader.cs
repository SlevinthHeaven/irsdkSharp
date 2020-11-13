using System.IO.MemoryMappedFiles;

namespace irsdkSharp.Models
{
    public class IRacingSdkHeader
    {
        //Header offsets
        private const int HVerOffset = 0;
        private const int HStatusOffset = 4;
        private const int HTickRateOffset = 8;
        private const int HSesInfoUpdateOffset = 12;
        private const int HSesInfoLenOffset = 16;
        private const int HSesInfoOffsetOffset = 20;
        private const int HNumVarsOffset = 24;
        private const int HVarHeaderOffsetOffset = 28;
        private const int HNumBufOffset = 32;
        private const int HBufLenOffset = 36;

        private readonly MemoryMappedViewAccessor _fileMapView = null;
        private readonly VarBuf _buffer = null;

        public IRacingSdkHeader(MemoryMappedViewAccessor mapView)
        {
            _fileMapView = mapView;
            _buffer = new VarBuf(mapView, this);
        }

        public int Version
        {
            get { return _fileMapView.ReadInt32(HVerOffset); }
        }

        public int Status
        {
            get { return _fileMapView.ReadInt32(HStatusOffset); }
        }

        public int TickRate
        {
            get { return _fileMapView.ReadInt32(HTickRateOffset); }
        }

        public int SessionInfoUpdate
        {
            get { return _fileMapView.ReadInt32(HSesInfoUpdateOffset); }
        }

        public int SessionInfoLength
        {
            get { return _fileMapView.ReadInt32(HSesInfoLenOffset); }
        }

        public int SessionInfoOffset
        {
            get { return _fileMapView.ReadInt32(HSesInfoOffsetOffset); }
        }

        public int VarCount
        {
            get { return _fileMapView.ReadInt32(HNumVarsOffset); }
        }

        public int VarHeaderOffset
        {
            get { return _fileMapView.ReadInt32(HVarHeaderOffsetOffset); }
        }

        public int BufferCount
        {
            get { return _fileMapView.ReadInt32(HNumBufOffset); }
        }

        public int BufferLength
        {
            get { return _fileMapView.ReadInt32(HBufLenOffset); }
        }

        public int Buffer
        {
            get
            {
                return _buffer.OffsetLatest;
            }
        }
    }
}
