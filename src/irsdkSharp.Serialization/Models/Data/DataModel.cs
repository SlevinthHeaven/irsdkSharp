using irsdkSharp.Models;
using System;
using System.Collections.Generic;

namespace irsdkSharp.Serialization.Models.Data
{
    public class DataModel
    {
        private readonly byte[] _data;
        private readonly Dictionary<string, VarHeader> _headers;

        public DataModel(byte[] data, Dictionary<string, VarHeader> headers)
        {
            _data = data;
            _headers = headers;

            Cars = new CarModel[64];
            for (var i = 0; i < Cars.Length; i++)
            {
                Cars[i] = new CarModel(i, data, headers);
            }
        }

        public CarModel[] Cars { get; set; }

        public float AirDensity => ValueSerializer.GetFloatValue(nameof(AirDensity), _data, _headers);

        public float AirPressure => ValueSerializer.GetFloatValue(nameof(AirPressure), _data, _headers);

        public float AirTemp => ValueSerializer.GetFloatValue(nameof(AirTemp), _data, _headers);

        public float Brake => ValueSerializer.GetFloatValue(nameof(Brake), _data, _headers);

        public float BrakeRaw => ValueSerializer.GetFloatValue(nameof(BrakeRaw), _data, _headers);

        public int CamCameraNumber => ValueSerializer.GetIntValue(nameof(CamCameraNumber), _data, _headers);

        public int CamCameraState => ValueSerializer.GetIntValue(nameof(CamCameraState), _data, _headers);

        public int CamCarIdx => ValueSerializer.GetIntValue(nameof(CamCarIdx), _data, _headers);

        public int CamGroupNumber => ValueSerializer.GetIntValue(nameof(CamGroupNumber), _data, _headers);

        public int CarLeftRight => ValueSerializer.GetIntValue(nameof(CarLeftRight), _data, _headers);

        public float Clutch => ValueSerializer.GetFloatValue(nameof(Clutch), _data, _headers);

        public float CpuUsageBG => ValueSerializer.GetFloatValue(nameof(CpuUsageBG), _data, _headers);

        public int DCDriversSoFar => ValueSerializer.GetIntValue(nameof(DCDriversSoFar), _data, _headers);

        public int DCLapStatus => ValueSerializer.GetIntValue(nameof(DCLapStatus), _data, _headers);

        public bool dcStarter => ValueSerializer.GetBoolValue(nameof(dcStarter), _data, _headers);

        public int DisplayUnits => ValueSerializer.GetIntValue(nameof(DisplayUnits), _data, _headers);

        public float dpFastRepair => ValueSerializer.GetFloatValue(nameof(dpFastRepair), _data, _headers);

        public float dpFuelAddKg => ValueSerializer.GetFloatValue(nameof(dpFuelAddKg), _data, _headers);

        public float dpFuelFill => ValueSerializer.GetFloatValue(nameof(dpFuelFill), _data, _headers);

        public float dpLFTireChange => ValueSerializer.GetFloatValue(nameof(dpLFTireChange), _data, _headers);

        public float dpLFTireColdPress => ValueSerializer.GetFloatValue(nameof(dpLFTireColdPress), _data, _headers);

        public float dpLRTireChange => ValueSerializer.GetFloatValue(nameof(dpLRTireChange), _data, _headers);

        public float dpLRTireColdPress => ValueSerializer.GetFloatValue(nameof(dpLRTireColdPress), _data, _headers);

        public float dpRFTireChange => ValueSerializer.GetFloatValue(nameof(dpRFTireChange), _data, _headers);

        public float dpRFTireColdPress => ValueSerializer.GetFloatValue(nameof(dpRFTireColdPress), _data, _headers);

        public float dpRRTireChange => ValueSerializer.GetFloatValue(nameof(dpRRTireChange), _data, _headers);

        public float dpRRTireColdPress => ValueSerializer.GetFloatValue(nameof(dpRRTireColdPress), _data, _headers);

        public float dpWindshieldTearoff => ValueSerializer.GetFloatValue(nameof(dpWindshieldTearoff), _data, _headers);

        public bool DriverMarker => ValueSerializer.GetBoolValue(nameof(DriverMarker), _data, _headers);

