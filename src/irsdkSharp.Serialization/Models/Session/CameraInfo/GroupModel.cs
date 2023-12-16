using System.Collections.Generic;

namespace irsdkSharp.Serialization.Models.Session.CameraInfo
{
    public class GroupModel
    {
        public int GroupNum { get; set; }// %d
        public string GroupName { get; set; }// %s
        public List<CameraModel> Cameras { get; set; }
    }
}
