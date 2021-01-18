using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;

namespace irsdkSharp.Models
{
    public class IRacingSdkHeader
    {
        private readonly MemoryMappedViewAccessor _mapView;
        public IRacingSdkHeader(MemoryMappedViewAccessor mapView)
        {
            _mapView = mapView;
        }

        public int Version => _mapView.ReadInt32(0);

        public int Status => _mapView.ReadInt32(4);

        public int TickRate => _mapView.ReadInt32(8);

        public int SessionInfoUpdate => _mapView.ReadInt32(12);

        public int SessionInfoLength => _mapView.ReadInt32(16);

        public int SessionInfoOffset => _mapView.ReadInt32(20);

        public int VarCount => _mapView.ReadInt32(24);

        public int VarHeaderOffset => _mapView.ReadInt32(28);

        public int BufferCount => _mapView.ReadInt32(32);

        public int BufferLength => _mapView.ReadInt32(36);

        internal List<VarBuf> Buffers()
        {
            var Buffers = new List<VarBuf>();
            for (var i = 0; i < BufferCount; i++)
            {
                var bufferArray = new byte[16];
                _mapView.ReadArray(48 + (1 * 16), bufferArray, 0, 16);
                Buffers.Add(new VarBuf(bufferArray));
            }
            return Buffers;
        }

        public int Buffer
        {
            get
            {
                return Buffers().OrderByDescending(x => x.TickCount).First().BufOffset;
            }
        }
    }
}
