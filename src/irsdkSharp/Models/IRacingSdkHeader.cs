using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;

namespace irsdkSharp.Models
{
    public class IRacingSdkHeader
    {
        private readonly MemoryMappedViewAccessor _mapView;
        
        private int? _offset;

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

        public int Offset => _offset ??= GetOffset();

        private int GetOffset()
        {
            int maxTickCount = _mapView.ReadInt32(48);
            int curOffset = _mapView.ReadInt32(48 + 4);
                
            for (var i = 1; i < BufferCount; i++)
            {
                var position = 48 + (i * 16);
                
                var curTick = _mapView.ReadInt32(position);
                
                if (maxTickCount < curTick)
                {
                    maxTickCount = curTick;
                    curOffset = _mapView.ReadInt32(position + 4);
                }
            }
                
            return curOffset;
        }

        public Dictionary<string, VarHeader> GetVarHeaders(Encoding encoding)
        {
            var varHeaders = new Dictionary<string, VarHeader>(VarCount);
            
            for (int i = 0; i < VarCount; i++)
            {
                int positionOffset = VarHeaderOffset + (i * VarHeader.Size);
                
                int type = _mapView.ReadInt32(positionOffset);
                int offset = _mapView.ReadInt32(positionOffset + IrSdkConstants.VarOffsetOffset);
                int count = _mapView.ReadInt32(positionOffset + IrSdkConstants.VarCountOffset);
                
                string name = ReadVarHeaderString(positionOffset + IrSdkConstants.VarNameOffset, IrSdkConstants.MaxString, encoding);
                string desc = ReadVarHeaderString(positionOffset + IrSdkConstants.VarDescOffset, IrSdkConstants.MaxDesc, encoding);
                string unit = ReadVarHeaderString(positionOffset + IrSdkConstants.VarUnitOffset, IrSdkConstants.MaxString, encoding);

                varHeaders[name] = new VarHeader(type, offset, count, name, desc, unit);  
            }
            
            return varHeaders;
        }
        
        private string ReadVarHeaderString(int position, int count, Encoding encoding)
        {
            var bytes = new byte[count];
            
            _mapView.ReadArray(position, bytes, 0, count);
            
            return encoding.GetString(bytes).TrimEnd(IrSdkConstants.EndChar);
        }
    }
}