        public int EngineWarnings => ValueSerializer.GetIntValue(nameof(EngineWarnings), _data, _headers);

        public int EnterExitReset => ValueSerializer.GetIntValue(nameof(EnterExitReset), _data, _headers);

        public int FastRepairAvailable => ValueSerializer.GetIntValue(nameof(FastRepairAvailable), _data, _headers);

        public int FastRepairUsed => ValueSerializer.GetIntValue(nameof(FastRepairUsed), _data, _headers);

        public float FogLevel => ValueSerializer.GetFloatValue(nameof(FogLevel), _data, _headers);

        public float FrameRate => ValueSerializer.GetFloatValue(nameof(FrameRate), _data, _headers);

        public float FuelLevel => ValueSerializer.GetFloatValue(nameof(FuelLevel), _data, _headers);

        public float FuelLevelPct => ValueSerializer.GetFloatValue(nameof(FuelLevelPct), _data, _headers);

        public float FuelPress => ValueSerializer.GetFloatValue(nameof(FuelPress), _data, _headers);

        public float FuelUsePerHour => ValueSerializer.GetFloatValue(nameof(FuelUsePerHour), _data, _headers);

        public int Gear => ValueSerializer.GetIntValue(nameof(Gear), _data, _headers);

        public float HandbrakeRaw => ValueSerializer.GetFloatValue(nameof(HandbrakeRaw), _data, _headers);

        public bool IsDiskLoggingActive => ValueSerializer.GetBoolValue(nameof(IsDiskLoggingActive), _data, _headers);

        public bool IsDiskLoggingEnabled => ValueSerializer.GetBoolValue(nameof(IsDiskLoggingEnabled), _data, _headers);

        public bool IsInGarage => ValueSerializer.GetBoolValue(nameof(IsInGarage), _data, _headers);

        public bool IsOnTrack => ValueSerializer.GetBoolValue(nameof(IsOnTrack), _data, _headers);

        public bool IsOnTrackCar => ValueSerializer.GetBoolValue(nameof(IsOnTrackCar), _data, _headers);

        public bool IsReplayPlaying => ValueSerializer.GetBoolValue(nameof(IsReplayPlaying), _data, _headers);

        public int Lap => ValueSerializer.GetIntValue(nameof(Lap), _data, _headers);

        public int LapBestLap => ValueSerializer.GetIntValue(nameof(LapBestLap), _data, _headers);

        public float LapBestLapTime => ValueSerializer.GetFloatValue(nameof(LapBestLapTime), _data, _headers);

        public int LapBestNLapLap => ValueSerializer.GetIntValue(nameof(LapBestNLapLap), _data, _headers);

        public float LapBestNLapTime => ValueSerializer.GetFloatValue(nameof(LapBestNLapTime), _data, _headers);

        public int LapCompleted => ValueSerializer.GetIntValue(nameof(LapCompleted), _data, _headers);

        public float LapCurrentLapTime => ValueSerializer.GetFloatValue(nameof(LapCurrentLapTime), _data, _headers);

        public float LapDeltaToBestLap => ValueSerializer.GetFloatValue(nameof(LapDeltaToBestLap), _data, _headers);

        public float LapDeltaToBestLap_DD => ValueSerializer.GetFloatValue(nameof(LapDeltaToBestLap_DD), _data, _headers);

        public bool LapDeltaToBestLap_OK => ValueSerializer.GetBoolValue(nameof(LapDeltaToBestLap_OK), _data, _headers);

        public float LapDeltaToOptimalLap => ValueSerializer.GetFloatValue(nameof(LapDeltaToOptimalLap), _data, _headers);

        public float LapDeltaToOptimalLap_DD => ValueSerializer.GetFloatValue(nameof(LapDeltaToOptimalLap_DD), _data, _headers);

        public bool LapDeltaToOptimalLap_OK => ValueSerializer.GetBoolValue(nameof(LapDeltaToOptimalLap_OK), _data, _headers);

        public float LapDeltaToSessionBestLap => ValueSerializer.GetFloatValue(nameof(LapDeltaToSessionBestLap), _data, _headers);

        public float LapDeltaToSessionBestLap_DD => ValueSerializer.GetFloatValue(nameof(LapDeltaToSessionBestLap_DD), _data, _headers);

