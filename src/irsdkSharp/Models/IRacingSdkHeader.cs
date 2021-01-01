using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;

namespace irsdkSharp.Models
{
    public class IRacingSdkHeader
    {
        public IRacingSdkHeader(MemoryMappedViewAccessor mapView)
        {
            var data = new byte[96];
            mapView.ReadArray(0, data, 0, data.Length);
            PopulateHeader(data);
        }

        public IRacingSdkHeader(Span<byte> span)
        {
            PopulateHeader(span);
        }

        public static int GetStatus(MemoryMappedViewAccessor mapView)
        {
            var data = new byte[96];
            mapView.ReadArray(0, data, 0, data.Length);
            return BitConverter.ToInt32(data, 4); 
        }

        private void PopulateHeader(Span<byte> span)
        {
            Version = BitConverter.ToInt32(span.Slice(0, 4));
            Status = BitConverter.ToInt32(span.Slice(4, 4));
            TickRate = BitConverter.ToInt32(span.Slice(8, 4));

            SessionInfoUpdate = BitConverter.ToInt32(span.Slice(12, 4));
            SessionInfoLength = BitConverter.ToInt32(span.Slice(16, 4));
            SessionInfoOffset = BitConverter.ToInt32(span.Slice(20, 4));

            VarCount = BitConverter.ToInt32(span.Slice(24, 4));
            VarHeaderOffset = BitConverter.ToInt32(span.Slice(28, 4));

            BufferCount = BitConverter.ToInt32(span.Slice(32, 4));
            BufferLength = BitConverter.ToInt32(span.Slice(36, 4));

            Buffers = new List<VarBuf>();
            for (var i = 0; i < BufferCount; i++)
            {
                Buffers.Add(new VarBuf(span.Slice(48 + (1 * 16), 16)));
            }
        }

        public int Version { get; private set; }

        public int Status { get; private set; }

        public int TickRate { get; private set; }

        public int SessionInfoUpdate { get; private set; }

        public int SessionInfoLength { get; private set; }

        public int SessionInfoOffset { get; private set; }

        public int VarCount { get; private set; }

        public int VarHeaderOffset { get; private set; }

        public int BufferCount { get; private set; }

        public int BufferLength { get; private set; }

        internal List<VarBuf> Buffers { get; private set; }

        public int Buffer
        {
            get
            {
                return Buffers.OrderByDescending(x => x.TickCount).First().BufOffset;
            }
        }
    }
}
