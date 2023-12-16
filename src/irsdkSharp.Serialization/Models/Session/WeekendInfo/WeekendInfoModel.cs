namespace irsdkSharp.Serialization.Models.Session.WeekendInfo
{
    public class WeekendInfoModel
    {
        public string TrackName { get; set; }//: %s
        public int TrackID { get; set; }//: %d
        public string TrackLength { get; set; }//: %0.2f km
        public string TrackLengthOfficial { get; set; }
        public string TrackDisplayName { get; set; }//: %s
        public string TrackDisplayShortName { get; set; }//: %s
		public string TrackConfigName { get; set; }//: %s
		public string TrackCity { get; set; }//: %s
		public string TrackCountry { get; set; }//: %s
		public string TrackAltitude { get; set; }//: %0.2f m
		public string TrackLatitude { get; set; }//: %0.6f m
		public string TrackLongitude { get; set; }//: %0.6f m
		public string TrackNorthOffset { get; set; }//: %0.4f rad
		public int TrackNumTurns { get; set; }//: %d
		public string TrackPitSpeedLimit { get; set; }//: %0.2f kph
		public string TrackType { get; set; }//: %s
		public string TrackDirection { get; set; }//: %s
		public string TrackWeatherType { get; set; }// %s
		public string TrackSkies { get; set; }// %s
		public string TrackSurfaceTemp { get; set; }// %0.2f C
		public string TrackAirTemp { get; set; }// %0.2f C
		public string TrackAirPressure { get; set; }// %0.2f Hg
		public string TrackWindVel { get; set; }// %0.2f m/s
		public string TrackWindDir { get; set; }// %0.2f rad
		public string TrackRelativeHumidity { get; set; }// %d %
		public string TrackFogLevel { get; set; }// %d %
		public int TrackCleanup { get; set; }// %d
		public int TrackDynamicTrack { get; set; }// %d
		public string TrackVersion { get; set; }//: %s
        public int SeriesID { get; set; }// %d
        public int SeasonID { get; set; }// %d
        public int SessionID { get; set; }// %d
        public int SubSessionID { get; set; }// %d
        public int LeagueID { get; set; }// %d
        public int Official { get; set; }// %d
        public int RaceWeek { get; set; }// %d
        public string EventType { get; set; }// %s
        public string Category { get; set; }// %s
        public string SimMode { get; set; }// %s
        public int TeamRacing { get; set; }// %d
        public int MinDrivers { get; set; }// %d
        public int MaxDrivers { get; set; }// %d
        public string DCRuleSet { get; set; }// %s
        public int QualifierMustStartRace { get; set; }// %d
        public int NumCarClasses { get; set; }// %d
        public int NumCarTypes { get; set; }// %d
        public string HeatRacing { get; set; }
        public string BuildType { get; set; }
        public string BuildTarget { get; set; }
        public string BuildVersion { get; set; }
        public WeekendOptionsModel WeekendOptions { get; set; }
        public TelemetryOptionsModel TelemetryOptions { get; set; }
    }
}
