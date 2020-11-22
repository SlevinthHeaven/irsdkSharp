namespace irsdkSharp.Serialization.Models.Session.QualifyResultsInfo
{
    public class ResultModel
    {
        public int Position { get; set; }// %d
        public int ClassPosition { get; set; }// %d
        public int CarIdx { get; set; }// %d
        public int FastestLap { get; set; }// %d
        public float FastestTime { get; set; }// %.3f
    }
}