        public bool LapDeltaToSessionBestLap_OK => ValueSerializer.GetBoolValue(nameof(LapDeltaToSessionBestLap_OK), _data, _headers);

        public float LapDeltaToSessionLastlLap => ValueSerializer.GetFloatValue(nameof(LapDeltaToSessionLastlLap), _data, _headers);

        public float LapDeltaToSessionLastlLap_DD => ValueSerializer.GetFloatValue(nameof(LapDeltaToSessionLastlLap_DD), _data, _headers);

        public bool LapDeltaToSessionLastlLap_OK => ValueSerializer.GetBoolValue(nameof(LapDeltaToSessionLastlLap_OK), _data, _headers);

        public float LapDeltaToSessionOptimalLap => ValueSerializer.GetFloatValue(nameof(LapDeltaToSessionOptimalLap), _data, _headers);

        public float LapDeltaToSessionOptimalLap_DD => ValueSerializer.GetFloatValue(nameof(LapDeltaToSessionOptimalLap_DD), _data, _headers);

        public bool LapDeltaToSessionOptimalLap_OK => ValueSerializer.GetBoolValue(nameof(LapDeltaToSessionOptimalLap_OK), _data, _headers);

        public float LapDist => ValueSerializer.GetFloatValue(nameof(LapDist), _data, _headers);

        public float LapDistPct => ValueSerializer.GetFloatValue(nameof(LapDistPct), _data, _headers);

        public int LapLasNLapSeq => ValueSerializer.GetIntValue(nameof(LapLasNLapSeq), _data, _headers);

        public float LapLastLapTime => ValueSerializer.GetFloatValue(nameof(LapLastLapTime), _data, _headers);

        public float LapLastNLapTime => ValueSerializer.GetFloatValue(nameof(LapLastNLapTime), _data, _headers);

        public float LatAccel => ValueSerializer.GetFloatValue(nameof(LatAccel), _data, _headers); 

        public float[] LatAccel_ST => ValueSerializer.GetFloatArrayValue(nameof(LatAccel_ST), _data, _headers);

        public float LFbrakeLinePress => ValueSerializer.GetFloatValue(nameof(LFbrakeLinePress), _data, _headers);

        public float LFcoldPressure => ValueSerializer.GetFloatValue(nameof(LFcoldPressure), _data, _headers);

        public float LFshockDefl => ValueSerializer.GetFloatValue(nameof(LFshockDefl), _data, _headers);

        public float[] LFshockDefl_ST => ValueSerializer.GetFloatArrayValue(nameof(LFshockDefl_ST), _data, _headers);

        public float LFshockVel => ValueSerializer.GetFloatValue(nameof(LFshockVel), _data, _headers);

        public float[] LFshockVel_ST => ValueSerializer.GetFloatArrayValue(nameof(LFshockVel_ST), _data, _headers);

        public float LFtempCL => ValueSerializer.GetFloatValue(nameof(LFtempCL), _data, _headers);

        public float LFtempCM => ValueSerializer.GetFloatValue(nameof(LFtempCM), _data, _headers);

        public float LFtempCR => ValueSerializer.GetFloatValue(nameof(LFtempCR), _data, _headers);

        public float LFwearL => ValueSerializer.GetFloatValue(nameof(LFwearL), _data, _headers);

        public float LFwearM => ValueSerializer.GetFloatValue(nameof(LFwearM), _data, _headers);

        public float LFwearR => ValueSerializer.GetFloatValue(nameof(LFwearR), _data, _headers);

        public bool LoadNumTextures => ValueSerializer.GetBoolValue(nameof(LoadNumTextures), _data, _headers);

        public float LongAccel => ValueSerializer.GetFloatValue(nameof(LongAccel), _data, _headers);

        public float[] LongAccel_ST => ValueSerializer.GetFloatArrayValue(nameof(LongAccel_ST), _data, _headers);

        public float LRbrakeLinePress => ValueSerializer.GetFloatValue(nameof(LRbrakeLinePress), _data, _headers);

        public float LRcoldPressure => ValueSerializer.GetFloatValue(nameof(LRcoldPressure), _data, _headers);

        public float LRshockDefl => ValueSerializer.GetFloatValue(nameof(LRshockDefl), _data, _headers);

