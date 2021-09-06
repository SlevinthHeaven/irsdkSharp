using System;
using System.Collections;
using irsdkSharp.Enums;

namespace irsdkSharp.Models
{
    public class Session
    {
        private readonly IRacingSDK _sdk;

        public Session(IRacingSDK sdk)
        {
            _sdk = sdk;
        }

        /// <summary>
        /// Density of air at start/finish line
        /// </summary>
        public float AirDensity => _sdk.Headers.TryGetValue(nameof(AirDensity), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pressure of air at start/finish line
        /// </summary>
        public float AirPressure => _sdk.Headers.TryGetValue(nameof(AirPressure), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Temperature of air at start/finish line
        /// </summary>
        public float AirTemp => _sdk.Headers.TryGetValue(nameof(AirTemp), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 0=brake released to 1=max pedal force
        /// </summary>
        public float Brake => _sdk.Headers.TryGetValue(nameof(Brake), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// true if abs is currently reducing brake force pressure
        /// </summary>
        public bool BrakeABSactive => _sdk.Headers.TryGetValue(nameof(BrakeABSactive), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Raw brake input 0=brake released to 1=max pedal force
        /// </summary>
        public float BrakeRaw => _sdk.Headers.TryGetValue(nameof(BrakeRaw), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Active camera number
        /// </summary>
        public int CamCameraNumber => _sdk.Headers.TryGetValue(nameof(CamCameraNumber), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// State of camera system
        /// </summary>
        public CameraState CamCameraState => _sdk.Headers.TryGetValue(nameof(CamCameraState), out var header)
            ? (CameraState) _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Active camera's focus car index
        /// </summary>
        public int CamCarIdx => _sdk.Headers.TryGetValue(nameof(CamCarIdx), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Active camera group number
        /// </summary>
        public int CamGroupNumber => _sdk.Headers.TryGetValue(nameof(CamGroupNumber), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Cars best lap number
        /// </summary>
        public int CarIdxBestLapNum => _sdk.Headers.TryGetValue(nameof(CarIdxBestLapNum), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Cars best lap time
        /// </summary>
        public float CarIdxBestLapTime => _sdk.Headers.TryGetValue(nameof(CarIdxBestLapTime), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Cars class id by car index
        /// </summary>
        public int CarIdxClass => _sdk.Headers.TryGetValue(nameof(CarIdxClass), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Cars class position in race by car index
        /// </summary>
        public int CarIdxClassPosition => _sdk.Headers.TryGetValue(nameof(CarIdxClassPosition), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Estimated time to reach current location on track
        /// </summary>
        public float CarIdxEstTime => _sdk.Headers.TryGetValue(nameof(CarIdxEstTime), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Race time behind leader or fastest lap time otherwise
        /// </summary>
        public float CarIdxF2Time => _sdk.Headers.TryGetValue(nameof(CarIdxF2Time), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many fast repairs each car has used
        /// </summary>
        public int CarIdxFastRepairsUsed => _sdk.Headers.TryGetValue(nameof(CarIdxFastRepairsUsed), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// -1=reverse  0=neutral  1..n=current gear by car index
        /// </summary>
        public int CarIdxGear => _sdk.Headers.TryGetValue(nameof(CarIdxGear), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Laps started by car index
        /// </summary>
        public int CarIdxLap => _sdk.Headers.TryGetValue(nameof(CarIdxLap), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Laps completed by car index
        /// </summary>
        public int CarIdxLapCompleted => _sdk.Headers.TryGetValue(nameof(CarIdxLapCompleted), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Percentage distance around lap by car index
        /// </summary>
        public float CarIdxLapDistPct => _sdk.Headers.TryGetValue(nameof(CarIdxLapDistPct), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Cars last lap time
        /// </summary>
        public float CarIdxLastLapTime => _sdk.Headers.TryGetValue(nameof(CarIdxLastLapTime), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// On pit road between the cones by car index
        /// </summary>
        public bool CarIdxOnPitRoad => _sdk.Headers.TryGetValue(nameof(CarIdxOnPitRoad), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Push2Pass count of usage (or remaining in Race)
        /// </summary>
        public int CarIdxP2P_Count => _sdk.Headers.TryGetValue(nameof(CarIdxP2P_Count), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Push2Pass active or not
        /// </summary>
        public bool CarIdxP2P_Status => _sdk.Headers.TryGetValue(nameof(CarIdxP2P_Status), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pacing status flags for each car
        /// </summary>
        public int CarIdxPaceFlags => _sdk.Headers.TryGetValue(nameof(CarIdxPaceFlags), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// What line cars are pacing in  or -1 if not pacing
        /// </summary>
        public int CarIdxPaceLine => _sdk.Headers.TryGetValue(nameof(CarIdxPaceLine), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// What row cars are pacing in  or -1 if not pacing
        /// </summary>
        public int CarIdxPaceRow => _sdk.Headers.TryGetValue(nameof(CarIdxPaceRow), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Cars position in race by car index
        /// </summary>
        public int CarIdxPosition => _sdk.Headers.TryGetValue(nameof(CarIdxPosition), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Cars Qual tire compound
        /// </summary>
        public int CarIdxQualTireCompound => _sdk.Headers.TryGetValue(nameof(CarIdxQualTireCompound), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Cars Qual tire compound is locked-in
        /// </summary>
        public bool CarIdxQualTireCompoundLocked =>
            _sdk.Headers.TryGetValue(nameof(CarIdxQualTireCompoundLocked), out var header)
                ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Engine rpm by car index
        /// </summary>
        public float CarIdxRPM => _sdk.Headers.TryGetValue(nameof(CarIdxRPM), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Steering wheel angle by car index
        /// </summary>
        public float CarIdxSteer => _sdk.Headers.TryGetValue(nameof(CarIdxSteer), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Cars current tire compound
        /// </summary>
        public int CarIdxTireCompound => _sdk.Headers.TryGetValue(nameof(CarIdxTireCompound), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Track surface type by car index
        /// </summary>
        public int CarIdxTrackSurface => _sdk.Headers.TryGetValue(nameof(CarIdxTrackSurface), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Track surface material type by car index
        /// </summary>
        public int CarIdxTrackSurfaceMaterial =>
            _sdk.Headers.TryGetValue(nameof(CarIdxTrackSurfaceMaterial), out var header)
                ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Notify if car is to the left or right of driver
        /// </summary>
        public CarLeftRight CarLeftRight => _sdk.Headers.TryGetValue(nameof(CarLeftRight), out var header)
            ? (CarLeftRight) _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Communications average latency
        /// </summary>
        public float ChanAvgLatency => _sdk.Headers.TryGetValue(nameof(ChanAvgLatency), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Communications server clock skew
        /// </summary>
        public float ChanClockSkew => _sdk.Headers.TryGetValue(nameof(ChanClockSkew), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Communications latency
        /// </summary>
        public float ChanLatency => _sdk.Headers.TryGetValue(nameof(ChanLatency), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Partner communications quality
        /// </summary>
        public float ChanPartnerQuality => _sdk.Headers.TryGetValue(nameof(ChanPartnerQuality), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Communications quality
        /// </summary>
        public float ChanQuality => _sdk.Headers.TryGetValue(nameof(ChanQuality), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 0=disengaged to 1=fully engaged
        /// </summary>
        public float Clutch => _sdk.Headers.TryGetValue(nameof(Clutch), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Percent of available tim bg thread took with a 1 sec avg
        /// </summary>
        public float CpuUsageBG => _sdk.Headers.TryGetValue(nameof(CpuUsageBG), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Percent of available tim fg thread took with a 1 sec avg
        /// </summary>
        public float CpuUsageFG => _sdk.Headers.TryGetValue(nameof(CpuUsageFG), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Number of team drivers who have run a stpublic int
        /// </summary>
        public int DCDriversSoFar => _sdk.Headers.TryGetValue(nameof(DCDriversSoFar), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Status of driver change lap requirements
        /// </summary>
        public int DCLapStatus => _sdk.Headers.TryGetValue(nameof(DCLapStatus), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// In car trigger car starter
        /// </summary>
        public bool dcStarter => _sdk.Headers.TryGetValue(nameof(dcStarter), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// In car tear off visor film
        /// </summary>
        public bool dcTearOffVisor => _sdk.Headers.TryGetValue(nameof(dcTearOffVisor), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Default units for the user public interface 0 = english 1 = metric
        /// </summary>
        public int DisplayUnits => _sdk.Headers.TryGetValue(nameof(DisplayUnits), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop fast repair set
        /// </summary>
        public float dpFastRepair => _sdk.Headers.TryGetValue(nameof(dpFastRepair), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop fuel add ammount
        /// </summary>
        public float dpFuelAddKg => _sdk.Headers.TryGetValue(nameof(dpFuelAddKg), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop fuel fill flag
        /// </summary>
        public float dpFuelFill => _sdk.Headers.TryGetValue(nameof(dpFuelFill), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop lf tire change request
        /// </summary>
        public float dpLFTireChange => _sdk.Headers.TryGetValue(nameof(dpLFTireChange), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop lf tire cold pressure adjustment
        /// </summary>
        public float dpLFTireColdPress => _sdk.Headers.TryGetValue(nameof(dpLFTireColdPress), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop lr tire change request
        /// </summary>
        public float dpLRTireChange => _sdk.Headers.TryGetValue(nameof(dpLRTireChange), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop lr tire cold pressure adjustment
        /// </summary>
        public float dpLRTireColdPress => _sdk.Headers.TryGetValue(nameof(dpLRTireColdPress), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop rf tire change request
        /// </summary>
        public float dpRFTireChange => _sdk.Headers.TryGetValue(nameof(dpRFTireChange), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop rf cold tire pressure adjustment
        /// </summary>
        public float dpRFTireColdPress => _sdk.Headers.TryGetValue(nameof(dpRFTireColdPress), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop rr tire change request
        /// </summary>
        public float dpRRTireChange => _sdk.Headers.TryGetValue(nameof(dpRRTireChange), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop rr cold tire pressure adjustment
        /// </summary>
        public float dpRRTireColdPress => _sdk.Headers.TryGetValue(nameof(dpRRTireColdPress), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Driver activated flag
        /// </summary>
        public bool DriverMarker => _sdk.Headers.TryGetValue(nameof(DriverMarker), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Bitfield for warning lights
        /// </summary>
        public EngineWarnings EngineWarnings => _sdk.Headers.TryGetValue(nameof(EngineWarnings), out var header)
            ? (EngineWarnings) _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Indicate action the reset key will take 0 enter 1 exit 2 reset
        /// </summary>
        public int EnterExitReset => _sdk.Headers.TryGetValue(nameof(EnterExitReset), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many fast repairs left  255 is unlimited
        /// </summary>
        public int FastRepairAvailable => _sdk.Headers.TryGetValue(nameof(FastRepairAvailable), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many fast repairs used so far
        /// </summary>
        public int FastRepairUsed => _sdk.Headers.TryGetValue(nameof(FastRepairUsed), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Fog level
        /// </summary>
        public float FogLevel => _sdk.Headers.TryGetValue(nameof(FogLevel), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Average frames per second
        /// </summary>
        public float FrameRate => _sdk.Headers.TryGetValue(nameof(FrameRate), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many front tire sets are remaining  255 is unlimited
        /// </summary>
        public int FrontTireSetsAvailable => _sdk.Headers.TryGetValue(nameof(FrontTireSetsAvailable), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many front tire sets used so far
        /// </summary>
        public int FrontTireSetsUsed => _sdk.Headers.TryGetValue(nameof(FrontTireSetsUsed), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Liters of fuel remaining
        /// </summary>
        public float FuelLevel => _sdk.Headers.TryGetValue(nameof(FuelLevel), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Percent fuel remaining
        /// </summary>
        public float FuelLevelPct => _sdk.Headers.TryGetValue(nameof(FuelLevelPct), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine fuel pressure
        /// </summary>
        public float FuelPress => _sdk.Headers.TryGetValue(nameof(FuelPress), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine fuel used instantaneous
        /// </summary>
        public float FuelUsePerHour => _sdk.Headers.TryGetValue(nameof(FuelUsePerHour), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// -1=reverse  0=neutral  1..n=current gear
        /// </summary>
        public int Gear => _sdk.Headers.TryGetValue(nameof(Gear), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Percent of available tim gpu took with a 1 sec avg
        /// </summary>
        public float GpuUsage => _sdk.Headers.TryGetValue(nameof(GpuUsage), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Raw handbrake input 0=handbrake released to 1=max force
        /// </summary>
        public float HandbrakeRaw => _sdk.Headers.TryGetValue(nameof(HandbrakeRaw), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 0=disk based telemetry file not being written  1=being written
        /// </summary>
        public bool IsDiskLoggingActive => _sdk.Headers.TryGetValue(nameof(IsDiskLoggingActive), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 0=disk based telemetry turned off  1=turned on
        /// </summary>
        public bool IsDiskLoggingEnabled => _sdk.Headers.TryGetValue(nameof(IsDiskLoggingEnabled), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 1=Car in garage physics running
        /// </summary>
        public bool IsInGarage => _sdk.Headers.TryGetValue(nameof(IsInGarage), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 1=Car on track physics running with player in car
        /// </summary>
        public bool IsOnTrack => _sdk.Headers.TryGetValue(nameof(IsOnTrack), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 1=Car on track physics running
        /// </summary>
        public bool IsOnTrackCar => _sdk.Headers.TryGetValue(nameof(IsOnTrackCar), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 0=replay not playing  1=replay playing
        /// </summary>
        public bool IsReplayPlaying => _sdk.Headers.TryGetValue(nameof(IsReplayPlaying), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Laps started count
        /// </summary>
        public int Lap => _sdk.Headers.TryGetValue(nameof(Lap), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players best lap number
        /// </summary>
        public int LapBestLap => _sdk.Headers.TryGetValue(nameof(LapBestLap), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players best lap time
        /// </summary>
        public float LapBestLapTime => _sdk.Headers.TryGetValue(nameof(LapBestLapTime), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Player last lap in best N average lap time
        /// </summary>
        public int LapBestNLapLap => _sdk.Headers.TryGetValue(nameof(LapBestNLapLap), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Player best N average lap time
        /// </summary>
        public float LapBestNLapTime => _sdk.Headers.TryGetValue(nameof(LapBestNLapTime), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Laps completed count
        /// </summary>
        public int LapCompleted => _sdk.Headers.TryGetValue(nameof(LapCompleted), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Estimate of players current lap time as shown in F3 box
        /// </summary>
        public float LapCurrentLapTime => _sdk.Headers.TryGetValue(nameof(LapCurrentLapTime), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Delta time for best lap
        /// </summary>
        public float LapDeltaToBestLap => _sdk.Headers.TryGetValue(nameof(LapDeltaToBestLap), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Rate of change of delta time for best lap
        /// </summary>
        public float LapDeltaToBestLap_DD => _sdk.Headers.TryGetValue(nameof(LapDeltaToBestLap_DD), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Delta time for best lap is valid
        /// </summary>
        public bool LapDeltaToBestLap_OK => _sdk.Headers.TryGetValue(nameof(LapDeltaToBestLap_OK), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Delta time for optimal lap
        /// </summary>
        public float LapDeltaToOptimalLap => _sdk.Headers.TryGetValue(nameof(LapDeltaToOptimalLap), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Rate of change of delta time for optimal lap
        /// </summary>
        public float LapDeltaToOptimalLap_DD =>
            _sdk.Headers.TryGetValue(nameof(LapDeltaToOptimalLap_DD), out var header)
                ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Delta time for optimal lap is valid
        /// </summary>
        public bool LapDeltaToOptimalLap_OK => _sdk.Headers.TryGetValue(nameof(LapDeltaToOptimalLap_OK), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Delta time for session best lap
        /// </summary>
        public float LapDeltaToSessionBestLap =>
            _sdk.Headers.TryGetValue(nameof(LapDeltaToSessionBestLap), out var header)
                ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Rate of change of delta time for session best lap
        /// </summary>
        public float LapDeltaToSessionBestLap_DD =>
            _sdk.Headers.TryGetValue(nameof(LapDeltaToSessionBestLap_DD), out var header)
                ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Delta time for session best lap is valid
        /// </summary>
        public bool LapDeltaToSessionBestLap_OK =>
            _sdk.Headers.TryGetValue(nameof(LapDeltaToSessionBestLap_OK), out var header)
                ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Delta time for session last lap
        /// </summary>
        public float LapDeltaToSessionLastlLap =>
            _sdk.Headers.TryGetValue(nameof(LapDeltaToSessionLastlLap), out var header)
                ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Rate of change of delta time for session last lap
        /// </summary>
        public float LapDeltaToSessionLastlLap_DD =>
            _sdk.Headers.TryGetValue(nameof(LapDeltaToSessionLastlLap_DD), out var header)
                ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Delta time for session last lap is valid
        /// </summary>
        public bool LapDeltaToSessionLastlLap_OK =>
            _sdk.Headers.TryGetValue(nameof(LapDeltaToSessionLastlLap_OK), out var header)
                ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Delta time for session optimal lap
        /// </summary>
        public float LapDeltaToSessionOptimalLap =>
            _sdk.Headers.TryGetValue(nameof(LapDeltaToSessionOptimalLap), out var header)
                ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Rate of change of delta time for session optimal lap
        /// </summary>
        public float LapDeltaToSessionOptimalLap_DD =>
            _sdk.Headers.TryGetValue(nameof(LapDeltaToSessionOptimalLap_DD), out var header)
                ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Delta time for session optimal lap is valid
        /// </summary>
        public bool LapDeltaToSessionOptimalLap_OK =>
            _sdk.Headers.TryGetValue(nameof(LapDeltaToSessionOptimalLap_OK), out var header)
                ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Meters traveled from S/F this lap
        /// </summary>
        public float LapDist => _sdk.Headers.TryGetValue(nameof(LapDist), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Percentage distance around lap
        /// </summary>
        public float LapDistPct => _sdk.Headers.TryGetValue(nameof(LapDistPct), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Player num consecutive clean laps completed for N average
        /// </summary>
        public int LapLasNLapSeq => _sdk.Headers.TryGetValue(nameof(LapLasNLapSeq), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players last lap time
        /// </summary>
        public float LapLastLapTime => _sdk.Headers.TryGetValue(nameof(LapLastLapTime), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Player last N average lap time
        /// </summary>
        public float LapLastNLapTime => _sdk.Headers.TryGetValue(nameof(LapLastNLapTime), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Lateral acceleration (including gravity)
        /// </summary>
        public float LatAccel => _sdk.Headers.TryGetValue(nameof(LatAccel), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Lateral acceleration (including gravity) at 360 Hz
        /// </summary>
        public float LatAccel_ST => _sdk.Headers.TryGetValue(nameof(LatAccel_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many left tire sets are remaining  255 is unlimited
        /// </summary>
        public int LeftTireSetsAvailable => _sdk.Headers.TryGetValue(nameof(LeftTireSetsAvailable), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many left tire sets used so far
        /// </summary>
        public int LeftTireSetsUsed => _sdk.Headers.TryGetValue(nameof(LeftTireSetsUsed), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF tire cold pressure  as set in the garage
        /// </summary>
        public float LFcoldPressure => _sdk.Headers.TryGetValue(nameof(LFcoldPressure), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF shock deflection
        /// </summary>
        public float LFshockDefl => _sdk.Headers.TryGetValue(nameof(LFshockDefl), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF shock deflection at 360 Hz
        /// </summary>
        public float LFshockDefl_ST => _sdk.Headers.TryGetValue(nameof(LFshockDefl_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF shock velocity
        /// </summary>
        public float LFshockVel => _sdk.Headers.TryGetValue(nameof(LFshockVel), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF shock velocity at 360 Hz
        /// </summary>
        public float LFshockVel_ST => _sdk.Headers.TryGetValue(nameof(LFshockVel_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF tire left carcass temperature
        /// </summary>
        public float LFtempCL => _sdk.Headers.TryGetValue(nameof(LFtempCL), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF tire middle carcass temperature
        /// </summary>
        public float LFtempCM => _sdk.Headers.TryGetValue(nameof(LFtempCM), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF tire right carcass temperature
        /// </summary>
        public float LFtempCR => _sdk.Headers.TryGetValue(nameof(LFtempCR), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many left front tires are remaining  255 is unlimited
        /// </summary>
        public int LFTiresAvailable => _sdk.Headers.TryGetValue(nameof(LFTiresAvailable), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many left front tires used so far
        /// </summary>
        public int LFTiresUsed => _sdk.Headers.TryGetValue(nameof(LFTiresUsed), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF tire left percent tread remaining
        /// </summary>
        public float LFwearL => _sdk.Headers.TryGetValue(nameof(LFwearL), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF tire middle percent tread remaining
        /// </summary>
        public float LFwearM => _sdk.Headers.TryGetValue(nameof(LFwearM), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF tire right percent tread remaining
        /// </summary>
        public float LFwearR => _sdk.Headers.TryGetValue(nameof(LFwearR), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// True if the car_num texture will be loaded
        /// </summary>
        public bool LoadNumTextures => _sdk.Headers.TryGetValue(nameof(LoadNumTextures), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Longitudinal acceleration (including gravity)
        /// </summary>
        public float LongAccel => _sdk.Headers.TryGetValue(nameof(LongAccel), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Longitudinal acceleration (including gravity) at 360 Hz
        /// </summary>
        public float LongAccel_ST => _sdk.Headers.TryGetValue(nameof(LongAccel_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR tire cold pressure  as set in the garage
        /// </summary>
        public float LRcoldPressure => _sdk.Headers.TryGetValue(nameof(LRcoldPressure), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR shock deflection
        /// </summary>
        public float LRshockDefl => _sdk.Headers.TryGetValue(nameof(LRshockDefl), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR shock deflection at 360 Hz
        /// </summary>
        public float LRshockDefl_ST => _sdk.Headers.TryGetValue(nameof(LRshockDefl_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR shock velocity
        /// </summary>
        public float LRshockVel => _sdk.Headers.TryGetValue(nameof(LRshockVel), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR shock velocity at 360 Hz
        /// </summary>
        public float LRshockVel_ST => _sdk.Headers.TryGetValue(nameof(LRshockVel_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR tire left carcass temperature
        /// </summary>
        public float LRtempCL => _sdk.Headers.TryGetValue(nameof(LRtempCL), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR tire middle carcass temperature
        /// </summary>
        public float LRtempCM => _sdk.Headers.TryGetValue(nameof(LRtempCM), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR tire right carcass temperature
        /// </summary>
        public float LRtempCR => _sdk.Headers.TryGetValue(nameof(LRtempCR), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many left rear tires are remaining  255 is unlimited
        /// </summary>
        public int LRTiresAvailable => _sdk.Headers.TryGetValue(nameof(LRTiresAvailable), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many left rear tires used so far
        /// </summary>
        public int LRTiresUsed => _sdk.Headers.TryGetValue(nameof(LRTiresUsed), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR tire left percent tread remaining
        /// </summary>
        public float LRwearL => _sdk.Headers.TryGetValue(nameof(LRwearL), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR tire middle percent tread remaining
        /// </summary>
        public float LRwearM => _sdk.Headers.TryGetValue(nameof(LRwearM), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR tire right percent tread remaining
        /// </summary>
        public float LRwearR => _sdk.Headers.TryGetValue(nameof(LRwearR), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine manifold pressure
        /// </summary>
        public float ManifoldPress => _sdk.Headers.TryGetValue(nameof(ManifoldPress), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Hybrid manual boost state
        /// </summary>
        public bool ManualBoost => _sdk.Headers.TryGetValue(nameof(ManualBoost), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Hybrid manual no boost state
        /// </summary>
        public bool ManualNoBoost => _sdk.Headers.TryGetValue(nameof(ManualNoBoost), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Memory page faults per second
        /// </summary>
        public float MemPageFaultSec => _sdk.Headers.TryGetValue(nameof(MemPageFaultSec), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine oil level
        /// </summary>
        public float OilLevel => _sdk.Headers.TryGetValue(nameof(OilLevel), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine oil pressure
        /// </summary>
        public float OilPress => _sdk.Headers.TryGetValue(nameof(OilPress), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine oil temperature
        /// </summary>
        public float OilTemp => _sdk.Headers.TryGetValue(nameof(OilTemp), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// True if it is ok to reload car textures at this time
        /// </summary>
        public bool OkToReloadTextures => _sdk.Headers.TryGetValue(nameof(OkToReloadTextures), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Is the player car on pit road between the cones
        /// </summary>
        public bool OnPitRoad => _sdk.Headers.TryGetValue(nameof(OnPitRoad), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Are we pacing or not
        /// </summary>
        public int PaceMode => _sdk.Headers.TryGetValue(nameof(PaceMode), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitch orientation
        /// </summary>
        public float Pitch => _sdk.Headers.TryGetValue(nameof(Pitch), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitch rate
        /// </summary>
        public float PitchRate => _sdk.Headers.TryGetValue(nameof(PitchRate), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitch rate at 360 Hz
        /// </summary>
        public float PitchRate_ST => _sdk.Headers.TryGetValue(nameof(PitchRate_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Time left for optional repairs if repairs are active
        /// </summary>
        public float PitOptRepairLeft => _sdk.Headers.TryGetValue(nameof(PitOptRepairLeft), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Time left for mandatory pit repairs if repairs are active
        /// </summary>
        public float PitRepairLeft => _sdk.Headers.TryGetValue(nameof(PitRepairLeft), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// True if pit stop is allowed for the current player
        /// </summary>
        public bool PitsOpen => _sdk.Headers.TryGetValue(nameof(PitsOpen), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Is the player getting pit stop service
        /// </summary>
        public bool PitstopActive => _sdk.Headers.TryGetValue(nameof(PitstopActive), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Bitfield of pit service checkboxes
        /// </summary>
        public PitServiceFlags PitSvSessionFlags => _sdk.Headers.TryGetValue(nameof(PitSvSessionFlags), out var header)
            ? (PitServiceFlags) _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pit service fuel add amount
        /// </summary>
        public float PitSvFuel => _sdk.Headers.TryGetValue(nameof(PitSvFuel), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pit service left front tire pressure
        /// </summary>
        public float PitSvLFP => _sdk.Headers.TryGetValue(nameof(PitSvLFP), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pit service left rear tire pressure
        /// </summary>
        public float PitSvLRP => _sdk.Headers.TryGetValue(nameof(PitSvLRP), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pit service right front tire pressure
        /// </summary>
        public float PitSvRFP => _sdk.Headers.TryGetValue(nameof(PitSvRFP), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pit service right rear tire pressure
        /// </summary>
        public float PitSvRRP => _sdk.Headers.TryGetValue(nameof(PitSvRRP), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pit service pending tire compound
        /// </summary>
        public int PitSvTireCompound => _sdk.Headers.TryGetValue(nameof(PitSvTireCompound), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Player car class id
        /// </summary>
        public int PlayerCarClass => _sdk.Headers.TryGetValue(nameof(PlayerCarClass), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players class position in race
        /// </summary>
        public int PlayerCarClassPosition => _sdk.Headers.TryGetValue(nameof(PlayerCarClassPosition), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Teams current drivers incident count for this session
        /// </summary>
        public int PlayerCarDriverIncidentCount =>
            _sdk.Headers.TryGetValue(nameof(PlayerCarDriverIncidentCount), out var header)
                ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Players dry tire set limit
        /// </summary>
        public int PlayerCarDryTireSetLimit =>
            _sdk.Headers.TryGetValue(nameof(PlayerCarDryTireSetLimit), out var header)
                ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Players carIdx
        /// </summary>
        public int PlayerCarIdx => _sdk.Headers.TryGetValue(nameof(PlayerCarIdx), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players car is properly in there pitstall
        /// </summary>
        public bool PlayerCarInPitStall => _sdk.Headers.TryGetValue(nameof(PlayerCarInPitStall), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players own incident count for this session
        /// </summary>
        public int PlayerCarMyIncidentCount =>
            _sdk.Headers.TryGetValue(nameof(PlayerCarMyIncidentCount), out var header)
                ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Players car pit service status bits
        /// </summary>
        public PitServiceStatus PlayerCarPitSvStatus =>
            _sdk.Headers.TryGetValue(nameof(PlayerCarPitSvStatus), out var header)
                ? (PitServiceStatus) _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Players position in race
        /// </summary>
        public int PlayerCarPosition => _sdk.Headers.TryGetValue(nameof(PlayerCarPosition), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players power adjust
        /// </summary>
        public float PlayerCarPowerAdjust => _sdk.Headers.TryGetValue(nameof(PlayerCarPowerAdjust), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players team incident count for this session
        /// </summary>
        public int PlayerCarTeamIncidentCount =>
            _sdk.Headers.TryGetValue(nameof(PlayerCarTeamIncidentCount), out var header)
                ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Players car is being towed if time is greater than zero
        /// </summary>
        public float PlayerCarTowTime => _sdk.Headers.TryGetValue(nameof(PlayerCarTowTime), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players weight penalty
        /// </summary>
        public float PlayerCarWeightPenalty => _sdk.Headers.TryGetValue(nameof(PlayerCarWeightPenalty), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players car number of fast repairs used
        /// </summary>
        public int PlayerFastRepairsUsed => _sdk.Headers.TryGetValue(nameof(PlayerFastRepairsUsed), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players car current tire compound
        /// </summary>
        public int PlayerTireCompound => _sdk.Headers.TryGetValue(nameof(PlayerTireCompound), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players car track surface type
        /// </summary>
        public int PlayerTrackSurface => _sdk.Headers.TryGetValue(nameof(PlayerTrackSurface), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players car track surface material type
        /// </summary>
        public int PlayerTrackSurfaceMaterial =>
            _sdk.Headers.TryGetValue(nameof(PlayerTrackSurfaceMaterial), out var header)
                ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Push to pass button state
        /// </summary>
        public bool PushToPass => _sdk.Headers.TryGetValue(nameof(PushToPass), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Laps completed in race
        /// </summary>
        public int RaceLaps => _sdk.Headers.TryGetValue(nameof(RaceLaps), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// The car index of the current person speaking on the radio
        /// </summary>
        public int RadioTransmitCarIdx => _sdk.Headers.TryGetValue(nameof(RadioTransmitCarIdx), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// The frequency index of the current person speaking on the radio
        /// </summary>
        public int RadioTransmitFrequencyIdx =>
            _sdk.Headers.TryGetValue(nameof(RadioTransmitFrequencyIdx), out var header)
                ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// The radio index of the current person speaking on the radio
        /// </summary>
        public int RadioTransmitRadioIdx => _sdk.Headers.TryGetValue(nameof(RadioTransmitRadioIdx), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many rear tire sets are remaining  255 is unlimited
        /// </summary>
        public int RearTireSetsAvailable => _sdk.Headers.TryGetValue(nameof(RearTireSetsAvailable), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many rear tire sets used so far
        /// </summary>
        public int RearTireSetsUsed => _sdk.Headers.TryGetValue(nameof(RearTireSetsUsed), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Relative Humidity
        /// </summary>
        public float RelativeHumidity => _sdk.Headers.TryGetValue(nameof(RelativeHumidity), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// public integer replay frame number (60 per second)
        /// </summary>
        public int ReplayFrameNum => _sdk.Headers.TryGetValue(nameof(ReplayFrameNum), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// public integer replay frame number from end of tape
        /// </summary>
        public int ReplayFrameNumEnd => _sdk.Headers.TryGetValue(nameof(ReplayFrameNumEnd), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 0=not slow motion  1=replay is in slow motion
        /// </summary>
        public bool ReplayPlaySlowMotion => _sdk.Headers.TryGetValue(nameof(ReplayPlaySlowMotion), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Replay playback speed
        /// </summary>
        public int ReplayPlaySpeed => _sdk.Headers.TryGetValue(nameof(ReplayPlaySpeed), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Replay session number
        /// </summary>
        public int ReplaySessionNum => _sdk.Headers.TryGetValue(nameof(ReplaySessionNum), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Seconds since replay session start
        /// </summary>
        double ReplaySessionTime;

        /// <summary>
        /// RF tire cold pressure  as set in the garage
        /// </summary>
        public float RFcoldPressure => _sdk.Headers.TryGetValue(nameof(RFcoldPressure), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF shock deflection
        /// </summary>
        public float RFshockDefl => _sdk.Headers.TryGetValue(nameof(RFshockDefl), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF shock deflection at 360 Hz
        /// </summary>
        public float RFshockDefl_ST => _sdk.Headers.TryGetValue(nameof(RFshockDefl_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF shock velocity
        /// </summary>
        public float RFshockVel => _sdk.Headers.TryGetValue(nameof(RFshockVel), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF shock velocity at 360 Hz
        /// </summary>
        public float RFshockVel_ST => _sdk.Headers.TryGetValue(nameof(RFshockVel_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF tire left carcass temperature
        /// </summary>
        public float RFtempCL => _sdk.Headers.TryGetValue(nameof(RFtempCL), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF tire middle carcass temperature
        /// </summary>
        public float RFtempCM => _sdk.Headers.TryGetValue(nameof(RFtempCM), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF tire right carcass temperature
        /// </summary>
        public float RFtempCR => _sdk.Headers.TryGetValue(nameof(RFtempCR), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many right front tires are remaining  255 is unlimited
        /// </summary>
        public int RFTiresAvailable => _sdk.Headers.TryGetValue(nameof(RFTiresAvailable), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many right front tires used so far
        /// </summary>
        public int RFTiresUsed => _sdk.Headers.TryGetValue(nameof(RFTiresUsed), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF tire left percent tread remaining
        /// </summary>
        public float RFwearL => _sdk.Headers.TryGetValue(nameof(RFwearL), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF tire middle percent tread remaining
        /// </summary>
        public float RFwearM => _sdk.Headers.TryGetValue(nameof(RFwearM), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF tire right percent tread remaining
        /// </summary>
        public float RFwearR => _sdk.Headers.TryGetValue(nameof(RFwearR), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many right tire sets are remaining  255 is unlimited
        /// </summary>
        public int RightTireSetsAvailable => _sdk.Headers.TryGetValue(nameof(RightTireSetsAvailable), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many right tire sets used so far
        /// </summary>
        public int RightTireSetsUsed => _sdk.Headers.TryGetValue(nameof(RightTireSetsUsed), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Roll orientation
        /// </summary>
        public float Roll => _sdk.Headers.TryGetValue(nameof(Roll), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Roll rate
        /// </summary>
        public float RollRate => _sdk.Headers.TryGetValue(nameof(RollRate), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Roll rate at 360 Hz
        /// </summary>
        public float RollRate_ST => _sdk.Headers.TryGetValue(nameof(RollRate_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine rpm
        /// </summary>
        public float RPM => _sdk.Headers.TryGetValue(nameof(RPM), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR tire cold pressure  as set in the garage
        /// </summary>
        public float RRcoldPressure => _sdk.Headers.TryGetValue(nameof(RRcoldPressure), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR shock deflection
        /// </summary>
        public float RRshockDefl => _sdk.Headers.TryGetValue(nameof(RRshockDefl), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR shock deflection at 360 Hz
        /// </summary>
        public float RRshockDefl_ST => _sdk.Headers.TryGetValue(nameof(RRshockDefl_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR shock velocity
        /// </summary>
        public float RRshockVel => _sdk.Headers.TryGetValue(nameof(RRshockVel), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR shock velocity at 360 Hz
        /// </summary>
        public float RRshockVel_ST => _sdk.Headers.TryGetValue(nameof(RRshockVel_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR tire left carcass temperature
        /// </summary>
        public float RRtempCL => _sdk.Headers.TryGetValue(nameof(RRtempCL), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR tire middle carcass temperature
        /// </summary>
        public float RRtempCM => _sdk.Headers.TryGetValue(nameof(RRtempCM), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR tire right carcass temperature
        /// </summary>
        public float RRtempCR => _sdk.Headers.TryGetValue(nameof(RRtempCR), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many right rear tires are remaining  255 is unlimited
        /// </summary>
        public int RRTiresAvailable => _sdk.Headers.TryGetValue(nameof(RRTiresAvailable), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many right rear tires used so far
        /// </summary>
        public int RRTiresUsed => _sdk.Headers.TryGetValue(nameof(RRTiresUsed), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR tire left percent tread remaining
        /// </summary>
        public float RRwearL => _sdk.Headers.TryGetValue(nameof(RRwearL), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR tire middle percent tread remaining
        /// </summary>
        public float RRwearM => _sdk.Headers.TryGetValue(nameof(RRwearM), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR tire right percent tread remaining
        /// </summary>
        public float RRwearR => _sdk.Headers.TryGetValue(nameof(RRwearR), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Session flags
        /// </summary>
        public SessionFlags SessionFlags => _sdk.Headers.TryGetValue(nameof(SessionFlags), out var header)
            ? (SessionFlags) _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Old laps left till session ends use SessionLapsRemainEx
        /// </summary>
        public int SessionLapsRemain => _sdk.Headers.TryGetValue(nameof(SessionLapsRemain), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// New improved laps left till session ends
        /// </summary>
        public int SessionLapsRemainEx => _sdk.Headers.TryGetValue(nameof(SessionLapsRemainEx), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Total number of laps in session
        /// </summary>
        public int SessionLapsTotal => _sdk.Headers.TryGetValue(nameof(SessionLapsTotal), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Session number
        /// </summary>
        public int SessionNum => _sdk.Headers.TryGetValue(nameof(SessionNum), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Session state
        /// </summary>
        public int SessionState => _sdk.Headers.TryGetValue(nameof(SessionState), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Current update number
        /// </summary>
        public int SessionTick => _sdk.Headers.TryGetValue(nameof(SessionTick), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Seconds since session start
        /// </summary>
        double SessionTime;

        /// <summary>
        /// Time of day in seconds
        /// </summary>
        public float SessionTimeOfDay => _sdk.Headers.TryGetValue(nameof(SessionTimeOfDay), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Seconds left till session ends
        /// </summary>
        double SessionTimeRemain;

        /// <summary>
        /// Total number of seconds in session
        /// </summary>
        double SessionTimeTotal;

        /// <summary>
        /// Session ID
        /// </summary>
        public int SessionUniqueID => _sdk.Headers.TryGetValue(nameof(SessionUniqueID), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RPM of shifter grinding noise
        /// </summary>
        public float ShiftGrindRPM => _sdk.Headers.TryGetValue(nameof(ShiftGrindRPM), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// DEPRECATED use DriverCarSLBlinkRPM instead
        /// </summary>
        public float ShiftIndicatorPct => _sdk.Headers.TryGetValue(nameof(ShiftIndicatorPct), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Friction torque applied to gears when shifting or grinding
        /// </summary>
        public float ShiftPowerPct => _sdk.Headers.TryGetValue(nameof(ShiftPowerPct), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Skies (0=clear/1=p cloudy/2=m cloudy/3=overcast)
        /// </summary>
        public int Skies => _sdk.Headers.TryGetValue(nameof(Skies), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// GPS vehicle speed
        /// </summary>
        public float Speed => _sdk.Headers.TryGetValue(nameof(Speed), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Steering wheel angle
        /// </summary>
        public float SteeringWheelAngle => _sdk.Headers.TryGetValue(nameof(SteeringWheelAngle), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Steering wheel max angle
        /// </summary>
        public float SteeringWheelAngleMax => _sdk.Headers.TryGetValue(nameof(SteeringWheelAngleMax), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Force feedback limiter strength limits impacts and oscillation
        /// </summary>
        public float SteeringWheelLimiter => _sdk.Headers.TryGetValue(nameof(SteeringWheelLimiter), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Force feedback 
        /// </summary>
        public float SteeringWheelPctDamper => _sdk.Headers.TryGetValue(nameof(SteeringWheelPctDamper), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Force feedback 
        /// </summary>
        public float SteeringWheelPctTorque => _sdk.Headers.TryGetValue(nameof(SteeringWheelPctTorque), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Force feedback 
        /// </summary>
        public float SteeringWheelPctTorqueSign =>
            _sdk.Headers.TryGetValue(nameof(SteeringWheelPctTorqueSign), out var header)
                ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Force feedback 
        /// </summary>
        public float SteeringWheelPctTorqueSignStops =>
            _sdk.Headers.TryGetValue(nameof(SteeringWheelPctTorqueSignStops), out var header)
                ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Peak torque mapping to direct input units for FFB
        /// </summary>
        public float SteeringWheelPeakForceNm =>
            _sdk.Headers.TryGetValue(nameof(SteeringWheelPeakForceNm), out var header)
                ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Output torque on steering shaft
        /// </summary>
        public float SteeringWheelTorque => _sdk.Headers.TryGetValue(nameof(SteeringWheelTorque), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Output torque on steering shaft at 360 Hz
        /// </summary>
        public float SteeringWheelTorque_ST => _sdk.Headers.TryGetValue(nameof(SteeringWheelTorque_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 0=off throttle to 1=full throttle
        /// </summary>
        public float Throttle => _sdk.Headers.TryGetValue(nameof(Throttle), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Raw throttle input 0=off throttle to 1=full throttle
        /// </summary>
        public float ThrottleRaw => _sdk.Headers.TryGetValue(nameof(ThrottleRaw), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players LF Tire Sound rumblestrip pitch
        /// </summary>
        public float TireLF_RumblePitch => _sdk.Headers.TryGetValue(nameof(TireLF_RumblePitch), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players LR Tire Sound rumblestrip pitch
        /// </summary>
        public float TireLR_RumblePitch => _sdk.Headers.TryGetValue(nameof(TireLR_RumblePitch), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players RF Tire Sound rumblestrip pitch
        /// </summary>
        public float TireRF_RumblePitch => _sdk.Headers.TryGetValue(nameof(TireRF_RumblePitch), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players RR Tire Sound rumblestrip pitch
        /// </summary>
        public float TireRR_RumblePitch => _sdk.Headers.TryGetValue(nameof(TireRR_RumblePitch), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many tire sets are remaining  255 is unlimited
        /// </summary>
        public int TireSetsAvailable => _sdk.Headers.TryGetValue(nameof(TireSetsAvailable), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many tire sets used so far
        /// </summary>
        public int TireSetsUsed => _sdk.Headers.TryGetValue(nameof(TireSetsUsed), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Deprecated  set to TrackTempCrew
        /// </summary>
        public float TrackTemp => _sdk.Headers.TryGetValue(nameof(TrackTemp), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Temperature of track measured by crew around track
        /// </summary>
        public float TrackTempCrew => _sdk.Headers.TryGetValue(nameof(TrackTempCrew), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// X velocity
        /// </summary>
        public float VelocityX => _sdk.Headers.TryGetValue(nameof(VelocityX), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// X velocity
        /// </summary>
        public float VelocityX_ST => _sdk.Headers.TryGetValue(nameof(VelocityX_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Y velocity
        /// </summary>
        public float VelocityY => _sdk.Headers.TryGetValue(nameof(VelocityY), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Y velocity
        /// </summary>
        public float VelocityY_ST => _sdk.Headers.TryGetValue(nameof(VelocityY_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Z velocity
        /// </summary>
        public float VelocityZ => _sdk.Headers.TryGetValue(nameof(VelocityZ), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Z velocity
        /// </summary>
        public float VelocityZ_ST => _sdk.Headers.TryGetValue(nameof(VelocityZ_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Vertical acceleration (including gravity)
        /// </summary>
        public float VertAccel => _sdk.Headers.TryGetValue(nameof(VertAccel), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Vertical acceleration (including gravity) at 360 Hz
        /// </summary>
        public float VertAccel_ST => _sdk.Headers.TryGetValue(nameof(VertAccel_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// True if video currently being captured
        /// </summary>
        public bool VidCapActive => _sdk.Headers.TryGetValue(nameof(VidCapActive), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// True if video capture system is enabled
        /// </summary>
        public bool VidCapEnabled => _sdk.Headers.TryGetValue(nameof(VidCapEnabled), out var header)
            ? _sdk.FileMapView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine voltage
        /// </summary>
        public float Voltage => _sdk.Headers.TryGetValue(nameof(Voltage), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine coolant level
        /// </summary>
        public float WaterLevel => _sdk.Headers.TryGetValue(nameof(WaterLevel), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine coolant temp
        /// </summary>
        public float WaterTemp => _sdk.Headers.TryGetValue(nameof(WaterTemp), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Weather type (0=constant  1=dynamic)
        /// </summary>
        public int WeatherType => _sdk.Headers.TryGetValue(nameof(WeatherType), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Wind direction at start/finish line
        /// </summary>
        public float WindDir => _sdk.Headers.TryGetValue(nameof(WindDir), out var header)
            ? _sdk.FileMapView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default(float);

        /// <summary>
        /// Wind velocity at start/finish line
        /// </summary>
        public float WindVel => _sdk.Headers.TryGetValue(nameof(WindVel), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Yaw orientation
        /// </summary>
        public float Yaw => _sdk.Headers.TryGetValue(nameof(Yaw), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Yaw orientation relative to north
        /// </summary>
        public float YawNorth => _sdk.Headers.TryGetValue(nameof(YawNorth), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Yaw rate
        /// </summary>
        public float YawRate => _sdk.Headers.TryGetValue(nameof(YawRate), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Yaw rate at 360 Hz
        /// </summary>
        public float YawRate_ST => _sdk.Headers.TryGetValue(nameof(YawRate_ST), out var header)
            ? _sdk.FileMapView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;
    }
}