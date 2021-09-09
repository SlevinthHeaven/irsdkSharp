using System.Collections.Generic;

namespace irsdkSharp.Models
{
    public class SessionInfoModel
    {
        public int NumSessions { get; set; }
        public List<SessionModel> Sessions { get; set; }
    }
}
