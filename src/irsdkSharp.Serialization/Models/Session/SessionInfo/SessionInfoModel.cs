using System.Collections.Generic;

namespace irsdkSharp.Serialization.Models.Session.SessionInfo
{
    public class SessionInfoModel
    {
        public int NumSessions { get; set; }
        public List<SessionModel> Sessions { get; set; }
    }
}
