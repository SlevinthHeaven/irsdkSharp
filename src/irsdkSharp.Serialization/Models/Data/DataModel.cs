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
        }

        private CarModel[] _cars;
        public CarModel[] Cars
        {
            get
            {
                if (_cars == null) 
                {
                    _cars = new CarModel[64];
                    for (var i = 0; i < _cars.Length; i++)
                    {
                        Cars[i] = new CarModel(i, _data, _headers);
                    }
                }
                return _cars;
            }
        }

        private float? _airDensity;
        public float AirDensity
        {
            get
            {
                if (!_airDensity.HasValue) _airDensity = ValueSerializer.GetFloatValue(nameof(AirDensity), _data, _headers);
                return _airDensity.Value;
            }
        }

        private float? _airPressure;
        public float AirPressure
        {
            get
            {
                if (!_airPressure.HasValue) _airPressure = ValueSerializer.GetFloatValue(nameof(AirPressure), _data, _headers);
                return _airPressure.Value;
            }
        }
        private float? _airTemp;
        public float AirTemp
        {
            get
            {
                if (!_airTemp.HasValue) _airTemp = ValueSerializer.GetFloatValue(nameof(AirTemp), _data, _headers);
                return _airTemp.Value;
            }
        }
        private float? _brake;
        public float Brake
        {
            get
            {
                if (!_brake.HasValue) _brake = ValueSerializer.GetFloatValue(nameof(Brake), _data, _headers);
                return _brake.Value;
            }
        }
        private float? _brakeRaw;
        public float BrakeRaw
        {
            get
            {
                if (!_brakeRaw.HasValue) _brakeRaw = ValueSerializer.GetFloatValue(nameof(BrakeRaw), _data, _headers);
                return _brakeRaw.Value;
            }
        }
        private int? _camCameraNumber;
        public int CamCameraNumber
        {
            get
            {
                if (!_camCameraNumber.HasValue) _camCameraNumber = ValueSerializer.GetIntValue(nameof(CamCameraNumber), _data, _headers);
                return _camCameraNumber.Value;
            }
        }
        private int? _camCameraState;
        public int CamCameraState
        {
            get
            {
                if (!_camCameraState.HasValue) _camCameraState = ValueSerializer.GetIntValue(nameof(CamCameraState), _data, _headers);
                return _camCameraState.Value;
            }
        }
        private int? _camCarIdx;
        public int CamCarIdx
        {
            get
            {
                if (!_camCarIdx.HasValue) _camCarIdx = ValueSerializer.GetIntValue(nameof(CamCarIdx), _data, _headers);
                return _camCarIdx.Value;
            }
        }
        private int? _camGroupNumber;
        public int CamGroupNumber
        {
            get
            {
                if (!_camGroupNumber.HasValue) _camGroupNumber = ValueSerializer.GetIntValue(nameof(CamGroupNumber), _data, _headers);
                return _camGroupNumber.Value;
            }
        }
        private int? _carLeftRight;
        public int CarLeftRight
        {
            get
            {
                if (!_carLeftRight.HasValue) _carLeftRight = ValueSerializer.GetIntValue(nameof(CarLeftRight), _data, _headers);
                return _carLeftRight.Value;
            }
        }
        private float? _clutch;
        public float Clutch
        {
            get
            {
                if (!_clutch.HasValue) _clutch = ValueSerializer.GetFloatValue(nameof(Clutch), _data, _headers);
                return _clutch.Value;
            }
        }
        private float? _cpuUsageBG;
        public float CpuUsageBG
        {
            get
            {
                if (!_cpuUsageBG.HasValue) _cpuUsageBG = ValueSerializer.GetFloatValue(nameof(CpuUsageBG), _data, _headers);
                return _cpuUsageBG.Value;
            }
        }
        private int? _dCDriversSoFar;
        public int DCDriversSoFar
        {
            get
            {
                if (!_dCDriversSoFar.HasValue) _dCDriversSoFar = ValueSerializer.GetIntValue(nameof(DCDriversSoFar), _data, _headers);
                return _dCDriversSoFar.Value;
            }
        }
        private int? _dCLapStatus;
        public int DCLapStatus
        {
            get
            {
                if (!_dCLapStatus.HasValue) _dCLapStatus = ValueSerializer.GetIntValue(nameof(DCLapStatus), _data, _headers);
                return _dCLapStatus.Value;
            }
        }
        private bool? _dcStarter;
        public bool dcStarter
        {
            get
            {
                if (!_dcStarter.HasValue) _dcStarter = ValueSerializer.GetBoolValue(nameof(dcStarter), _data, _headers);
                return _dcStarter.Value;
            }
        }
        private int? _displayUnits;
        public int DisplayUnits
        {
            get
            {
                if (!_displayUnits.HasValue) _displayUnits = ValueSerializer.GetIntValue(nameof(DisplayUnits), _data, _headers);
                return _displayUnits.Value;
            }
        }
        private float? _dpFastRepair;
        public float dpFastRepair
        {
            get
            {
                if (!_dpFastRepair.HasValue) _dpFastRepair = ValueSerializer.GetFloatValue(nameof(dpFastRepair), _data, _headers);
                return _dpFastRepair.Value;
            }
        }
        private float? _dpFuelAddKg;
        public float dpFuelAddKg
        {
            get
            {
                if (!_dpFuelAddKg.HasValue) _dpFuelAddKg = ValueSerializer.GetFloatValue(nameof(dpFuelAddKg), _data, _headers);
                return _dpFuelAddKg.Value;
            }
        }
        private float? _dpFuelFill;
        public float dpFuelFill
        {
            get
            {
                if (!_dpFuelFill.HasValue) _dpFuelFill = ValueSerializer.GetFloatValue(nameof(dpFuelFill), _data, _headers);
                return _dpFuelFill.Value;
            }
        }
        private float? _dpLFTireChange;
        public float dpLFTireChange
        {
            get
            {
                if (!_dpLFTireChange.HasValue) _dpLFTireChange = ValueSerializer.GetFloatValue(nameof(dpLFTireChange), _data, _headers);
                return _dpLFTireChange.Value;
            }
        }
        private float? _dpLFTireColdPress;
        public float dpLFTireColdPress
        {
            get
            {
                if (!_dpLFTireColdPress.HasValue) _dpLFTireColdPress = ValueSerializer.GetFloatValue(nameof(dpLFTireColdPress), _data, _headers);
                return _dpLFTireColdPress.Value;
            }
        }
        private float? _dpLRTireChange;
        public float dpLRTireChange
        {
            get
            {
                if (!_dpLRTireChange.HasValue) _dpLRTireChange = ValueSerializer.GetFloatValue(nameof(dpLRTireChange), _data, _headers);
                return _dpLRTireChange.Value;
            }
        }
        private float? _dpLRTireColdPress;
        public float dpLRTireColdPress
        {
            get
            {
                if (!_dpLRTireColdPress.HasValue) _dpLRTireColdPress = ValueSerializer.GetFloatValue(nameof(dpLRTireColdPress), _data, _headers);
                return _dpLRTireColdPress.Value;
            }
        }
        private float? _dpRFTireChange;
        public float dpRFTireChange
        {
            get
            {
                if (!_dpRFTireChange.HasValue) _dpRFTireChange = ValueSerializer.GetFloatValue(nameof(dpRFTireChange), _data, _headers);
                return _dpRFTireChange.Value;
            }
        }
        private float? _dpRFTireColdPress;
        public float dpRFTireColdPress
        {
            get
            {
                if (!_dpRFTireColdPress.HasValue) _dpRFTireColdPress = ValueSerializer.GetFloatValue(nameof(dpRFTireColdPress), _data, _headers);
                return _dpRFTireColdPress.Value;
            }
        }
        private float? _dpRRTireChange;
        public float dpRRTireChange
        {
            get
            {
                if (!_dpRRTireChange.HasValue) _dpRRTireChange = ValueSerializer.GetFloatValue(nameof(dpRRTireChange), _data, _headers);
                return _dpRRTireChange.Value;
            }
        }
        private float? _dpRRTireColdPress;
        public float dpRRTireColdPress
        {
            get
            {
                if (!_dpRRTireColdPress.HasValue) _dpRRTireColdPress = ValueSerializer.GetFloatValue(nameof(dpRRTireColdPress), _data, _headers);
                return _dpRRTireColdPress.Value;
            }
        }
        private float? _dpWindshieldTearoff;
        public float dpWindshieldTearoff
        {
            get
            {
                if (!_dpWindshieldTearoff.HasValue) _dpWindshieldTearoff = ValueSerializer.GetFloatValue(nameof(dpWindshieldTearoff), _data, _headers);
                return _dpWindshieldTearoff.Value;
            }
        }
        private bool? _driverMarker;
        public bool DriverMarker
        {
            get
            {
                if (!_driverMarker.HasValue) _driverMarker = ValueSerializer.GetBoolValue(nameof(DriverMarker), _data, _headers);
                return _driverMarker.Value;
            }
        }
        private int? _engineWarnings;
        public int EngineWarnings
        {
            get
            {
                if (!_engineWarnings.HasValue) _engineWarnings = ValueSerializer.GetIntValue(nameof(EngineWarnings), _data, _headers);
                return _engineWarnings.Value;
            }
        }
        private int? _enterExitReset;
        public int EnterExitReset
        {
            get
            {
                if (!_enterExitReset.HasValue) _enterExitReset = ValueSerializer.GetIntValue(nameof(EnterExitReset), _data, _headers);
                return _enterExitReset.Value;
            }
        }
        private int? _fastRepairAvailable;
        public int FastRepairAvailable
        {
            get
            {
                if (!_fastRepairAvailable.HasValue) _fastRepairAvailable = ValueSerializer.GetIntValue(nameof(FastRepairAvailable), _data, _headers);
                return _fastRepairAvailable.Value;
            }
        }
        private int? _fastRepairUsed;
        public int FastRepairUsed
        {
            get
            {
                if (!_fastRepairUsed.HasValue) _fastRepairUsed = ValueSerializer.GetIntValue(nameof(FastRepairUsed), _data, _headers);
                return _fastRepairUsed.Value;
            }
        }
        private float? _fogLevel;
        public float FogLevel
        {
            get
            {
                if (!_fogLevel.HasValue) _fogLevel = ValueSerializer.GetFloatValue(nameof(FogLevel), _data, _headers);
                return _fogLevel.Value;
            }
        }
        private float? _frameRate;
        public float FrameRate
        {
            get
            {
                if (!_frameRate.HasValue) _frameRate = ValueSerializer.GetFloatValue(nameof(FrameRate), _data, _headers);
                return _frameRate.Value;
            }
        }
        private float? _fuelLevel;
        public float FuelLevel
        {
            get
            {
                if (!_fuelLevel.HasValue) _fuelLevel = ValueSerializer.GetFloatValue(nameof(FuelLevel), _data, _headers);
                return _fuelLevel.Value;
            }
        }
        private float? _fuelLevelPct;
        public float FuelLevelPct
        {
            get
            {
                if (!_fuelLevelPct.HasValue) _fuelLevelPct = ValueSerializer.GetFloatValue(nameof(FuelLevelPct), _data, _headers);
                return _fuelLevelPct.Value;
            }
        }
        private float? _fuelPress;
        public float FuelPress
        {
            get
            {
                if (!_fuelPress.HasValue) _fuelPress = ValueSerializer.GetFloatValue(nameof(FuelPress), _data, _headers);
                return _fuelPress.Value;
            }
        }
        private float? _fuelUsePerHour;
        public float FuelUsePerHour
        {
            get
            {
                if (!_fuelUsePerHour.HasValue) _fuelUsePerHour = ValueSerializer.GetFloatValue(nameof(FuelUsePerHour), _data, _headers);
                return _fuelUsePerHour.Value;
            }
        }
        private int? _gear;
        public int Gear
        {
            get
            {
                if (!_gear.HasValue) _gear = ValueSerializer.GetIntValue(nameof(Gear), _data, _headers);
                return _gear.Value;
            }
        }
        private float? _handbrakeRaw;
        public float HandbrakeRaw
        {
            get
            {
                if (!_handbrakeRaw.HasValue) _handbrakeRaw = ValueSerializer.GetFloatValue(nameof(HandbrakeRaw), _data, _headers);
                return _handbrakeRaw.Value;
            }
        }
        private bool? _isDiskLoggingActive;
        public bool IsDiskLoggingActive
        {
            get
            {
                if (!_isDiskLoggingActive.HasValue) _isDiskLoggingActive = ValueSerializer.GetBoolValue(nameof(IsDiskLoggingActive), _data, _headers);
                return _isDiskLoggingActive.Value;
            }
        }
        private bool? _isDiskLoggingEnabled;
        public bool IsDiskLoggingEnabled
        {
            get
            {
                if (!_isDiskLoggingEnabled.HasValue) _isDiskLoggingEnabled = ValueSerializer.GetBoolValue(nameof(IsDiskLoggingEnabled), _data, _headers);
                return _isDiskLoggingEnabled.Value;
            }
        }
        private bool? _isInGarage;
        public bool IsInGarage
        {
            get
            {
                if (!_isInGarage.HasValue) _isInGarage = ValueSerializer.GetBoolValue(nameof(IsInGarage), _data, _headers);
                return _isInGarage.Value;
            }
        }
        private bool? _isOnTrack;
        public bool IsOnTrack
        {
            get
            {
                if (!_isOnTrack.HasValue) _isOnTrack = ValueSerializer.GetBoolValue(nameof(IsOnTrack), _data, _headers);
                return _isOnTrack.Value;
            }
        }
        private bool? _isOnTrackCar;
        public bool IsOnTrackCar
        {
            get
            {
                if (!_isOnTrackCar.HasValue) _isOnTrackCar = ValueSerializer.GetBoolValue(nameof(IsOnTrackCar), _data, _headers);
                return _isOnTrackCar.Value;
            }
        }
        private bool? _isReplayPlaying;
        public bool IsReplayPlaying
        {
            get
            {
                if (!_isReplayPlaying.HasValue) _isReplayPlaying = ValueSerializer.GetBoolValue(nameof(IsReplayPlaying), _data, _headers);
                return _isReplayPlaying.Value;
            }
        }
        private int? _lap;
        public int Lap
        {
            get
            {
                if (!_lap.HasValue) _lap = ValueSerializer.GetIntValue(nameof(Lap), _data, _headers);
                return _lap.Value;
            }
        }
        private int? _lapBestLap;
        public int LapBestLap
        {
            get
            {
                if (!_lapBestLap.HasValue) _lapBestLap = ValueSerializer.GetIntValue(nameof(LapBestLap), _data, _headers);
                return _lapBestLap.Value;
            }
        }
        private float? _lapBestLapTime;
        public float LapBestLapTime
        {
            get
            {
                if (!_lapBestLapTime.HasValue) _lapBestLapTime = ValueSerializer.GetFloatValue(nameof(LapBestLapTime), _data, _headers);
                return _lapBestLapTime.Value;
            }
        }
        private int? _lapBestNLapLap;
        public int LapBestNLapLap
        {
            get
            {
                if (!_lapBestNLapLap.HasValue) _lapBestNLapLap = ValueSerializer.GetIntValue(nameof(LapBestNLapLap), _data, _headers);
                return _lapBestNLapLap.Value;
            }
        }
        private float? _lapBestNLapTime;
        public float LapBestNLapTime
        {
            get
            {
                if (!_lapBestNLapTime.HasValue) _lapBestNLapTime = ValueSerializer.GetFloatValue(nameof(LapBestNLapTime), _data, _headers);
                return _lapBestNLapTime.Value;
            }
        }
        private int? _lapCompleted;
        public int LapCompleted
        {
            get
            {
                if (!_lapCompleted.HasValue) _lapCompleted = ValueSerializer.GetIntValue(nameof(LapCompleted), _data, _headers);
                return _lapCompleted.Value;
            }
        }
        private float? _lapCurrentLapTime;
        public float LapCurrentLapTime
        {
            get
            {
                if (!_lapCurrentLapTime.HasValue) _lapCurrentLapTime = ValueSerializer.GetFloatValue(nameof(LapCurrentLapTime), _data, _headers);
                return _lapCurrentLapTime.Value;
            }
        }
        private float? _lapDeltaToBestLap;
        public float LapDeltaToBestLap
        {
            get
            {
                if (!_lapDeltaToBestLap.HasValue) _lapDeltaToBestLap = ValueSerializer.GetFloatValue(nameof(LapDeltaToBestLap), _data, _headers);
                return _lapDeltaToBestLap.Value;
            }
        }
        private float? _lapDeltaToBestLap_DD;
        public float LapDeltaToBestLap_DD
        {
            get
            {
                if (!_lapDeltaToBestLap_DD.HasValue) _lapDeltaToBestLap_DD = ValueSerializer.GetFloatValue(nameof(LapDeltaToBestLap_DD), _data, _headers);
                return _lapDeltaToBestLap_DD.Value;
            }
        }
        private bool? _lapDeltaToBestLap_OK;
        public bool LapDeltaToBestLap_OK
        {
            get
            {
                if (!_lapDeltaToBestLap_OK.HasValue) _lapDeltaToBestLap_OK = ValueSerializer.GetBoolValue(nameof(LapDeltaToBestLap_OK), _data, _headers);
                return _lapDeltaToBestLap_OK.Value;
            }
        }
        private float? _lapDeltaToOptimalLap;
        public float LapDeltaToOptimalLap
        {
            get
            {
                if (!_lapDeltaToOptimalLap.HasValue) _lapDeltaToOptimalLap = ValueSerializer.GetFloatValue(nameof(LapDeltaToOptimalLap), _data, _headers);
                return _lapDeltaToOptimalLap.Value;
            }
        }
        private float? _lapDeltaToOptimalLap_DD;
        public float LapDeltaToOptimalLap_DD
        {
            get
            {
                if (!_lapDeltaToOptimalLap_DD.HasValue) _lapDeltaToOptimalLap_DD = ValueSerializer.GetFloatValue(nameof(LapDeltaToOptimalLap_DD), _data, _headers);
                return _lapDeltaToOptimalLap_DD.Value;
            }
        }
        private bool? _lapDeltaToOptimalLap_OK;
        public bool LapDeltaToOptimalLap_OK
        {
            get
            {
                if (!_lapDeltaToOptimalLap_OK.HasValue) _lapDeltaToOptimalLap_OK = ValueSerializer.GetBoolValue(nameof(LapDeltaToOptimalLap_OK), _data, _headers);
                return _lapDeltaToOptimalLap_OK.Value;
            }
        }
        private float? _lapDeltaToSessionBestLap;
        public float LapDeltaToSessionBestLap
        {
            get
            {
                if (!_lapDeltaToSessionBestLap.HasValue) _lapDeltaToSessionBestLap = ValueSerializer.GetFloatValue(nameof(LapDeltaToSessionBestLap), _data, _headers);
                return _lapDeltaToSessionBestLap.Value;
            }
        }
        private float? _lapDeltaToSessionBestLap_DD;
        public float LapDeltaToSessionBestLap_DD
        {
            get
            {
                if (!_lapDeltaToSessionBestLap_DD.HasValue) _lapDeltaToSessionBestLap_DD = ValueSerializer.GetFloatValue(nameof(LapDeltaToSessionBestLap_DD), _data, _headers);
                return _lapDeltaToSessionBestLap_DD.Value;
            }
        }
        private bool? _lapDeltaToSessionBestLap_OK;
        public bool LapDeltaToSessionBestLap_OK
        {
            get
            {
                if (!_lapDeltaToSessionBestLap_OK.HasValue) _lapDeltaToSessionBestLap_OK = ValueSerializer.GetBoolValue(nameof(LapDeltaToSessionBestLap_OK), _data, _headers);
                return _lapDeltaToSessionBestLap_OK.Value;
            }
        }
        private float? _lapDeltaToSessionLastLap;
        public float LapDeltaToSessionLastLap
        {
            get
            {
                if (!_lapDeltaToSessionLastLap.HasValue) _lapDeltaToSessionLastLap = ValueSerializer.GetFloatValue(nameof(LapDeltaToSessionLastLap), _data, _headers);
                return _lapDeltaToSessionLastLap.Value;
            }
        }
        private float? _lapDeltaToSessionLastLap_DD;
        public float LapDeltaToSessionLastLap_DD
        {
            get
            {
                if (!_lapDeltaToSessionLastLap_DD.HasValue) _lapDeltaToSessionLastLap_DD = ValueSerializer.GetFloatValue(nameof(LapDeltaToSessionLastLap_DD), _data, _headers);
                return _lapDeltaToSessionLastLap_DD.Value;
            }
        }
        private bool? _lapDeltaToSessionLastLap_OK;
        public bool LapDeltaToSessionLastLap_OK
        {
            get
            {
                if (!_lapDeltaToSessionLastLap_OK.HasValue) _lapDeltaToSessionLastLap_OK = ValueSerializer.GetBoolValue(nameof(LapDeltaToSessionLastLap_OK), _data, _headers);
                return _lapDeltaToSessionLastLap_OK.Value;
            }
        }
        private float? _lapDeltaToSessionOptimalLap;
        public float LapDeltaToSessionOptimalLap
        {
            get
            {
                if (!_lapDeltaToSessionOptimalLap.HasValue) _lapDeltaToSessionOptimalLap = ValueSerializer.GetFloatValue(nameof(LapDeltaToSessionOptimalLap), _data, _headers);
                return _lapDeltaToSessionOptimalLap.Value;
            }
        }
        private float? _lapDeltaToSessionOptimalLap_DD;
        public float LapDeltaToSessionOptimalLap_DD
        {
            get
            {
                if (!_lapDeltaToSessionOptimalLap_DD.HasValue) _lapDeltaToSessionOptimalLap_DD = ValueSerializer.GetFloatValue(nameof(LapDeltaToSessionOptimalLap_DD), _data, _headers);
                return _lapDeltaToSessionOptimalLap_DD.Value;
            }
        }
        private bool? _lapDeltaToSessionOptimalLap_OK;
        public bool LapDeltaToSessionOptimalLap_OK
        {
            get
            {
                if (!_lapDeltaToSessionOptimalLap_OK.HasValue) _lapDeltaToSessionOptimalLap_OK = ValueSerializer.GetBoolValue(nameof(LapDeltaToSessionOptimalLap_OK), _data, _headers);
                return _lapDeltaToSessionOptimalLap_OK.Value;
            }
        }
        private float? _lapDist;
        public float LapDist
        {
            get
            {
                if (!_lapDist.HasValue) _lapDist = ValueSerializer.GetFloatValue(nameof(LapDist), _data, _headers);
                return _lapDist.Value;
            }
        }
        private float? _lapDistPct;
        public float LapDistPct
        {
            get
            {
                if (!_lapDistPct.HasValue) _lapDistPct = ValueSerializer.GetFloatValue(nameof(LapDistPct), _data, _headers);
                return _lapDistPct.Value;
            }
        }
        private int? _lapLasNLapSeq;
        public int LapLasNLapSeq
        {
            get
            {
                if (!_lapLasNLapSeq.HasValue) _lapLasNLapSeq = ValueSerializer.GetIntValue(nameof(LapLasNLapSeq), _data, _headers);
                return _lapLasNLapSeq.Value;
            }
        }
        private float? _lapLastLapTime;
        public float LapLastLapTime
        {
            get
            {
                if (!_lapLastLapTime.HasValue) _lapLastLapTime = ValueSerializer.GetFloatValue(nameof(LapLastLapTime), _data, _headers);
                return _lapLastLapTime.Value;
            }
        }
        private float? _lapLastNLapTime;
        public float LapLastNLapTime
        {
            get
            {
                if (!_lapLastNLapTime.HasValue) _lapLastNLapTime = ValueSerializer.GetFloatValue(nameof(LapLastNLapTime), _data, _headers);
                return _lapLastNLapTime.Value;
            }
        }
        private float? _latAccel;
        public float LatAccel
        {
            get
            {
                if (!_latAccel.HasValue) _latAccel = ValueSerializer.GetFloatValue(nameof(LatAccel), _data, _headers);
                return _latAccel.Value;
            }
        }
        private float[] _latAccel_ST;
        public float[] LatAccel_ST
        {
            get
            {
                if (_latAccel_ST == null) _latAccel_ST = ValueSerializer.GetFloatArrayValue(nameof(LatAccel_ST), _data, _headers);
                return _latAccel_ST;
            }
        }
        private float? _lFbrakeLinePress;
        public float LFbrakeLinePress
        {
            get
            {
                if (!_lFbrakeLinePress.HasValue) _lFbrakeLinePress = ValueSerializer.GetFloatValue(nameof(LFbrakeLinePress), _data, _headers);
                return _lFbrakeLinePress.Value;
            }
        }
        private float? _lFcoldPressure;
        public float LFcoldPressure
        {
            get
            {
                if (!_lFcoldPressure.HasValue) _lFcoldPressure = ValueSerializer.GetFloatValue(nameof(LFcoldPressure), _data, _headers);
                return _lFcoldPressure.Value;
            }
        }
        private float? _lFshockDefl;
        public float LFshockDefl
        {
            get
            {
                if (!_lFshockDefl.HasValue) _lFshockDefl = ValueSerializer.GetFloatValue(nameof(LFshockDefl), _data, _headers);
                return _lFshockDefl.Value;
            }
        }
        private float[] _lFshockDefl_ST;
        public float[] LFshockDefl_ST
        {
            get
            {
                if (_lFshockDefl_ST == null) _lFshockDefl_ST = ValueSerializer.GetFloatArrayValue(nameof(LFshockDefl_ST), _data, _headers);
                return _lFshockDefl_ST;
            }
        }
        private float? _lFshockVel;
        public float LFshockVel
        {
            get
            {
                if (!_lFshockVel.HasValue) _lFshockVel = ValueSerializer.GetFloatValue(nameof(LFshockVel), _data, _headers);
                return _lFshockVel.Value;
            }
        }
        private float[] _lFshockVel_ST;
        public float[] LFshockVel_ST
        {
            get
            {
                if (_lFshockVel_ST == null) _lFshockVel_ST = ValueSerializer.GetFloatArrayValue(nameof(LFshockVel_ST), _data, _headers);
                return _lFshockVel_ST;
            }
        }
        private float? _lFtempCL;
        public float LFtempCL
        {
            get
            {
                if (!_lFtempCL.HasValue) _lFtempCL = ValueSerializer.GetFloatValue(nameof(LFtempCL), _data, _headers);
                return _lFtempCL.Value;
            }
        }
        private float? _lFtempCM;
        public float LFtempCM
        {
            get
            {
                if (!_lFtempCM.HasValue) _lFtempCM = ValueSerializer.GetFloatValue(nameof(LFtempCM), _data, _headers);
                return _lFtempCM.Value;
            }
        }
        private float? _lFtempCR;
        public float LFtempCR
        {
            get
            {
                if (!_lFtempCR.HasValue) _lFtempCR = ValueSerializer.GetFloatValue(nameof(LFtempCR), _data, _headers);
                return _lFtempCR.Value;
            }
        }
        private float? _lFwearL;
        public float LFwearL
        {
            get
            {
                if (!_lFwearL.HasValue) _lFwearL = ValueSerializer.GetFloatValue(nameof(LFwearL), _data, _headers);
                return _lFwearL.Value;
            }
        }
        private float? _lFwearM;
        public float LFwearM
        {
            get
            {
                if (!_lFwearM.HasValue) _lFwearM = ValueSerializer.GetFloatValue(nameof(LFwearM), _data, _headers);
                return _lFwearM.Value;
            }
        }
        private float? _lFwearR;
        public float LFwearR
        {
            get
            {
                if (!_lFwearR.HasValue) _lFwearR = ValueSerializer.GetFloatValue(nameof(LFwearR), _data, _headers);
                return _lFwearR.Value;
            }
        }
        private bool? _loadNumTextures;
        public bool LoadNumTextures
        {
            get
            {
                if (!_loadNumTextures.HasValue) _loadNumTextures = ValueSerializer.GetBoolValue(nameof(LoadNumTextures), _data, _headers);
                return _loadNumTextures.Value;
            }
        }
        private float? _longAccel;
        public float LongAccel
        {
            get
            {
                if (!_longAccel.HasValue) _longAccel = ValueSerializer.GetFloatValue(nameof(LongAccel), _data, _headers);
                return _longAccel.Value;
            }
        }
        private float[] _longAccel_ST;
        public float[] LongAccel_ST
        {
            get
            {
                if (_longAccel_ST == null) _longAccel_ST = ValueSerializer.GetFloatArrayValue(nameof(LongAccel_ST), _data, _headers);
                return _longAccel_ST;
            }
        }
        private float? _lRbrakeLinePress;
        public float LRbrakeLinePress
        {
            get
            {
                if (!_lRbrakeLinePress.HasValue) _lRbrakeLinePress = ValueSerializer.GetFloatValue(nameof(LRbrakeLinePress), _data, _headers);
                return _lRbrakeLinePress.Value;
            }
        }
        private float? _lRcoldPressure;
        public float LRcoldPressure
        {
            get
            {
                if (!_lRcoldPressure.HasValue) _lRcoldPressure = ValueSerializer.GetFloatValue(nameof(LRcoldPressure), _data, _headers);
                return _lRcoldPressure.Value;
            }
        }
        private float? _lRshockDefl;
        public float LRshockDefl
        {
            get
            {
                if (!_lRshockDefl.HasValue) _lRshockDefl = ValueSerializer.GetFloatValue(nameof(LRshockDefl), _data, _headers);
                return _lRshockDefl.Value;
            }
        }
        private float[] _lRshockDefl_ST;
        public float[] LRshockDefl_ST
        {
            get
            {
                if (_lRshockDefl_ST == null) _lRshockDefl_ST = ValueSerializer.GetFloatArrayValue(nameof(LRshockDefl_ST), _data, _headers);
                return _lRshockDefl_ST;
            }
        }
        private float? _lRshockVel;
        public float LRshockVel
        {
            get
            {
                if (!_lRshockVel.HasValue) _lRshockVel = ValueSerializer.GetFloatValue(nameof(LRshockVel), _data, _headers);
                return _lRshockVel.Value;
            }
        }
        private float[] _lRshockVel_ST;
        public float[] LRshockVel_ST
        {
            get
            {
                if (_lRshockVel_ST == null) _lRshockVel_ST = ValueSerializer.GetFloatArrayValue(nameof(LRshockVel_ST), _data, _headers);
                return _lRshockVel_ST;
            }
        }
        private float? _lRtempCL;
        public float LRtempCL
        {
            get
            {
                if (!_lRtempCL.HasValue) _lRtempCL = ValueSerializer.GetFloatValue(nameof(LRtempCL), _data, _headers);
                return _lRtempCL.Value;
            }
        }
        private float? _lRtempCM;
        public float LRtempCM
        {
            get
            {
                if (!_lRtempCM.HasValue) _lRtempCM = ValueSerializer.GetFloatValue(nameof(LRtempCM), _data, _headers);
                return _lRtempCM.Value;
            }
        }
        private float? _lRtempCR;
        public float LRtempCR
        {
            get
            {
                if (!_lRtempCR.HasValue) _lRtempCR = ValueSerializer.GetFloatValue(nameof(LRtempCR), _data, _headers);
                return _lRtempCR.Value;
            }
        }
        private float? _lRwearL;
        public float LRwearL
        {
            get
            {
                if (!_lRwearL.HasValue) _lRwearL = ValueSerializer.GetFloatValue(nameof(LRwearL), _data, _headers);
                return _lRwearL.Value;
            }
        }
        private float? _lRwearM;
        public float LRwearM
        {
            get
            {
                if (!_lRwearM.HasValue) _lRwearM = ValueSerializer.GetFloatValue(nameof(LRwearM), _data, _headers);
                return _lRwearM.Value;
            }
        }
        private float? _lRwearR;
        public float LRwearR
        {
            get
            {
                if (!_lRwearR.HasValue) _lRwearR = ValueSerializer.GetFloatValue(nameof(LRwearR), _data, _headers);
                return _lRwearR.Value;
            }
        }
        private float? _manifoldPress;
        public float ManifoldPress
        {
            get
            {
                if (!_manifoldPress.HasValue) _manifoldPress = ValueSerializer.GetFloatValue(nameof(ManifoldPress), _data, _headers);
                return _manifoldPress.Value;
            }
        }
        private bool? _manualBoost;
        public bool ManualBoost
        {
            get
            {
                if (!_manualBoost.HasValue) _manualBoost = ValueSerializer.GetBoolValue(nameof(ManualBoost), _data, _headers);
                return _manualBoost.Value;
            }
        }
        private bool? _manualNoBoost;
        public bool ManualNoBoost
        {
            get
            {
                if (!_manualNoBoost.HasValue) _manualNoBoost = ValueSerializer.GetBoolValue(nameof(ManualNoBoost), _data, _headers);
                return _manualNoBoost.Value;
            }
        }
        private float? _oilLevel;
        public float OilLevel
        {
            get
            {
                if (!_oilLevel.HasValue) _oilLevel = ValueSerializer.GetFloatValue(nameof(OilLevel), _data, _headers);
                return _oilLevel.Value;
            }
        }
        private float? _oilPress;
        public float OilPress
        {
            get
            {
                if (!_oilPress.HasValue) _oilPress = ValueSerializer.GetFloatValue(nameof(OilPress), _data, _headers);
                return _oilPress.Value;
            }
        }
        private float? _oilTemp;
        public float OilTemp
        {
            get
            {
                if (!_oilTemp.HasValue) _oilTemp = ValueSerializer.GetFloatValue(nameof(OilTemp), _data, _headers);
                return _oilTemp.Value;
            }
        }
        private bool? _okToReloadTextures;
        public bool OkToReloadTextures
        {
            get
            {
                if (!_okToReloadTextures.HasValue) _okToReloadTextures = ValueSerializer.GetBoolValue(nameof(OkToReloadTextures), _data, _headers);
                return _okToReloadTextures.Value;
            }
        }
        private bool? _onPitRoad;
        public bool OnPitRoad
        {
            get
            {
                if (!_onPitRoad.HasValue) _onPitRoad = ValueSerializer.GetBoolValue(nameof(OnPitRoad), _data, _headers);
                return _onPitRoad.Value;
            }
        }
        private float? _pitch;
        public float Pitch
        {
            get
            {
                if (!_pitch.HasValue) _pitch = ValueSerializer.GetFloatValue(nameof(Pitch), _data, _headers);
                return _pitch.Value;
            }
        }
        private float? _pitchRate;
        public float PitchRate
        {
            get
            {
                if (!_pitchRate.HasValue) _pitchRate = ValueSerializer.GetFloatValue(nameof(PitchRate), _data, _headers);
                return _pitchRate.Value;
            }
        }
        private float[] _pitchRate_ST;
        public float[] PitchRate_ST
        {
            get
            {
                if (_pitchRate_ST == null) _pitchRate_ST = ValueSerializer.GetFloatArrayValue(nameof(PitchRate_ST), _data, _headers);
                return _pitchRate_ST;
            }
        }
        private float? _pitOptRepairLeft;
        public float PitOptRepairLeft
        {
            get
            {
                if (!_pitOptRepairLeft.HasValue) _pitOptRepairLeft = ValueSerializer.GetFloatValue(nameof(PitOptRepairLeft), _data, _headers);
                return _pitOptRepairLeft.Value;
            }
        }
        private float? _pitRepairLeft;
        public float PitRepairLeft
        {
            get
            {
                if (!_pitRepairLeft.HasValue) _pitRepairLeft = ValueSerializer.GetFloatValue(nameof(PitRepairLeft), _data, _headers);
                return _pitRepairLeft.Value;
            }
        }
        private bool? _pitsOpen;
        public bool PitsOpen
        {
            get
            {
                if (!_pitsOpen.HasValue) _pitsOpen = ValueSerializer.GetBoolValue(nameof(PitsOpen), _data, _headers);
                return _pitsOpen.Value;
            }
        }
        private bool? _pitstopActive;
        public bool PitstopActive
        {
            get
            {
                if (!_pitstopActive.HasValue) _pitstopActive = ValueSerializer.GetBoolValue(nameof(PitstopActive), _data, _headers);
                return _pitstopActive.Value;
            }
        }
        private int? _pitSvFlags;
        public int PitSvFlags
        {
            get
            {
                if (!_pitSvFlags.HasValue) _pitSvFlags = ValueSerializer.GetIntValue(nameof(PitSvFlags), _data, _headers);
                return _pitSvFlags.Value;
            }
        }
        private float? _pitSvFuel;
        public float PitSvFuel
        {
            get
            {
                if (!_pitSvFuel.HasValue) _pitSvFuel = ValueSerializer.GetFloatValue(nameof(PitSvFuel), _data, _headers);
                return _pitSvFuel.Value;
            }
        }
        private float? _pitSvLFP;
        public float PitSvLFP
        {
            get
            {
                if (!_pitSvLFP.HasValue) _pitSvLFP = ValueSerializer.GetFloatValue(nameof(PitSvLFP), _data, _headers);
                return _pitSvLFP.Value;
            }
        }
        private float? _pitSvLRP;
        public float PitSvLRP
        {
            get
            {
                if (!_pitSvLRP.HasValue) _pitSvLRP = ValueSerializer.GetFloatValue(nameof(PitSvLRP), _data, _headers);
                return _pitSvLRP.Value;
            }
        }
        private float? _pitSvRFP;
        public float PitSvRFP
        {
            get
            {
                if (!_pitSvRFP.HasValue) _pitSvRFP = ValueSerializer.GetFloatValue(nameof(PitSvRFP), _data, _headers);
                return _pitSvRFP.Value;
            }
        }
        private float? _pitSvRRP;
        public float PitSvRRP
        {
            get
            {
                if (!_pitSvRRP.HasValue) _pitSvRRP = ValueSerializer.GetFloatValue(nameof(PitSvRRP), _data, _headers);
                return _pitSvRRP.Value;
            }
        }
        private int? _playerCarClassPosition;
        public int PlayerCarClassPosition
        {
            get
            {
                if (!_playerCarClassPosition.HasValue) _playerCarClassPosition = ValueSerializer.GetIntValue(nameof(PlayerCarClassPosition), _data, _headers);
                return _playerCarClassPosition.Value;
            }
        }
        private int? _playerCarDriverIncidentCount;
        public int PlayerCarDriverIncidentCount
        {
            get
            {
                if (!_playerCarDriverIncidentCount.HasValue) _playerCarDriverIncidentCount = ValueSerializer.GetIntValue(nameof(PlayerCarDriverIncidentCount), _data, _headers);
                return _playerCarDriverIncidentCount.Value;
            }
        }
        private int? _playerCarIdx;
        public int PlayerCarIdx
        {
            get
            {
                if (!_playerCarIdx.HasValue) _playerCarIdx = ValueSerializer.GetIntValue(nameof(PlayerCarIdx), _data, _headers);
                return _playerCarIdx.Value;
            }
        }
        private bool? _playerCarInPitStall;
        public bool PlayerCarInPitStall
        {
            get
            {
                if (!_playerCarInPitStall.HasValue) _playerCarInPitStall = ValueSerializer.GetBoolValue(nameof(PlayerCarInPitStall), _data, _headers);
                return _playerCarInPitStall.Value;
            }
        }
        private int? _playerCarMyIncidentCount;
        public int PlayerCarMyIncidentCount
        {
            get
            {
                if (!_playerCarMyIncidentCount.HasValue) _playerCarMyIncidentCount = ValueSerializer.GetIntValue(nameof(PlayerCarMyIncidentCount), _data, _headers);
                return _playerCarMyIncidentCount.Value;
            }
        }
        private int? _playerCarPitSvStatus;
        public int PlayerCarPitSvStatus
        {
            get
            {
                if (!_playerCarPitSvStatus.HasValue) _playerCarPitSvStatus = ValueSerializer.GetIntValue(nameof(PlayerCarPitSvStatus), _data, _headers);
                return _playerCarPitSvStatus.Value;
            }
        }
        private int? _playerCarPosition;
        public int PlayerCarPosition
        {
            get
            {
                if (!_playerCarPosition.HasValue) _playerCarPosition = ValueSerializer.GetIntValue(nameof(PlayerCarPosition), _data, _headers);
                return _playerCarPosition.Value;
            }
        }
        private float? _playerCarPowerAdjust;
        public float PlayerCarPowerAdjust
        {
            get
            {
                if (!_playerCarPowerAdjust.HasValue) _playerCarPowerAdjust = ValueSerializer.GetFloatValue(nameof(PlayerCarPowerAdjust), _data, _headers);
                return _playerCarPowerAdjust.Value;
            }
        }
        private int? _playerCarTeamIncidentCount;
        public int PlayerCarTeamIncidentCount
        {
            get
            {
                if (!_playerCarTeamIncidentCount.HasValue) _playerCarTeamIncidentCount = ValueSerializer.GetIntValue(nameof(PlayerCarTeamIncidentCount), _data, _headers);
                return _playerCarTeamIncidentCount.Value;
            }
        }
        private float? _playerCarTowTime;
        public float PlayerCarTowTime
        {
            get
            {
                if (!_playerCarTowTime.HasValue) _playerCarTowTime = ValueSerializer.GetFloatValue(nameof(PlayerCarTowTime), _data, _headers);
                return _playerCarTowTime.Value;
            }
        }
        private float? _playerCarWeightPenalty;
        public float PlayerCarWeightPenalty
        {
            get
            {
                if (!_playerCarWeightPenalty.HasValue) _playerCarWeightPenalty = ValueSerializer.GetFloatValue(nameof(PlayerCarWeightPenalty), _data, _headers);
                return _playerCarWeightPenalty.Value;
            }
        }
        private int? _playerTrackSurface;
        public int PlayerTrackSurface
        {
            get
            {
                if (!_playerTrackSurface.HasValue) _playerTrackSurface = ValueSerializer.GetIntValue(nameof(PlayerTrackSurface), _data, _headers);
                return _playerTrackSurface.Value;
            }
        }
        private int? _playerTrackSurfaceMaterial;
        public int PlayerTrackSurfaceMaterial
        {
            get
            {
                if (!_playerTrackSurfaceMaterial.HasValue) _playerTrackSurfaceMaterial = ValueSerializer.GetIntValue(nameof(PlayerTrackSurfaceMaterial), _data, _headers);
                return _playerTrackSurfaceMaterial.Value;
            }
        }
        private bool? _pushToPass;
        public bool PushToPass
        {
            get
            {
                if (!_pushToPass.HasValue) _pushToPass = ValueSerializer.GetBoolValue(nameof(PushToPass), _data, _headers);
                return _pushToPass.Value;
            }
        }
        private int? _raceLaps;
        public int RaceLaps
        {
            get
            {
                if (!_raceLaps.HasValue) _raceLaps = ValueSerializer.GetIntValue(nameof(RaceLaps), _data, _headers);
                return _raceLaps.Value;
            }
        }
        private int? _radioTransmitCarIdx;
        public int RadioTransmitCarIdx
        {
            get
            {
                if (!_radioTransmitCarIdx.HasValue) _radioTransmitCarIdx = ValueSerializer.GetIntValue(nameof(RadioTransmitCarIdx), _data, _headers);
                return _radioTransmitCarIdx.Value;
            }
        }
        private int? _radioTransmitFrequencyIdx;
        public int RadioTransmitFrequencyIdx
        {
            get
            {
                if (!_radioTransmitFrequencyIdx.HasValue) _radioTransmitFrequencyIdx = ValueSerializer.GetIntValue(nameof(RadioTransmitFrequencyIdx), _data, _headers);
                return _radioTransmitFrequencyIdx.Value;
            }
        }
        private int? _radioTransmitRadioIdx;
        public int RadioTransmitRadioIdx
        {
            get
            {
                if (!_radioTransmitRadioIdx.HasValue) _radioTransmitRadioIdx = ValueSerializer.GetIntValue(nameof(RadioTransmitRadioIdx), _data, _headers);
                return _radioTransmitRadioIdx.Value;
            }
        }
        private float? _relativeHumidity;
        public float RelativeHumidity
        {
            get
            {
                if (!_relativeHumidity.HasValue) _relativeHumidity = ValueSerializer.GetFloatValue(nameof(RelativeHumidity), _data, _headers);
                return _relativeHumidity.Value;
            }
        }
        private int? _replayFrameNum;
        public int ReplayFrameNum
        {
            get
            {
                if (!_replayFrameNum.HasValue) _replayFrameNum = ValueSerializer.GetIntValue(nameof(ReplayFrameNum), _data, _headers);
                return _replayFrameNum.Value;
            }
        }
        private int? _replayFrameNumEnd;
        public int ReplayFrameNumEnd
        {
            get
            {
                if (!_replayFrameNumEnd.HasValue) _replayFrameNumEnd = ValueSerializer.GetIntValue(nameof(ReplayFrameNumEnd), _data, _headers);
                return _replayFrameNumEnd.Value;
            }
        }
        private bool? _replayPlaySlowMotion;
        public bool ReplayPlaySlowMotion
        {
            get
            {
                if (!_replayPlaySlowMotion.HasValue) _replayPlaySlowMotion = ValueSerializer.GetBoolValue(nameof(ReplayPlaySlowMotion), _data, _headers);
                return _replayPlaySlowMotion.Value;
            }
        }
        private int? _replayPlaySpeed;
        public int ReplayPlaySpeed
        {
            get
            {
                if (!_replayPlaySpeed.HasValue) _replayPlaySpeed = ValueSerializer.GetIntValue(nameof(ReplayPlaySpeed), _data, _headers);
                return _replayPlaySpeed.Value;
            }
        }
        private int? _replaySessionNum;
        public int ReplaySessionNum
        {
            get
            {
                if (!_replaySessionNum.HasValue) _replaySessionNum = ValueSerializer.GetIntValue(nameof(ReplaySessionNum), _data, _headers);
                return _replaySessionNum.Value;
            }
        }
        public double? _replaySessionTime;
        public double ReplaySessionTime
        {
            get
            {
                if (!_replaySessionTime.HasValue) _replaySessionTime = ValueSerializer.GetDoubleValue(nameof(ReplaySessionTime), _data, _headers);
                return _replaySessionTime.Value;
            }
        }
        private float? _rFbrakeLinePress;
        public float RFbrakeLinePress
        {
            get
            {
                if (!_rFbrakeLinePress.HasValue) _rFbrakeLinePress = ValueSerializer.GetFloatValue(nameof(RFbrakeLinePress), _data, _headers);
                return _rFbrakeLinePress.Value;
            }
        }
        private float? _rFcoldPressure;
        public float RFcoldPressure
        {
            get
            {
                if (!_rFcoldPressure.HasValue) _rFcoldPressure = ValueSerializer.GetFloatValue(nameof(RFcoldPressure), _data, _headers);
                return _rFcoldPressure.Value;
            }
        }
        private float? _rFshockDefl;
        public float RFshockDefl
        {
            get
            {
                if (!_rFshockDefl.HasValue) _rFshockDefl = ValueSerializer.GetFloatValue(nameof(RFshockDefl), _data, _headers);
                return _rFshockDefl.Value;
            }
        }
        private float[] _rFshockDefl_ST;
        public float[] RFshockDefl_ST
        {
            get
            {
                if (_rFshockDefl_ST == null) _rFshockDefl_ST = ValueSerializer.GetFloatArrayValue(nameof(RFshockDefl_ST), _data, _headers);
                return _rFshockDefl_ST;
            }
        }
        private float? _rFshockVel;
        public float RFshockVel
        {
            get
            {
                if (!_rFshockVel.HasValue) _rFshockVel = ValueSerializer.GetFloatValue(nameof(RFshockVel), _data, _headers);
                return _rFshockVel.Value;
            }
        }
        private float[] _rFshockVel_ST;
        public float[] RFshockVel_ST
        {
            get
            {
                if (_rFshockVel_ST == null) _rFshockVel_ST = ValueSerializer.GetFloatArrayValue(nameof(RFshockVel_ST), _data, _headers);
                return _rFshockVel_ST;
            }
        }
        private float? _rFtempCL;
        public float RFtempCL
        {
            get
            {
                if (!_rFtempCL.HasValue) _rFtempCL = ValueSerializer.GetFloatValue(nameof(RFtempCL), _data, _headers);
                return _rFtempCL.Value;
            }
        }
        private float? _rFtempCM;
        public float RFtempCM
        {
            get
            {
                if (!_rFtempCM.HasValue) _rFtempCM = ValueSerializer.GetFloatValue(nameof(RFtempCM), _data, _headers);
                return _rFtempCM.Value;
            }
        }
        private float? _rFtempCR;
        public float RFtempCR
        {
            get
            {
                if (!_rFtempCR.HasValue) _rFtempCR = ValueSerializer.GetFloatValue(nameof(RFtempCR), _data, _headers);
                return _rFtempCR.Value;
            }
        }
        private float? _rFwearL;
        public float RFwearL
        {
            get
            {
                if (!_rFwearL.HasValue) _rFwearL = ValueSerializer.GetFloatValue(nameof(RFwearL), _data, _headers);
                return _rFwearL.Value;
            }
        }
        private float? _rFwearM;
        public float RFwearM
        {
            get
            {
                if (!_rFwearM.HasValue) _rFwearM = ValueSerializer.GetFloatValue(nameof(RFwearM), _data, _headers);
                return _rFwearM.Value;
            }
        }
        private float? _rFwearR;
        public float RFwearR
        {
            get
            {
                if (!_rFwearR.HasValue) _rFwearR = ValueSerializer.GetFloatValue(nameof(RFwearR), _data, _headers);
                return _rFwearR.Value;
            }
        }
        private float? _roll;
        public float Roll
        {
            get
            {
                if (!_roll.HasValue) _roll = ValueSerializer.GetFloatValue(nameof(Roll), _data, _headers);
                return _roll.Value;
            }
        }
        private float? _rollRate;
        public float RollRate
        {
            get
            {
                if (!_rollRate.HasValue) _rollRate = ValueSerializer.GetFloatValue(nameof(RollRate), _data, _headers);
                return _rollRate.Value;
            }
        }
        private float[] _rollRate_ST;
        public float[] RollRate_ST
        {
            get
            {
                if (_rollRate_ST == null) _rollRate_ST = ValueSerializer.GetFloatArrayValue(nameof(RollRate_ST), _data, _headers);
                return _rollRate_ST;
            }
        }
        private float? _rPM;
        public float RPM
        {
            get
            {
                if (!_rPM.HasValue) _rPM = ValueSerializer.GetFloatValue(nameof(RPM), _data, _headers);
                return _rPM.Value;
            }
        }
        private float? _rRbrakeLinePress;
        public float RRbrakeLinePress
        {
            get
            {
                if (!_rRbrakeLinePress.HasValue) _rRbrakeLinePress = ValueSerializer.GetFloatValue(nameof(RRbrakeLinePress), _data, _headers);
                return _rRbrakeLinePress.Value;
            }
        }
        private float? _rRcoldPressure;
        public float RRcoldPressure
        {
            get
            {
                if (!_rRcoldPressure.HasValue) _rRcoldPressure = ValueSerializer.GetFloatValue(nameof(RRcoldPressure), _data, _headers);
                return _rRcoldPressure.Value;
            }
        }
        private float? _rRshockDefl;
        public float RRshockDefl
        {
            get
            {
                if (!_rRshockDefl.HasValue) _rRshockDefl = ValueSerializer.GetFloatValue(nameof(RRshockDefl), _data, _headers);
                return _rRshockDefl.Value;
            }
        }
        private float[] _rRshockDefl_ST;
        public float[] RRshockDefl_ST
        {
            get
            {
                if (_rRshockDefl_ST == null) _rRshockDefl_ST = ValueSerializer.GetFloatArrayValue(nameof(RRshockDefl_ST), _data, _headers);
                return _rRshockDefl_ST;
            }
        }
        private float? _rRshockVel;
        public float RRshockVel
        {
            get
            {
                if (!_rRshockVel.HasValue) _rRshockVel = ValueSerializer.GetFloatValue(nameof(RRshockVel), _data, _headers);
                return _rRshockVel.Value;
            }
        }
        private float[] _rRshockVel_ST;
        public float[] RRshockVel_ST
        {
            get
            {
                if (_rRshockVel_ST == null) _rRshockVel_ST = ValueSerializer.GetFloatArrayValue(nameof(RRshockVel_ST), _data, _headers);
                return _rRshockVel_ST;
            }
        }
        private float? _rRtempCL;
        public float RRtempCL
        {
            get
            {
                if (!_rRtempCL.HasValue) _rRtempCL = ValueSerializer.GetFloatValue(nameof(RRtempCL), _data, _headers);
                return _rRtempCL.Value;
            }
        }
        private float? _rRtempCM;
        public float RRtempCM
        {
            get
            {
                if (!_rRtempCM.HasValue) _rRtempCM = ValueSerializer.GetFloatValue(nameof(RRtempCM), _data, _headers);
                return _rRtempCM.Value;
            }
        }
        private float? _rRtempCR;
        public float RRtempCR
        {
            get
            {
                if (!_rRtempCR.HasValue) _rRtempCR = ValueSerializer.GetFloatValue(nameof(RRtempCR), _data, _headers);
                return _rRtempCR.Value;
            }
        }
        private float? _rRwearL;
        public float RRwearL
        {
            get
            {
                if (!_rRwearL.HasValue) _rRwearL = ValueSerializer.GetFloatValue(nameof(RRwearL), _data, _headers);
                return _rRwearL.Value;
            }
        }
        private float? _rRwearM;
        public float RRwearM
        {
            get
            {
                if (!_rRwearM.HasValue) _rRwearM = ValueSerializer.GetFloatValue(nameof(RRwearM), _data, _headers);
                return _rRwearM.Value;
            }
        }
        private float? _rRwearR;
        public float RRwearR
        {
            get
            {
                if (!_rRwearR.HasValue) _rRwearR = ValueSerializer.GetFloatValue(nameof(RRwearR), _data, _headers);
                return _rRwearR.Value;
            }
        }
        private int? _sessionFlags;
        public int SessionFlags
        {
            get
            {
                if (!_sessionFlags.HasValue) _sessionFlags = ValueSerializer.GetIntValue(nameof(SessionFlags), _data, _headers);
                return _sessionFlags.Value;
            }
        }
        private int? _sessionLapsRemain;
        public int SessionLapsRemain
        {
            get
            {
                if (!_sessionLapsRemain.HasValue) _sessionLapsRemain = ValueSerializer.GetIntValue(nameof(SessionLapsRemain), _data, _headers);
                return _sessionLapsRemain.Value;
            }
        }
        private int? _sessionLapsRemainEx;
        public int SessionLapsRemainEx
        {
            get
            {
                if (!_sessionLapsRemainEx.HasValue) _sessionLapsRemainEx = ValueSerializer.GetIntValue(nameof(SessionLapsRemainEx), _data, _headers);
                return _sessionLapsRemainEx.Value;
            }
        }
        private int? _sessionNum;
        public int SessionNum
        {
            get
            {
                if (!_sessionNum.HasValue) _sessionNum = ValueSerializer.GetIntValue(nameof(SessionNum), _data, _headers);
                return _sessionNum.Value;
            }
        }
        private int? _sessionState;
        public int SessionState
        {
            get
            {
                if (!_sessionState.HasValue) _sessionState = ValueSerializer.GetIntValue(nameof(SessionState), _data, _headers);
                return _sessionState.Value;
            }
        }
        private int? _sessionTick;
        public int SessionTick
        {
            get
            {
                if (!_sessionTick.HasValue) _sessionTick = ValueSerializer.GetIntValue(nameof(SessionTick), _data, _headers);
                return _sessionTick.Value;
            }
        }
        public double? _sessionTime;
        public double SessionTime
        {
            get
            {
                if (!_sessionTime.HasValue) _sessionTime = ValueSerializer.GetDoubleValue(nameof(SessionTime), _data, _headers);
                return _sessionTime.Value;
            }
        }
        private float? _sessionTimeOfDay;
        public float SessionTimeOfDay
        {
            get
            {
                if (!_sessionTimeOfDay.HasValue) _sessionTimeOfDay = ValueSerializer.GetFloatValue(nameof(SessionTimeOfDay), _data, _headers);
                return _sessionTimeOfDay.Value;
            }
        }
        public double? _sessionTimeRemain;
        public double SessionTimeRemain
        {
            get
            {
                if (!_sessionTimeRemain.HasValue) _sessionTimeRemain = ValueSerializer.GetDoubleValue(nameof(SessionTimeRemain), _data, _headers);
                return _sessionTimeRemain.Value;
            }
        }
        private int? _sessionUniqueID;
        public int SessionUniqueID
        {
            get
            {
                if (!_sessionUniqueID.HasValue) _sessionUniqueID = ValueSerializer.GetIntValue(nameof(SessionUniqueID), _data, _headers);
                return _sessionUniqueID.Value;
            }
        }
        private float? _shiftGrindRPM;
        public float ShiftGrindRPM
        {
            get
            {
                if (!_shiftGrindRPM.HasValue) _shiftGrindRPM = ValueSerializer.GetFloatValue(nameof(ShiftGrindRPM), _data, _headers);
                return _shiftGrindRPM.Value;
            }
        }
        private float? _shiftIndicatorPct;
        public float ShiftIndicatorPct
        {
            get
            {
                if (!_shiftIndicatorPct.HasValue) _shiftIndicatorPct = ValueSerializer.GetFloatValue(nameof(ShiftIndicatorPct), _data, _headers);
                return _shiftIndicatorPct.Value;
            }
        }
        private float? _shiftPowerPct;
        public float ShiftPowerPct
        {
            get
            {
                if (!_shiftPowerPct.HasValue) _shiftPowerPct = ValueSerializer.GetFloatValue(nameof(ShiftPowerPct), _data, _headers);
                return _shiftPowerPct.Value;
            }
        }
        private int? _skies;
        public int Skies
        {
            get
            {
                if (!_skies.HasValue) _skies = ValueSerializer.GetIntValue(nameof(Skies), _data, _headers);
                return _skies.Value;
            }
        }
        private float? _speed;
        public float Speed
        {
            get
            {
                if (!_speed.HasValue) _speed = ValueSerializer.GetFloatValue(nameof(Speed), _data, _headers);
                return _speed.Value;
            }
        }
        private float? _steeringWheelAngle;
        public float SteeringWheelAngle
        {
            get
            {
                if (!_steeringWheelAngle.HasValue) _steeringWheelAngle = ValueSerializer.GetFloatValue(nameof(SteeringWheelAngle), _data, _headers);
                return _steeringWheelAngle.Value;
            }
        }
        private float? _steeringWheelAngleMax;
        public float SteeringWheelAngleMax
        {
            get
            {
                if (!_steeringWheelAngleMax.HasValue) _steeringWheelAngleMax = ValueSerializer.GetFloatValue(nameof(SteeringWheelAngleMax), _data, _headers);
                return _steeringWheelAngleMax.Value;
            }
        }
        private float? _steeringWheelMaxForceNm;
        public float SteeringWheelMaxForceNm
        {
            get
            {
                if (!_steeringWheelMaxForceNm.HasValue) _steeringWheelMaxForceNm = ValueSerializer.GetFloatValue(nameof(SteeringWheelMaxForceNm), _data, _headers);
                return _steeringWheelMaxForceNm.Value;
            }
        }
        private float? _steeringWheelPctDamper;
        public float SteeringWheelPctDamper
        {
            get
            {
                if (!_steeringWheelPctDamper.HasValue) _steeringWheelPctDamper = ValueSerializer.GetFloatValue(nameof(SteeringWheelPctDamper), _data, _headers);
                return _steeringWheelPctDamper.Value;
            }
        }
        private float? _steeringWheelPctTorque;
        public float SteeringWheelPctTorque
        {
            get
            {
                if (!_steeringWheelPctTorque.HasValue) _steeringWheelPctTorque = ValueSerializer.GetFloatValue(nameof(SteeringWheelPctTorque), _data, _headers);
                return _steeringWheelPctTorque.Value;
            }
        }
        private float? _steeringWheelPctTorqueSign;
        public float SteeringWheelPctTorqueSign
        {
            get
            {
                if (!_steeringWheelPctTorqueSign.HasValue) _steeringWheelPctTorqueSign = ValueSerializer.GetFloatValue(nameof(SteeringWheelPctTorqueSign), _data, _headers);
                return _steeringWheelPctTorqueSign.Value;
            }
        }
        private float? _steeringWheelPctTorqueSignStops;
        public float SteeringWheelPctTorqueSignStops
        {
            get
            {
                if (!_steeringWheelPctTorqueSignStops.HasValue) _steeringWheelPctTorqueSignStops = ValueSerializer.GetFloatValue(nameof(SteeringWheelPctTorqueSignStops), _data, _headers);
                return _steeringWheelPctTorqueSignStops.Value;
            }
        }
        private float? _steeringWheelPeakForceNm;
        public float SteeringWheelPeakForceNm
        {
            get
            {
                if (!_steeringWheelPeakForceNm.HasValue) _steeringWheelPeakForceNm = ValueSerializer.GetFloatValue(nameof(SteeringWheelPeakForceNm), _data, _headers);
                return _steeringWheelPeakForceNm.Value;
            }
        }
        private float? _steeringWheelTorque;
        public float SteeringWheelTorque
        {
            get
            {
                if (!_steeringWheelTorque.HasValue) _steeringWheelTorque = ValueSerializer.GetFloatValue(nameof(SteeringWheelTorque), _data, _headers);
                return _steeringWheelTorque.Value;
            }
        }
        private float[] _steeringWheelTorque_ST;
        public float[] SteeringWheelTorque_ST
        {
            get
            {
                if (_steeringWheelTorque_ST == null) _steeringWheelTorque_ST = ValueSerializer.GetFloatArrayValue(nameof(SteeringWheelTorque_ST), _data, _headers);
                return _steeringWheelTorque_ST;
            }
        }
        private bool? _steeringWheelUseLinear;
        public bool SteeringWheelUseLinear
        {
            get
            {
                if (!_steeringWheelUseLinear.HasValue) _steeringWheelUseLinear = ValueSerializer.GetBoolValue(nameof(SteeringWheelUseLinear), _data, _headers);
                return _steeringWheelUseLinear.Value;
            }
        }
        private float? _throttle;
        public float Throttle
        {
            get
            {
                if (!_throttle.HasValue) _throttle = ValueSerializer.GetFloatValue(nameof(Throttle), _data, _headers);
                return _throttle.Value;
            }
        }
        private float? _throttleRaw;
        public float ThrottleRaw
        {
            get
            {
                if (!_throttleRaw.HasValue) _throttleRaw = ValueSerializer.GetFloatValue(nameof(ThrottleRaw), _data, _headers);
                return _throttleRaw.Value;
            }
        }
        private float? _tireLF_RumblePitch;
        public float TireLF_RumblePitch
        {
            get
            {
                if (!_tireLF_RumblePitch.HasValue) _tireLF_RumblePitch = ValueSerializer.GetFloatValue(nameof(TireLF_RumblePitch), _data, _headers);
                return _tireLF_RumblePitch.Value;
            }
        }
        private float? _tireLR_RumblePitch;
        public float TireLR_RumblePitch
        {
            get
            {
                if (!_tireLR_RumblePitch.HasValue) _tireLR_RumblePitch = ValueSerializer.GetFloatValue(nameof(TireLR_RumblePitch), _data, _headers);
                return _tireLR_RumblePitch.Value;
            }
        }
        private float? _tireRF_RumblePitch;
        public float TireRF_RumblePitch
        {
            get
            {
                if (!_tireRF_RumblePitch.HasValue) _tireRF_RumblePitch = ValueSerializer.GetFloatValue(nameof(TireRF_RumblePitch), _data, _headers);
                return _tireRF_RumblePitch.Value;
            }
        }
        private float? _tireRR_RumblePitch;
        public float TireRR_RumblePitch
        {
            get
            {
                if (!_tireRR_RumblePitch.HasValue) _tireRR_RumblePitch = ValueSerializer.GetFloatValue(nameof(TireRR_RumblePitch), _data, _headers);
                return _tireRR_RumblePitch.Value;
            }
        }
        private float? _trackTemp;
        public float TrackTemp
        {
            get
            {
                if (!_trackTemp.HasValue) _trackTemp = ValueSerializer.GetFloatValue(nameof(TrackTemp), _data, _headers);
                return _trackTemp.Value;
            }
        }
        private float? _trackTempCrew;
        public float TrackTempCrew
        {
            get
            {
                if (!_trackTempCrew.HasValue) _trackTempCrew = ValueSerializer.GetFloatValue(nameof(TrackTempCrew), _data, _headers);
                return _trackTempCrew.Value;
            }
        }
        private float? _velocityX;
        public float VelocityX
        {
            get
            {
                if (!_velocityX.HasValue) _velocityX = ValueSerializer.GetFloatValue(nameof(VelocityX), _data, _headers);
                return _velocityX.Value;
            }
        }
        private float[] _velocityX_ST;
        public float[] VelocityX_ST
        {
            get
            {
                if (_velocityX_ST == null) _velocityX_ST = ValueSerializer.GetFloatArrayValue(nameof(VelocityX_ST), _data, _headers);
                return _velocityX_ST;
            }
        }
        private float? _velocityY;
        public float VelocityY
        {
            get
            {
                if (!_velocityY.HasValue) _velocityY = ValueSerializer.GetFloatValue(nameof(VelocityY), _data, _headers);
                return _velocityY.Value;
            }
        }
        private float[] _velocityY_ST;
        public float[] VelocityY_ST
        {
            get
            {
                if (_velocityY_ST == null) _velocityY_ST = ValueSerializer.GetFloatArrayValue(nameof(VelocityY_ST), _data, _headers);
                return _velocityY_ST;
            }
        }
        private float? _velocityZ;
        public float VelocityZ
        {
            get
            {
                if (!_velocityZ.HasValue) _velocityZ = ValueSerializer.GetFloatValue(nameof(VelocityZ), _data, _headers);
                return _velocityZ.Value;
            }
        }
        private float[] _velocityZ_ST;
        public float[] VelocityZ_ST
        {
            get
            {
                if (_velocityZ_ST == null) _velocityZ_ST = ValueSerializer.GetFloatArrayValue(nameof(VelocityZ_ST), _data, _headers);
                return _velocityZ_ST;
            }
        }
        private float? _vertAccel;
        public float VertAccel
        {
            get
            {
                if (!_vertAccel.HasValue) _vertAccel = ValueSerializer.GetFloatValue(nameof(VertAccel), _data, _headers);
                return _vertAccel.Value;
            }
        }
        private float[] _vertAccel_ST;
        public float[] VertAccel_ST
        {
            get
            {
                if (_vertAccel_ST == null) _vertAccel_ST = ValueSerializer.GetFloatArrayValue(nameof(VertAccel_ST), _data, _headers);
                return _vertAccel_ST;
            }
        }
        private float? _voltage;
        public float Voltage
        {
            get
            {
                if (!_voltage.HasValue) _voltage = ValueSerializer.GetFloatValue(nameof(Voltage), _data, _headers);
                return _voltage.Value;
            }
        }
        private float? _waterLevel;
        public float WaterLevel
        {
            get
            {
                if (!_waterLevel.HasValue) _waterLevel = ValueSerializer.GetFloatValue(nameof(WaterLevel), _data, _headers);
                return _waterLevel.Value;
            }
        }
        private float? _waterTemp;
        public float WaterTemp
        {
            get
            {
                if (!_waterTemp.HasValue) _waterTemp = ValueSerializer.GetFloatValue(nameof(WaterTemp), _data, _headers);
                return _waterTemp.Value;
            }
        }
        private int? _weatherType;
        public int WeatherType
        {
            get
            {
                if (!_weatherType.HasValue) _weatherType = ValueSerializer.GetIntValue(nameof(WeatherType), _data, _headers);
                return _weatherType.Value;
            }
        }
        private float? _windDir;
        public float WindDir
        {
            get
            {
                if (!_windDir.HasValue) _windDir = ValueSerializer.GetFloatValue(nameof(WindDir), _data, _headers);
                return _windDir.Value;
            }
        }
        private float? _windVel;
        public float WindVel
        {
            get
            {
                if (!_windVel.HasValue) _windVel = ValueSerializer.GetFloatValue(nameof(WindVel), _data, _headers);
                return _windVel.Value;
            }
        }
        private float? _yaw;
        public float Yaw
        {
            get
            {
                if (!_yaw.HasValue) _yaw = ValueSerializer.GetFloatValue(nameof(Yaw), _data, _headers);
                return _yaw.Value;
            }
        }
        private float? _yawNorth;
        public float YawNorth
        {
            get
            {
                if (!_yawNorth.HasValue) _yawNorth = ValueSerializer.GetFloatValue(nameof(YawNorth), _data, _headers);
                return _yawNorth.Value;
            }
        }
        private float? _yawRate;
        public float YawRate
        {
            get
            {
                if (!_yawRate.HasValue) _yawRate = ValueSerializer.GetFloatValue(nameof(YawRate), _data, _headers);
                return _yawRate.Value;
            }
        }
        private float[] _yawRate_ST;
        public float[] YawRate_ST
        {
            get
            {
                if (_yawRate_ST == null) _yawRate_ST = ValueSerializer.GetFloatArrayValue(nameof(YawRate_ST), _data, _headers);
                return _yawRate_ST;
            }
        }





        private double? _sessionTimeTotal;
        public double SessionTimeTotal
        {
            get
            {
                if (!_sessionTimeTotal.HasValue) _sessionTimeTotal = ValueSerializer.GetDoubleValue(nameof(SessionTimeTotal), _data, _headers);
                return _sessionTimeTotal.Value;
            }
        }
        private int? _sessionLapsTotal;
        public int SessionLapsTotal
        {
            get
            {
                if (!_sessionLapsTotal.HasValue) _sessionLapsTotal = ValueSerializer.GetIntValue(nameof(SessionLapsTotal), _data, _headers);
                return _sessionLapsTotal.Value;
            }
        }
        private int? _sessionJokerLapsRemain;
        public int SessionJokerLapsRemain
        {
            get
            {
                if (!_sessionJokerLapsRemain.HasValue) _sessionJokerLapsRemain = ValueSerializer.GetIntValue(nameof(SessionJokerLapsRemain), _data, _headers);
                return _sessionJokerLapsRemain.Value;
            }
        }
        private bool? _sessionOnJokerLap;
        public bool SessionOnJokerLap
        {
            get
            {
                if (!_sessionOnJokerLap.HasValue) _sessionOnJokerLap = ValueSerializer.GetBoolValue(nameof(SessionOnJokerLap), _data, _headers);
                return _sessionOnJokerLap.Value;
            }
        }
        private float? _cpuUsageFG;
        public float CpuUsageFG
        {
            get
            {
                if (!_cpuUsageFG.HasValue) _cpuUsageFG = ValueSerializer.GetFloatValue(nameof(CpuUsageFG), _data, _headers);
                return _cpuUsageFG.Value;
            }
        }
        private float? _gpuUsage;
        public float GpuUsage
        {
            get
            {
                if (!_gpuUsage.HasValue) _gpuUsage = ValueSerializer.GetFloatValue(nameof(GpuUsage), _data, _headers);
                return _gpuUsage.Value;
            }
        }
        private float? _chanAvgLatency;
        public float ChanAvgLatency
        {
            get
            {
                if (!_chanAvgLatency.HasValue) _chanAvgLatency = ValueSerializer.GetFloatValue(nameof(ChanAvgLatency), _data, _headers);
                return _chanAvgLatency.Value;
            }
        }
        private float? _chanLatency;
        public float ChanLatency
        {
            get
            {
                if (!_chanLatency.HasValue) _chanLatency = ValueSerializer.GetFloatValue(nameof(ChanLatency), _data, _headers);
                return _chanLatency.Value;
            }
        }
        private float? _chanQuality;
        public float ChanQuality
        {
            get
            {
                if (!_chanQuality.HasValue) _chanQuality = ValueSerializer.GetFloatValue(nameof(ChanQuality), _data, _headers);
                return _chanQuality.Value;
            }
        }
        private float? _chanPartnerQuality;
        public float ChanPartnerQuality
        {
            get
            {
                if (!_chanPartnerQuality.HasValue) _chanPartnerQuality = ValueSerializer.GetFloatValue(nameof(ChanPartnerQuality), _data, _headers);
                return _chanPartnerQuality.Value;
            }
        }
        private float? _chanClockSkew;
        public float ChanClockSkew
        {
            get
            {
                if (!_chanClockSkew.HasValue) _chanClockSkew = ValueSerializer.GetFloatValue(nameof(ChanClockSkew), _data, _headers);
                return _chanClockSkew.Value;
            }
        }
        private float? _memPageFaultSec;
        public float MemPageFaultSec
        {
            get
            {
                if (!_memPageFaultSec.HasValue) _memPageFaultSec = ValueSerializer.GetFloatValue(nameof(MemPageFaultSec), _data, _headers);
                return _memPageFaultSec.Value;
            }
        }
        private int? _playerCarClass;
        public int PlayerCarClass
        {
            get
            {
                if (!_playerCarClass.HasValue) _playerCarClass = ValueSerializer.GetIntValue(nameof(PlayerCarClass), _data, _headers);
                return _playerCarClass.Value;
            }
        }
        private int? _playerCarDryTireSetLimit;
        public int PlayerCarDryTireSetLimit
        {
            get
            {
                if (!_playerCarDryTireSetLimit.HasValue) _playerCarDryTireSetLimit = ValueSerializer.GetIntValue(nameof(PlayerCarDryTireSetLimit), _data, _headers);
                return _playerCarDryTireSetLimit.Value;
            }
        }
        private int? _playerTireCompound;
        public int PlayerTireCompound
        {
            get
            {
                if (!_playerTireCompound.HasValue) _playerTireCompound = ValueSerializer.GetIntValue(nameof(PlayerTireCompound), _data, _headers);
                return _playerTireCompound.Value;
            }
        }
        private int? _playerFastRepairsUsed;
        public int PlayerFastRepairsUsed
        {
            get
            {
                if (!_playerFastRepairsUsed.HasValue) _playerFastRepairsUsed = ValueSerializer.GetIntValue(nameof(PlayerFastRepairsUsed), _data, _headers);
                return _playerFastRepairsUsed.Value;
            }
        }
        private int? _paceMode;
        public int PaceMode
        {
            get
            {
                if (!_paceMode.HasValue) _paceMode = ValueSerializer.GetIntValue(nameof(PaceMode), _data, _headers);
                return _paceMode.Value;
            }
        }
        private bool? _vidCapEnabled;
        public bool VidCapEnabled
        {
            get
            {
                if (!_vidCapEnabled.HasValue) _vidCapEnabled = ValueSerializer.GetBoolValue(nameof(VidCapEnabled), _data, _headers);
                return _vidCapEnabled.Value;
            }
        }
        private bool? _vidCapActive;
        public bool VidCapActive
        {
            get
            {
                if (!_vidCapActive.HasValue) _vidCapActive = ValueSerializer.GetBoolValue(nameof(VidCapActive), _data, _headers);
                return _vidCapActive.Value;
            }
        }
        private int? _lFTiresUsed;
        public int LFTiresUsed
        {
            get
            {
                if (!_lFTiresUsed.HasValue) _lFTiresUsed = ValueSerializer.GetIntValue(nameof(LFTiresUsed), _data, _headers);
                return _lFTiresUsed.Value;
            }
        }
        private int? _rFTiresUsed;
        public int RFTiresUsed
        {
            get
            {
                if (!_rFTiresUsed.HasValue) _rFTiresUsed = ValueSerializer.GetIntValue(nameof(RFTiresUsed), _data, _headers);
                return _rFTiresUsed.Value;
            }
        }
        private int? _lRTiresUsed;
        public int LRTiresUsed
        {
            get
            {
                if (!_lRTiresUsed.HasValue) _lRTiresUsed = ValueSerializer.GetIntValue(nameof(LRTiresUsed), _data, _headers);
                return _lRTiresUsed.Value;
            }
        }
        private int? _rRTiresUsed;
        public int RRTiresUsed
        {
            get
            {
                if (!_rRTiresUsed.HasValue) _rRTiresUsed = ValueSerializer.GetIntValue(nameof(RRTiresUsed), _data, _headers);
                return _rRTiresUsed.Value;
            }
        }
        private int? _leftTireSetsUsed;
        public int LeftTireSetsUsed
        {
            get
            {
                if (!_leftTireSetsUsed.HasValue) _leftTireSetsUsed = ValueSerializer.GetIntValue(nameof(LeftTireSetsUsed), _data, _headers);
                return _leftTireSetsUsed.Value;
            }
        }
        private int? _rightTireSetsUsed;
        public int RightTireSetsUsed
        {
            get
            {
                if (!_rightTireSetsUsed.HasValue) _rightTireSetsUsed = ValueSerializer.GetIntValue(nameof(RightTireSetsUsed), _data, _headers);
                return _rightTireSetsUsed.Value;
            }
        }
        private int? _frontTireSetsUsed;
        public int FrontTireSetsUsed
        {
            get
            {
                if (!_frontTireSetsUsed.HasValue) _frontTireSetsUsed = ValueSerializer.GetIntValue(nameof(FrontTireSetsUsed), _data, _headers);
                return _frontTireSetsUsed.Value;
            }
        }
        private int? _rearTireSetsUsed;
        public int RearTireSetsUsed
        {
            get
            {
                if (!_rearTireSetsUsed.HasValue) _rearTireSetsUsed = ValueSerializer.GetIntValue(nameof(RearTireSetsUsed), _data, _headers);
                return _rearTireSetsUsed.Value;
            }
        }
        private int? _tireSetsUsed;
        public int TireSetsUsed
        {
            get
            {
                if (!_tireSetsUsed.HasValue) _tireSetsUsed = ValueSerializer.GetIntValue(nameof(TireSetsUsed), _data, _headers);
                return _tireSetsUsed.Value;
            }
        }
        private int? _lFTiresAvailable;
        public int LFTiresAvailable
        {
            get
            {
                if (!_lFTiresAvailable.HasValue) _lFTiresAvailable = ValueSerializer.GetIntValue(nameof(LFTiresAvailable), _data, _headers);
                return _lFTiresAvailable.Value;
            }
        }
        private int? _rFTiresAvailable;
        public int RFTiresAvailable
        {
            get
            {
                if (!_rFTiresAvailable.HasValue) _rFTiresAvailable = ValueSerializer.GetIntValue(nameof(RFTiresAvailable), _data, _headers);
                return _rFTiresAvailable.Value;
            }
        }
        private int? _lRTiresAvailable;
        public int LRTiresAvailable
        {
            get
            {
                if (!_lRTiresAvailable.HasValue) _lRTiresAvailable = ValueSerializer.GetIntValue(nameof(LRTiresAvailable), _data, _headers);
                return _lRTiresAvailable.Value;
            }
        }
        private int? _rRTiresAvailable;
        public int RRTiresAvailable
        {
            get
            {
                if (!_rRTiresAvailable.HasValue) _rRTiresAvailable = ValueSerializer.GetIntValue(nameof(RRTiresAvailable), _data, _headers);
                return _rRTiresAvailable.Value;
            }
        }
        private int? _leftTireSetsAvailable;
        public int LeftTireSetsAvailable
        {
            get
            {
                if (!_leftTireSetsAvailable.HasValue) _leftTireSetsAvailable = ValueSerializer.GetIntValue(nameof(LeftTireSetsAvailable), _data, _headers);
                return _leftTireSetsAvailable.Value;
            }
        }
        private int? _rightTireSetsAvailable;
        public int RightTireSetsAvailable
        {
            get
            {
                if (!_rightTireSetsAvailable.HasValue) _rightTireSetsAvailable = ValueSerializer.GetIntValue(nameof(RightTireSetsAvailable), _data, _headers);
                return _rightTireSetsAvailable.Value;
            }
        }
        private int? _frontTireSetsAvailable;
        public int FrontTireSetsAvailable
        {
            get
            {
                if (!_frontTireSetsAvailable.HasValue) _frontTireSetsAvailable = ValueSerializer.GetIntValue(nameof(FrontTireSetsAvailable), _data, _headers);
                return _frontTireSetsAvailable.Value;
            }
        }
        private int? _rearTireSetsAvailable;
        public int RearTireSetsAvailable
        {
            get
            {
                if (!_rearTireSetsAvailable.HasValue) _rearTireSetsAvailable = ValueSerializer.GetIntValue(nameof(RearTireSetsAvailable), _data, _headers);
                return _rearTireSetsAvailable.Value;
            }
        }
        private int? _tireSetsAvailable;
        public int TireSetsAvailable
        {
            get
            {
                if (!_tireSetsAvailable.HasValue) _tireSetsAvailable = ValueSerializer.GetIntValue(nameof(TireSetsAvailable), _data, _headers);
                return _tireSetsAvailable.Value;
            }
        }
        private float? _steeringWheelLimiter;
        public float SteeringWheelLimiter
        {
            get
            {
                if (!_steeringWheelLimiter.HasValue) _steeringWheelLimiter = ValueSerializer.GetFloatValue(nameof(SteeringWheelLimiter), _data, _headers);
                return _steeringWheelLimiter.Value;
            }
        }
        private bool? _brakeABSactive;
        public bool BrakeABSactive
        {
            get
            {
                if (!_brakeABSactive.HasValue) _brakeABSactive = ValueSerializer.GetBoolValue(nameof(BrakeABSactive), _data, _headers);
                return _brakeABSactive.Value;
            }
        }
        private int? _pitSvTireCompound;
        public int PitSvTireCompound
        {
            get
            {
                if (!_pitSvTireCompound.HasValue) _pitSvTireCompound = ValueSerializer.GetIntValue(nameof(PitSvTireCompound), _data, _headers);
                return _pitSvTireCompound.Value;
            }
        }
        private bool? _dcTearOffVisor;
        public bool dcTearOffVisor
        {
            get
            {
                if (!_dcTearOffVisor.HasValue) _dcTearOffVisor = ValueSerializer.GetBoolValue(nameof(dcTearOffVisor), _data, _headers);
                return _dcTearOffVisor.Value;
            }
        }
        private float? _dcAntiRollFront;
        public float dcAntiRollFront
        {
            get
            {
                if (!_dcAntiRollFront.HasValue) _dcAntiRollFront = ValueSerializer.GetFloatValue(nameof(dcAntiRollFront), _data, _headers);
                return _dcAntiRollFront.Value;
            }
        }
        private float? _dcAntiRollRear;
        public float dcAntiRollRear
        {
            get
            {
                if (!_dcAntiRollRear.HasValue) _dcAntiRollRear = ValueSerializer.GetFloatValue(nameof(dcAntiRollRear), _data, _headers);
                return _dcAntiRollRear.Value;
            }
        }
        private float? _dcBrakeBias;
        public float dcBrakeBias
        {
            get
            {
                if (!_dcBrakeBias.HasValue) _dcBrakeBias = ValueSerializer.GetFloatValue(nameof(dcBrakeBias), _data, _headers);
                return _dcBrakeBias.Value;
            }
        }
		private bool? _pushToTalk;
		public bool PushToTalk
		{
			get
			{
				if ( !_pushToTalk.HasValue ) _pushToTalk = ValueSerializer.GetBoolValue( nameof( PushToTalk ), _data, _headers );
				return _pushToTalk.Value;
			}
		}

	}
}
