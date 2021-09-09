using System;
using System.Collections.Generic;
using System.Text;

namespace irsdkSharp.Models
{
    public class RadioModel
    {
        public int RadioNum { get; set; }// %d
        public int HopCount { get; set; }// %d
        public int NumFrequencies { get; set; }// %d
        public int TunedToFrequencyNum { get; set; }// %d
        public int ScanningIsOn { get; set; }// %d
        public List<FrequencyModel> Frequencies { get; set; }
    }
}
