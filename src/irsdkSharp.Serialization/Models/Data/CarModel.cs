using System;
using System.Collections.Generic;
using System.Text;

namespace irsdkSharp.Serialization.Models.Data
{
    public class CarModel
    {
        public int CarIdx { get; set; }
        public int CarIdxBestLapNum { get; set; }
        public float CarIdxBestLapTime { get; set; }
        public int CarIdxClassPosition { get; set; }
        public float CarIdxEstTime { get; set; }
        public float CarIdxF2Time { get; set; }
        public int CarIdxGear { get; set; }
        public int CarIdxLap { get; set; }
        public int CarIdxLapCompleted { get; set; }
        public float CarIdxLapDistPct { get; set; }
        public float CarIdxLastLapTime { get; set; }
        public bool CarIdxOnPitRoad { get; set; }
        public int CarIdxP2P_Count { get; set; }
        public bool CarIdxP2P_Status { get; set; }
        public int CarIdxPosition { get; set; }
        public float CarIdxRPM { get; set; }
        public float CarIdxSteer { get; set; }
        public int CarIdxTrackSurface { get; set; }
        public int CarIdxTrackSurfaceMaterial { get; set; }
    }
}
