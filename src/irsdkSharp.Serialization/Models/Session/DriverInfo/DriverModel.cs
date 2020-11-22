using System;
using System.Collections.Generic;
using System.Text;

namespace irsdkSharp.Serialization.Models.Session.DriverInfo
{
    public class DriverModel
    {
        public int CarIdx { get; set; }// %d
        public string UserName { get; set; }// %s
        public string AbbrevName { get; set; }// %s
        public string Initials { get; set; }// %s
        public int UserID { get; set; }// %d
        public int TeamID { get; set; }// %d
        public string TeamName { get; set; }// %s
        public string CarNumber { get; set; }// "%s"
        public int CarNumberRaw { get; set; }// %d
        public string CarPath { get; set; }// %s
        public int CarClassID { get; set; }// %d
        public int CarID { get; set; }// %d
        public string CarIsPaceCar { get; set; }// %d
        public string CarIsAI { get; set; }// %d
        public string CarClassPowerAdjust { get; set; }// %d
        public string CarClassDryTireSetLimit{ get; set; }// %d
        public int CurDriverIncidentCount { get; set; }// %d
        public int TeamIncidentCount { get; set; }// %d
        public string CarScreenName { get; set; }// %s
        public string CarScreenNameShort { get; set; }// %s
        public string CarClassShortName { get; set; }// %s
        public int CarClassRelSpeed { get; set; }// %d
        public int CarClassLicenseLevel { get; set; }// %d
        public string CarClassMaxFuelPct { get; set; }// %.3f %
        public string CarClassWeightPenalty { get; set; }// %.3f kg
        public string CarClassColor { get; set; }// 0x%02x%02x%02x
        public int IRating { get; set; }// %d
        public int LicLevel { get; set; }// %d
        public int LicSubLevel { get; set; }// %d
        public string LicString { get; set; }// %s
        public string LicColor { get; set; }// 0x%s
        public int IsSpectator { get; set; }// %d
        public string CarDesignStr { get; set; }// %s
        public string HelmetDesignStr { get; set; }// %s
        public string SuitDesignStr { get; set; }// %s
        public string CarNumberDesignStr { get; set; }// %s
        public int CarSponsor_1 { get; set; }// %d
        public int CarSponsor_2 { get; set; }// %d
        public string ClubName { get; set; }// %s
        public string DivisionName { get; set; }// %s
    }
}