        public float[] LRshockDefl_ST => ValueSerializer.GetFloatArrayValue(nameof(LRshockDefl_ST), _data, _headers);

        public float LRshockVel => ValueSerializer.GetFloatValue(nameof(LRshockVel), _data, _headers);

        public float[] LRshockVel_ST => ValueSerializer.GetFloatArrayValue(nameof(LRshockVel_ST), _data, _headers);

        public float LRtempCL => ValueSerializer.GetFloatValue(nameof(LRtempCL), _data, _headers);

        public float LRtempCM => ValueSerializer.GetFloatValue(nameof(LRtempCM), _data, _headers);

        public float LRtempCR => ValueSerializer.GetFloatValue(nameof(LRtempCR), _data, _headers);

        public float LRwearL => ValueSerializer.GetFloatValue(nameof(LRwearL), _data, _headers);

        public float LRwearM => ValueSerializer.GetFloatValue(nameof(LRwearM), _data, _headers);

        public float LRwearR => ValueSerializer.GetFloatValue(nameof(LRwearR), _data, _headers);

        public float ManifoldPress => ValueSerializer.GetFloatValue(nameof(ManifoldPress), _data, _headers);

        public bool ManualBoost => ValueSerializer.GetBoolValue(nameof(ManualBoost), _data, _headers);

        public bool ManualNoBoost => ValueSerializer.GetBoolValue(nameof(ManualNoBoost), _data, _headers);

        public float OilLevel => ValueSerializer.GetFloatValue(nameof(OilLevel), _data, _headers);

        public float OilPress => ValueSerializer.GetFloatValue(nameof(OilPress), _data, _headers);

        public float OilTemp => ValueSerializer.GetFloatValue(nameof(OilTemp), _data, _headers);

        public bool OkToReloadTextures => ValueSerializer.GetBoolValue(nameof(OkToReloadTextures), _data, _headers);

        public bool OnPitRoad => ValueSerializer.GetBoolValue(nameof(OnPitRoad), _data, _headers);

        public float Pitch => ValueSerializer.GetFloatValue(nameof(Pitch), _data, _headers);

        public float PitchRate => ValueSerializer.GetFloatValue(nameof(PitchRate), _data, _headers);

        public float[] PitchRate_ST => ValueSerializer.GetFloatArrayValue(nameof(PitchRate_ST), _data, _headers);

        public float PitOptRepairLeft => ValueSerializer.GetFloatValue(nameof(PitOptRepairLeft), _data, _headers);

        public float PitRepairLeft => ValueSerializer.GetFloatValue(nameof(PitRepairLeft), _data, _headers);

        public bool PitsOpen => ValueSerializer.GetBoolValue(nameof(PitsOpen), _data, _headers);

        public bool PitstopActive => ValueSerializer.GetBoolValue(nameof(PitstopActive), _data, _headers);

        public int PitSvFlags => ValueSerializer.GetIntValue(nameof(PitSvFlags), _data, _headers);

        public float PitSvFuel => ValueSerializer.GetFloatValue(nameof(PitSvFuel), _data, _headers);

        public float PitSvLFP => ValueSerializer.GetFloatValue(nameof(PitSvLFP), _data, _headers);

        public float PitSvLRP => ValueSerializer.GetFloatValue(nameof(PitSvLRP), _data, _headers);

        public float PitSvRFP => ValueSerializer.GetFloatValue(nameof(PitSvRFP), _data, _headers);

        public float PitSvRRP => ValueSerializer.GetFloatValue(nameof(PitSvRRP), _data, _headers);

        public int PlayerCarClassPosition => ValueSerializer.GetIntValue(nameof(PlayerCarClassPosition), _data, _headers);

        public int PlayerCarDriverIncidentCount => ValueSerializer.GetIntValue(nameof(PlayerCarDriverIncidentCount), _data, _headers);

        public int PlayerCarIdx => ValueSerializer.GetIntValue(nameof(PlayerCarIdx), _data, _headers);

        public bool PlayerCarInPitStall => ValueSerializer.GetBoolValue(nameof(PlayerCarInPitStall), _data, _headers);

        public int PlayerCarMyIncidentCount => ValueSerializer.GetIntValue(nameof(PlayerCarMyIncidentCount), _data, _headers);

