using System;
using System.Collections.Generic;
using System.Text;

namespace irsdkSharp.Serialization.Models.Session.DriverInfo
{
    public class DriverInfoModel
    {
        public int DriverCarIdx { get; set; }// %d
        public int PaceCarIdx { get; set; }// %d
        public string DriverUserID { get; set; }// %d
        public int DriverIncidentCount { get; set; }// %d
        public float DriverHeadPosX { get; set; }// %.3f
        public float DriverHeadPosY { get; set; }// %.3f
        public float DriverHeadPosZ { get; set; }// %.3f
        public int DriverCarEngCylinderCount { get; set; }
        public string DriverCarVersion { get; set; }// %d
        public float DriverCarIdleRPM { get; set; }// %.3f
        public float DriverCarRedLine { get; set; }// %.3f
        public float DriverCarFuelKgPerLtr { get; set; }// %.3f
        public float DriverCarFuelMaxLtr { get; set; }// %.3f
        public float DriverCarMaxFuelPct { get; set; }// %.3f
        public float DriverCarSLFirstRPM { get; set; }// %.3f
        public float DriverCarSLShiftRPM { get; set; }// %.3f
        public float DriverCarSLLastRPM { get; set; }// %.3f
        public float DriverCarSLBlinkRPM { get; set; }// %.3f
        public float DriverPitTrkPct { get; set; }// %.3f
        public float DriverCarEstLapTime { get; set; }// %.3f
        public string DriverSetupName { get; set; }// %s
        public int DriverSetupIsModified { get; set; }// %d
        public string DriverSetupLoadTypeName { get; set; }// %s
        public int DriverSetupPassedTech { get; set; }// %d

        public int DriverCarGearReverse { get; set; } //%d
        public int DriverCarGearNeutral { get; set; } // %d
        public int DriverCarGearNumForward { get; set; } // %d
        public List<DriverModel> Drivers { get; set; }
    }
}
