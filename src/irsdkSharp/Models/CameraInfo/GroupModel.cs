using System.Collections.Generic;

namespace irsdkSharp.Models
{
    public class GroupModel
    {
        public int GroupNum { get; set; }// %d
        public string GroupName { get; set; }// %s
        public bool IsScenic { get; set; }// %b
        public List<CameraModel> Cameras { get; set; }
    }
}