        public int PlayerCarPitSvStatus => ValueSerializer.GetIntValue(nameof(PlayerCarPitSvStatus), _data, _headers);

        public int PlayerCarPosition => ValueSerializer.GetIntValue(nameof(PlayerCarPosition), _data, _headers);

        public float PlayerCarPowerAdjust => ValueSerializer.GetFloatValue(nameof(PlayerCarPowerAdjust), _data, _headers);

        public int PlayerCarTeamIncidentCount => ValueSerializer.GetIntValue(nameof(PlayerCarTeamIncidentCount), _data, _headers);

        public float PlayerCarTowTime => ValueSerializer.GetFloatValue(nameof(PlayerCarTowTime), _data, _headers);

        public float PlayerCarWeightPenalty => ValueSerializer.GetFloatValue(nameof(PlayerCarWeightPenalty), _data, _headers);

        public int PlayerTrackSurface => ValueSerializer.GetIntValue(nameof(PlayerTrackSurface), _data, _headers);

        public int PlayerTrackSurfaceMaterial => ValueSerializer.GetIntValue(nameof(PlayerTrackSurfaceMaterial), _data, _headers);

        public bool PushToPass => ValueSerializer.GetBoolValue(nameof(PushToPass), _data, _headers);

        public int RaceLaps => ValueSerializer.GetIntValue(nameof(RaceLaps), _data, _headers);

        public int RadioTransmitCarIdx => ValueSerializer.GetIntValue(nameof(RadioTransmitCarIdx), _data, _headers);

        public int RadioTransmitFrequencyIdx => ValueSerializer.GetIntValue(nameof(RadioTransmitFrequencyIdx), _data, _headers);

        public int RadioTransmitRadioIdx => ValueSerializer.GetIntValue(nameof(RadioTransmitRadioIdx), _data, _headers);

        public float RelativeHumidity => ValueSerializer.GetFloatValue(nameof(RelativeHumidity), _data, _headers);

        public int ReplayFrameNum => ValueSerializer.GetIntValue(nameof(ReplayFrameNum), _data, _headers);

        public int ReplayFrameNumEnd => ValueSerializer.GetIntValue(nameof(ReplayFrameNumEnd), _data, _headers);

        public bool ReplayPlaySlowMotion => ValueSerializer.GetBoolValue(nameof(ReplayPlaySlowMotion), _data, _headers);

        public int ReplayPlaySpeed => ValueSerializer.GetIntValue(nameof(ReplayPlaySpeed), _data, _headers);

        public int ReplaySessionNum => ValueSerializer.GetIntValue(nameof(ReplaySessionNum), _data, _headers);

        public double ReplaySessionTime => ValueSerializer.GetFloatValue(nameof(ReplaySessionTime), _data, _headers);

        public float RFbrakeLinePress => ValueSerializer.GetFloatValue(nameof(RFbrakeLinePress), _data, _headers);

        public float RFcoldPressure => ValueSerializer.GetFloatValue(nameof(RFcoldPressure), _data, _headers);

        public float RFshockDefl => ValueSerializer.GetFloatValue(nameof(RFshockDefl), _data, _headers);

        public float[] RFshockDefl_ST => ValueSerializer.GetFloatArrayValue(nameof(RFshockDefl_ST), _data, _headers);

        public float RFshockVel => ValueSerializer.GetFloatValue(nameof(RFshockVel), _data, _headers);

        public float[] RFshockVel_ST => ValueSerializer.GetFloatArrayValue(nameof(RFshockVel_ST), _data, _headers);

        public float RFtempCL => ValueSerializer.GetFloatValue(nameof(RFtempCL), _data, _headers);

        public float RFtempCM => ValueSerializer.GetFloatValue(nameof(RFtempCM), _data, _headers);

        public float RFtempCR => ValueSerializer.GetFloatValue(nameof(RFtempCR), _data, _headers);

        public float RFwearL => ValueSerializer.GetFloatValue(nameof(RFwearL), _data, _headers);

        public float RFwearM => ValueSerializer.GetFloatValue(nameof(RFwearM), _data, _headers);

        public float RFwearR => ValueSerializer.GetFloatValue(nameof(RFwearR), _data, _headers);

