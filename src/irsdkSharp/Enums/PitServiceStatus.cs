using System;

namespace irsdkSharp.Enums
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