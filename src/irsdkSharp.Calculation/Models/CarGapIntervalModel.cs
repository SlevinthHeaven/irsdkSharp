using System;

namespace irsdkSharp.Calculation.Models
{
    public class CarGapIntervalModel
    {
        public int CarIdx { get; set; }
        public TimeSpan Gap { get; set; }
        public int GapLapDifference { get; set; }
        public TimeSpan ClassGap { get; set; }
        public int ClassGapLapDifference { get; set; }
        public TimeSpan Interval { get; set; }
        public int IntervalLapDifference { get; set; }
        public TimeSpan ClassInterval { get; set; }
        public int ClassIntervalLapDifference { get; set; }
    }
}