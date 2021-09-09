using System;
using System.Collections.Generic;
using System.Text;

namespace irsdkSharp.Serialization.Models.Data
{
    public class DataModel
    {
        public DataModel()
        {
            Cars = new CarModel[64] ;
        }
        public CarModel[] Cars { get; set; }
        public float AirDensity { get; set; }
        public float AirPressure { get; set; }
        public float AirTemp { get; set; }
        public float Brake { get; set; }
        public float BrakeRaw { get; set; }
        public int CamCameraNumber { get; set; }
        public int CamCameraState { get; set; }
        public int CamCarIdx { get; set; }
        public int CamGroupNumber { get; set; }
        
        public int CarLeftRight { get; set; }
        public float Clutch { get; set; }
        public float CpuUsageBG { get; set; }
        public int DCDriversSoFar { get; set; }
        public int DCLapStatus { get; set; }
        public bool dcStarter { get; set; }
        public int DisplayUnits { get; set; }
        public float dpFastRepair { get; set; }
        public float dpFuelAddKg { get; set; }
        public float dpFuelFill { get; set; }
        public float dpLFTireChange { get; set; }
        public float dpLFTireColdPress { get; set; }
        public float dpLRTireChange { get; set; }
        public float dpLRTireColdPress { get; set; }
        public float dpRFTireChange { get; set; }
        public float dpRFTireColdPress { get; set; }
        public float dpRRTireChange { get; set; }
        public float dpRRTireColdPress { get; set; }
        public float dpWindshieldTearoff { get; set; }
        public bool DriverMarker { get; set; }
        public int EngineWarnings { get; set; }
        public int EnterExitReset { get; set; }
        public int FastRepairAvailable { get; set; }
        public int FastRepairUsed { get; set; }
        public float FogLevel { get; set; }
        public float FrameRate { get; set; }
        public float FuelLevel { get; set; }
        public float FuelLevelPct { get; set; }
        public float FuelPress { get; set; }
        public float FuelUsePerHour { get; set; }
        public int Gear { get; set; }
        public float HandbrakeRaw { get; set; }
        public bool IsDiskLoggingActive { get; set; }
        public bool IsDiskLoggingEnabled { get; set; }
        public bool IsInGarage { get; set; }
        public bool IsOnTrack { get; set; }
        public bool IsOnTrackCar { get; set; }
        public bool IsReplayPlaying { get; set; }
        public int Lap { get; set; }
        public int LapBestLap { get; set; }
        public float LapBestLapTime { get; set; }
        public int LapBestNLapLap { get; set; }
        public float LapBestNLapTime { get; set; }
        public int LapCompleted { get; set; }
        public float LapCurrentLapTime { get; set; }
        public float LapDeltaToBestLap { get; set; }
        public float LapDeltaToBestLap_DD { get; set; }
        public bool LapDeltaToBestLap_OK { get; set; }
        public float LapDeltaToOptimalLap { get; set; }
        public float LapDeltaToOptimalLap_DD { get; set; }
        public bool LapDeltaToOptimalLap_OK { get; set; }
        public float LapDeltaToSessionBestLap { get; set; }
        public float LapDeltaToSessionBestLap_DD { get; set; }
        public bool LapDeltaToSessionBestLap_OK { get; set; }
        public float LapDeltaToSessionLastlLap { get; set; }
        public float LapDeltaToSessionLastlLap_DD { get; set; }
        public bool LapDeltaToSessionLastlLap_OK { get; set; }
        public float LapDeltaToSessionOptimalLap { get; set; }
        public float LapDeltaToSessionOptimalLap_DD { get; set; }
        public bool LapDeltaToSessionOptimalLap_OK { get; set; }
        public float LapDist { get; set; }
        public float LapDistPct { get; set; }
        public int LapLasNLapSeq { get; set; }
        public float LapLastLapTime { get; set; }
        public float LapLastNLapTime { get; set; }
        public float LatAccel { get; set; }
        public float[] LatAccel_ST { get; set; } = new float[6];
        public float LFbrakeLinePress { get; set; }
        public float LFcoldPressure { get; set; }
        public float LFshockDefl { get; set; }
        public float[] LFshockDefl_ST { get; set; } = new float[6];
        public float LFshockVel { get; set; }
        public float[] LFshockVel_ST { get; set; } = new float[6];
        public float LFtempCL { get; set; }
        public float LFtempCM { get; set; }
        public float LFtempCR { get; set; }
        public float LFwearL { get; set; }
        public float LFwearM { get; set; }
        public float LFwearR { get; set; }
        public bool LoadNumTextures { get; set; }
        public float LongAccel { get; set; }
        public float[] LongAccel_ST { get; set; } = new float[6];
        public float LRbrakeLinePress { get; set; }
        public float LRcoldPressure { get; set; }
        public float LRshockDefl { get; set; }
        public float[] LRshockDefl_ST { get; set; } = new float[6];
        public float LRshockVel { get; set; }
        public float[] LRshockVel_ST { get; set; } = new float[6];
        public float LRtempCL { get; set; }
        public float LRtempCM { get; set; }
        public float LRtempCR { get; set; }
        public float LRwearL { get; set; }
        public float LRwearM { get; set; }
        public float LRwearR { get; set; }
        public float ManifoldPress { get; set; }
        public bool ManualBoost { get; set; }
        public bool ManualNoBoost { get; set; }
        public float OilLevel { get; set; }
        public float OilPress { get; set; }
        public float OilTemp { get; set; }
        public bool OkToReloadTextures { get; set; }
        public bool OnPitRoad { get; set; }
        public float Pitch { get; set; }
        public float PitchRate { get; set; }
        public float[] PitchRate_ST { get; set; } = new float[6];
        public float PitOptRepairLeft { get; set; }
        public float PitRepairLeft { get; set; }
        public bool PitsOpen { get; set; }
        public bool PitstopActive { get; set; }
        public int PitSvFlags { get; set; }
        public float PitSvFuel { get; set; }
        public float PitSvLFP { get; set; }
        public float PitSvLRP { get; set; }
        public float PitSvRFP { get; set; }
        public float PitSvRRP { get; set; }
        public int PlayerCarClassPosition { get; set; }
        public int PlayerCarDriverIncidentCount { get; set; }
        public int PlayerCarIdx { get; set; }
        public bool PlayerCarInPitStall { get; set; }
        public int PlayerCarMyIncidentCount { get; set; }
        public int PlayerCarPitSvStatus { get; set; }
        public int PlayerCarPosition { get; set; }
        public float PlayerCarPowerAdjust { get; set; }
        public int PlayerCarTeamIncidentCount { get; set; }
        public float PlayerCarTowTime { get; set; }
        public float PlayerCarWeightPenalty { get; set; }
        public int PlayerTrackSurface { get; set; }
        public int PlayerTrackSurfaceMaterial { get; set; }
        public bool PushToPass { get; set; }
        public int RaceLaps { get; set; }
        public int RadioTransmitCarIdx { get; set; }
        public int RadioTransmitFrequencyIdx { get; set; }
        public int RadioTransmitRadioIdx { get; set; }
        public float RelativeHumidity { get; set; }
        public int ReplayFrameNum { get; set; }
        public int ReplayFrameNumEnd { get; set; }
        public bool ReplayPlaySlowMotion { get; set; }
        public int ReplayPlaySpeed { get; set; }
        public int ReplaySessionNum { get; set; }
        public double ReplaySessionTime { get; set; }
        public float RFbrakeLinePress { get; set; }
        public float RFcoldPressure { get; set; }
        public float RFshockDefl { get; set; }
        public float[] RFshockDefl_ST { get; set; } = new float[6];
        public float RFshockVel { get; set; }
        public float[] RFshockVel_ST { get; set; } = new float[6];
        public float RFtempCL { get; set; }
        public float RFtempCM { get; set; }
        public float RFtempCR { get; set; }
        public float RFwearL { get; set; }
        public float RFwearM { get; set; }
        public float RFwearR { get; set; }
        public float Roll { get; set; }
        public float RollRate { get; set; }
        public float[] RollRate_ST { get; set; } = new float[6];
        public float RPM { get; set; }
        public float RRbrakeLinePress { get; set; }
        public float RRcoldPressure { get; set; }
        public float RRshockDefl { get; set; }
        public float[] RRshockDefl_ST { get; set; } = new float[6];
        public float RRshockVel { get; set; }
        public float[] RRshockVel_ST { get; set; } = new float[6];
        public float RRtempCL { get; set; }
        public float RRtempCM { get; set; }
        public float RRtempCR { get; set; }
        public float RRwearL { get; set; }
        public float RRwearM { get; set; }
        public float RRwearR { get; set; }
        public int SessionFlags { get; set; }
        public int SessionLapsRemain { get; set; }
        public int SessionLapsRemainEx { get; set; }
        public int SessionNum { get; set; }
        public int SessionState { get; set; }
        public int SessionTick { get; set; }
        public double SessionTime { get; set; }
        public float SessionTimeOfDay { get; set; }
        public double SessionTimeRemain { get; set; }
        public int SessionUniqueID { get; set; }
        public float ShiftGrindRPM { get; set; }
        public float ShiftIndicatorPct { get; set; }
        public float ShiftPowerPct { get; set; }
        public int Skies { get; set; }
        public float Speed { get; set; }
        public float SteeringWheelAngle { get; set; }
        public float SteeringWheelAngleMax { get; set; }
        public float SteeringWheelPctDamper { get; set; }
        public float SteeringWheelPctTorque { get; set; }
        public float SteeringWheelPctTorqueSign { get; set; }
        public float SteeringWheelPctTorqueSignStops { get; set; }
        public float SteeringWheelPeakForceNm { get; set; }
        public float SteeringWheelTorque { get; set; }
        public float[] SteeringWheelTorque_ST { get; set; } = new float[6];
        public float Throttle { get; set; }
        public float ThrottleRaw { get; set; }
        public float TireLF_RumblePitch { get; set; }
        public float TireLR_RumblePitch { get; set; }
        public float TireRF_RumblePitch { get; set; }
        public float TireRR_RumblePitch { get; set; }
        public float TrackTemp { get; set; }
        public float TrackTempCrew { get; set; }
        public float VelocityX { get; set; }
        public float[] VelocityX_ST { get; set; } = new float[6];
        public float VelocityY { get; set; }
        public float[] VelocityY_ST { get; set; } = new float[6];
        public float VelocityZ { get; set; }
        public float[] VelocityZ_ST { get; set; } = new float[6];
        public float VertAccel { get; set; }
        public float[] VertAccel_ST { get; set; } = new float[6];
        public float Voltage { get; set; }
        public float WaterLevel { get; set; }
        public float WaterTemp { get; set; }
        public int WeatherType { get; set; }
        public float WindDir { get; set; }
        public float WindVel { get; set; }
        public float Yaw { get; set; }
        public float YawNorth { get; set; }
        public float YawRate { get; set; }
        public float[] YawRate_ST { get; set; } = new float[6];

        public float SteeringWheelMaxForceNm { get; set; }

        public bool SteeringWheelUseLinear { get; set; }

    }
}
