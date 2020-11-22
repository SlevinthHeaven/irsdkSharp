namespace irsdkSharp.Serialization.Models.Session.SessionInfo
{
    public class PositionModel
    {
        public int Position { get; set; }// %d
        public int ClassPosition { get; set; }// %d
        public int CarIdx { get; set; }// %d
        public int Lap { get; set; }// %d
        public float Time { get; set; }// %.3f
        public int FastestLap { get; set; }// %d
        public float FastestTime { get; set; }// %.3f
        public float LastTime { get; set; }// %.3f
        public int LapsLed { get; set; }// %d
        public int LapsComplete { get; set; }// %d
        public int JokerLapsComplete { get; set; }// %d
        public float LapsDriven { get; set; }// %.3f
        public int Incidents { get; set; }// %d
        public int ReasonOutId { get; set; }// %d
        public string ReasonOutStr { get; set; }// %s
    }
}
