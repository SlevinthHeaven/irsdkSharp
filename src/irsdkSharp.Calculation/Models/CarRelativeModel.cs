using System;

namespace irsdkSharp.Calculation.Models
{
    public class CarRelativeModel
    {
        public int CarIdx { get; set; }
        public bool Selected { get; set; }
        public TimeSpan Ahead { get; set; }
        public TimeSpan Behind { get; set; }
        public int Lap { get; set; }
        public int Position { get; set; }
    }
}