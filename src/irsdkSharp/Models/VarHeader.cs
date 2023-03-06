using irsdkSharp.Enums;

namespace irsdkSharp.Models
{
    public class VarHeader
    {
        public const int Size = 144;

        public VarHeader(int type, int offset, int count, string name, string desc, string unit)
        {
            Type = (VarType)type;
            Offset = offset;
            Count = count;
            Name = name;
            Desc = desc;
            Unit = unit;
        }

        public VarType Type { get; }

        public int Offset { get; }

        public int Count { get; }

        public string Name { get; }

        public string Desc { get; }

        public string Unit { get; }

        public int Bytes
        {
            get
            {
                switch (Type)
                {
                    case VarType.irChar:
                    case VarType.irBool:
                        return 1;
                    
                    case VarType.irInt:
                    case VarType.irBitField:
                    case VarType.irFloat:
                        return 4;
                    
                    case VarType.irDouble:
                        return 8;
                    
                    default:
                        return 0;
                }
            }
        }

        public int Length => Count * Bytes;
    }
}
