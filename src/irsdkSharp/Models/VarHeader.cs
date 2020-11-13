using irsdkSharp.Enums;

namespace irsdkSharp.Models
{
    public class VarHeader
    {
        public static int Size { get; } = 144;

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
                if (Type == VarType.irChar || Type == VarType.irBool)
                    return 1;
                else if (Type == VarType.irInt || Type == VarType.irBitField || Type == VarType.irFloat)
                    return 4;
                else if (Type == VarType.irDouble)
                    return 8;

                return 0;
            }
        }

        public int Length
        {
            get
            {
                return Bytes * Count;
            }
        }
    }
}
