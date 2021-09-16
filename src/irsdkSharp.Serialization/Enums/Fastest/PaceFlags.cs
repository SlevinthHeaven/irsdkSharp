using System;

namespace irsdkSharp.Serialization.Enums.Fastest
{
    [Flags]
    public enum PaceFlags
    {
        EndOfLine = 0x01,
        FreePass = 0x02,
        WavedAround = 0x04
    }
}