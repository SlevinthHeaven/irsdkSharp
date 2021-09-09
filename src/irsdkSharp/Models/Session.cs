namespace irsdkSharp.Models
{
    public class Session
    {
        public WeekendInfoModel WeekendInfo { get; set; }
        public SessionInfoModel SessionInfo { get; set; }
        public QualifyResultsInfoModel QualifyResultsInfo { get; set; }
        public CameraInfoModel CameraInfo { get; set; }
        public RadioInfoModel RadioInfo { get; set; }
        public DriverInfoModel DriverInfo { get; set; }
        public SplitTimeInfoModel SplitTimeInfo { get; set; }

        public override string ToString()
        {
	        return $@"Session:
	WeekendInfo: {WeekendInfo?.ToString()}
	SessionInfo: {SessionInfo?.ToString()}
	QualifyResultsInfo: {QualifyResultsInfo?.ToString()}
	CameraInfo: {CameraInfo?.ToString()}
	RadioInfo: {RadioInfo?.ToString()}
	DriverInfo: {DriverInfo?.ToString()}
	SplitTimeInfo: {SplitTimeInfo?.ToString()}";
        }
    }
}