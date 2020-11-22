using System;
using System.Collections.Generic;
using System.Text;

namespace irsdkSharp.Serialization.Models.Session.RadioInfo
{
    public class RadioInfoModel
    {
        public int SelectedRadioNum { get; set; }
        public List<RadioModel> Radios { get; set; }
    }
}
