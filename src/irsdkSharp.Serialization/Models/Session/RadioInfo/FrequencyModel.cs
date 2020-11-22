using System;
using System.Collections.Generic;
using System.Text;

namespace irsdkSharp.Serialization.Models.Session.RadioInfo
{
    public class FrequencyModel
    {
        public int FrequencyNum { get; set; }// %d
        public string FrequencyName { get; set; }// "%s"
        public int Priority { get; set; }// %d
        public int CarIdx { get; set; }// %d
        public int EntryIdx { get; set; }// %d
        public int ClubID { get; set; }//%d
        public int CanScan { get; set; }// %d
        public int CanSquawk { get; set; }// %d
        public int Muted { get; set; }// %d
        public int IsMutable { get; set; }// %d
        public int IsDeletable { get; set; }// %d
    }
}
