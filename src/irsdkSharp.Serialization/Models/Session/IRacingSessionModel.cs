using irsdkSharp.Serialization.Models.Session.CameraInfo;
using irsdkSharp.Serialization.Models.Session.DriverInfo;
using irsdkSharp.Serialization.Models.Session.QualifyResultsInfo;
using irsdkSharp.Serialization.Models.Session.RadioInfo;
using irsdkSharp.Serialization.Models.Session.SessionInfo;
using irsdkSharp.Serialization.Models.Session.SplitTimeInfo;
using irsdkSharp.Serialization.Models.Session.WeekendInfo;
using System;
using System.IO;
using YamlDotNet.Serialization;

namespace irsdkSharp.Serialization.Models.Session
{
    public class IRacingSessionModel
    {
        public static IRacingSessionModel Serialize(string yaml)
        {
            var r = new StringReader(yaml);
            var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
            try
            {
                return deserializer.Deserialize<IRacingSessionModel>(r);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public WeekendInfoModel WeekendInfo { get; set; }
        public SessionInfoModel SessionInfo { get; set; }
        public QualifyResultsInfoModel QualifyResultsInfo { get; set; }
        public CameraInfoModel CameraInfo { get; set; }
        public RadioInfoModel RadioInfo { get; set; }
        public DriverInfoModel DriverInfo { get; set; }
        public SplitTimeInfoModel SplitTimeInfo { get; set; }

    }
}
