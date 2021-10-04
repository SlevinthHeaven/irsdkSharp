using System;

namespace irsdkSharp.Serialization.Enums.Fastest
{
    public enum PitServiceStatus
    {
        // status
        PitSvNone = 0,
        PitSvInProgress,
        PitSvComplete,

        // errors
        PitSvTooFarLeft = 100,
        PitSvTooFarRight,
        PitSvTooFarForward,
        PitSvTooFarBack,
        PitSvBadAngle,
        PitSvCantFixThat,
    }
}