using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;

namespace irsdkSharp.Models
{
    public class IRacingSdkHeader
    {
        private readonly MemoryMappedViewAccessor _mapView;
        private byte[] latestBufferArray = new byte[16];
        private byte[] bufferArray = new byte[16];

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

        public int Offset
        {
            get
            {
                int maxTickCount = _mapView.ReadInt32(48);
                int curOffset = _mapView.ReadInt32(48 + 4);
                for (var i = 1; i < BufferCount; i++)
                {
                    var curTick = _mapView.ReadInt32(48 + (i * 16));
                    if (maxTickCount < curTick)
                    {
                        maxTickCount = curTick;
                        curOffset = _mapView.ReadInt32(48 + (i * 16) + 4);
                    }
                }
                return curOffset;
            }
        }
    }
}