        public float Roll => ValueSerializer.GetFloatValue(nameof(Roll), _data, _headers);

        public float RollRate => ValueSerializer.GetFloatValue(nameof(RollRate), _data, _headers);

        public float[] RollRate_ST => ValueSerializer.GetFloatArrayValue(nameof(RollRate_ST), _data, _headers);

        public float RPM => ValueSerializer.GetFloatValue(nameof(RPM), _data, _headers);

        public float RRbrakeLinePress => ValueSerializer.GetFloatValue(nameof(RRbrakeLinePress), _data, _headers);

        public float RRcoldPressure => ValueSerializer.GetFloatValue(nameof(RRcoldPressure), _data, _headers);

        public float RRshockDefl => ValueSerializer.GetFloatValue(nameof(RRshockDefl), _data, _headers);

        public float[] RRshockDefl_ST => ValueSerializer.GetFloatArrayValue(nameof(RRshockDefl_ST), _data, _headers);

        public float RRshockVel => ValueSerializer.GetFloatValue(nameof(RRshockVel), _data, _headers);

        public float[] RRshockVel_ST => ValueSerializer.GetFloatArrayValue(nameof(RRshockVel_ST), _data, _headers);

        public float RRtempCL => ValueSerializer.GetFloatValue(nameof(RRtempCL), _data, _headers);

        public float RRtempCM => ValueSerializer.GetFloatValue(nameof(RRtempCM), _data, _headers);

        public float RRtempCR => ValueSerializer.GetFloatValue(nameof(RRtempCR), _data, _headers);

        public float RRwearL => ValueSerializer.GetFloatValue(nameof(RRwearL), _data, _headers);

        public float RRwearM => ValueSerializer.GetFloatValue(nameof(RRwearM), _data, _headers);

        public float RRwearR => ValueSerializer.GetFloatValue(nameof(RRwearR), _data, _headers);

        public int SessionFlags => ValueSerializer.GetIntValue(nameof(SessionFlags), _data, _headers);

        public int SessionLapsRemain => ValueSerializer.GetIntValue(nameof(SessionLapsRemain), _data, _headers);

        public int SessionLapsRemainEx => ValueSerializer.GetIntValue(nameof(SessionLapsRemainEx), _data, _headers);

        public int SessionNum => ValueSerializer.GetIntValue(nameof(SessionNum), _data, _headers);

        public int SessionState => ValueSerializer.GetIntValue(nameof(SessionState), _data, _headers);

        public int SessionTick => ValueSerializer.GetIntValue(nameof(SessionTick), _data, _headers);

        public double SessionTime => ValueSerializer.GetFloatValue(nameof(SessionTime), _data, _headers);

        public float SessionTimeOfDay => ValueSerializer.GetFloatValue(nameof(SessionTimeOfDay), _data, _headers);

        public double SessionTimeRemain => ValueSerializer.GetFloatValue(nameof(SessionTimeRemain), _data, _headers);

        public int SessionUniqueID => ValueSerializer.GetIntValue(nameof(SessionUniqueID), _data, _headers);

        public float ShiftGrindRPM => ValueSerializer.GetFloatValue(nameof(ShiftGrindRPM), _data, _headers);

        public float ShiftIndicatorPct => ValueSerializer.GetFloatValue(nameof(ShiftIndicatorPct), _data, _headers);

        public float ShiftPowerPct => ValueSerializer.GetFloatValue(nameof(ShiftPowerPct), _data, _headers);

        public int Skies => ValueSerializer.GetIntValue(nameof(Skies), _data, _headers);

        public float Speed => ValueSerializer.GetFloatValue(nameof(Speed), _data, _headers);

        public float SteeringWheelAngle => ValueSerializer.GetFloatValue(nameof(SteeringWheelAngle), _data, _headers);

        public float SteeringWheelAngleMax => ValueSerializer.GetFloatValue(nameof(SteeringWheelAngleMax), _data, _headers);

        public float SteeringWheelPctDamper => ValueSerializer.GetFloatValue(nameof(SteeringWheelPctDamper), _data, _headers);

        public float SteeringWheelPctTorque => ValueSerializer.GetFloatValue(nameof(SteeringWheelPctTorque), _data, _headers);

        public float SteeringWheelPctTorqueSign => ValueSerializer.GetFloatValue(nameof(SteeringWheelPctTorqueSign), _data, _headers);

        public float SteeringWheelPctTorqueSignStops => ValueSerializer.GetFloatValue(nameof(SteeringWheelPctTorqueSignStops), _data, _headers);

        public float SteeringWheelPeakForceNm => ValueSerializer.GetFloatValue(nameof(SteeringWheelPeakForceNm), _data, _headers);

        public float SteeringWheelTorque => ValueSerializer.GetFloatValue(nameof(SteeringWheelTorque), _data, _headers);

        public float[] SteeringWheelTorque_ST => ValueSerializer.GetFloatArrayValue(nameof(SteeringWheelTorque_ST), _data, _headers);

        public float Throttle => ValueSerializer.GetFloatValue(nameof(Throttle), _data, _headers);

        public float ThrottleRaw => ValueSerializer.GetFloatValue(nameof(ThrottleRaw), _data, _headers);

        public float TireLF_RumblePitch => ValueSerializer.GetFloatValue(nameof(TireLF_RumblePitch), _data, _headers);

        public float TireLR_RumblePitch => ValueSerializer.GetFloatValue(nameof(TireLR_RumblePitch), _data, _headers);

        public float TireRF_RumblePitch => ValueSerializer.GetFloatValue(nameof(TireRF_RumblePitch), _data, _headers);

        public float TireRR_RumblePitch => ValueSerializer.GetFloatValue(nameof(TireRR_RumblePitch), _data, _headers);

        public float TrackTemp => ValueSerializer.GetFloatValue(nameof(TrackTemp), _data, _headers);

        public float TrackTempCrew => ValueSerializer.GetFloatValue(nameof(TrackTempCrew), _data, _headers);

        public float VelocityX => ValueSerializer.GetFloatValue(nameof(VelocityX), _data, _headers);

        public float[] VelocityX_ST => ValueSerializer.GetFloatArrayValue(nameof(VelocityX_ST), _data, _headers);

        public float VelocityY => ValueSerializer.GetFloatValue(nameof(VelocityY), _data, _headers);

        public float[] VelocityY_ST => ValueSerializer.GetFloatArrayValue(nameof(VelocityY_ST), _data, _headers);

        public float VelocityZ => ValueSerializer.GetFloatValue(nameof(VelocityZ), _data, _headers);

        public float[] VelocityZ_ST => ValueSerializer.GetFloatArrayValue(nameof(VelocityZ_ST), _data, _headers);

        public float VertAccel => ValueSerializer.GetFloatValue(nameof(VertAccel), _data, _headers);

        public float[] VertAccel_ST => ValueSerializer.GetFloatArrayValue(nameof(VertAccel_ST), _data, _headers);

        public float Voltage => ValueSerializer.GetFloatValue(nameof(Voltage), _data, _headers);

        public float WaterLevel => ValueSerializer.GetFloatValue(nameof(WaterLevel), _data, _headers);

        public float WaterTemp => ValueSerializer.GetFloatValue(nameof(WaterTemp), _data, _headers);

        public int WeatherType => ValueSerializer.GetIntValue(nameof(WeatherType), _data, _headers);

        public float WindDir => ValueSerializer.GetFloatValue(nameof(WindDir), _data, _headers);

        public float WindVel => ValueSerializer.GetFloatValue(nameof(WindVel), _data, _headers);

        public float Yaw => ValueSerializer.GetFloatValue(nameof(Yaw), _data, _headers);

        public float YawNorth => ValueSerializer.GetFloatValue(nameof(YawNorth), _data, _headers);

        public float YawRate => ValueSerializer.GetFloatValue(nameof(YawRate), _data, _headers);

        public float[] YawRate_ST => ValueSerializer.GetFloatArrayValue(nameof(YawRate_ST), _data, _headers);

        public float SteeringWheelMaxForceNm => ValueSerializer.GetFloatValue(nameof(SteeringWheelMaxForceNm), _data, _headers);

        public bool SteeringWheelUseLinear => ValueSerializer.GetBoolValue(nameof(SteeringWheelUseLinear), _data, _headers);

    }
}
