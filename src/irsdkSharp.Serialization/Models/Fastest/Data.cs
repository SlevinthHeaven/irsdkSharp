using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using irsdkSharp.Enums;
using irsdkSharp.Models;
using irsdkSharp.Serialization.Enums.Fastest;
using irsdkSharp.Serialization.Models.Session;
using YamlDotNet.Serialization;

namespace irsdkSharp.Serialization.Models.Fastest
{
    /// <summary>
    /// Serializes all session properties on-demand with the latest value.
    /// </summary>
    public class Data
    {
        private readonly IRacingSDK _sdk;
        private readonly MemoryMappedViewAccessor _fileView;
        private readonly Dictionary<string, VarHeader> _headers;
        private volatile int _currentSessionUpdate;

        public Data(IRacingSDK sdk)
        {
            _sdk = sdk;
            _fileView = IRacingSDK.GetFileMapView(sdk);
            _headers = IRacingSDK.GetVarHeaders(sdk);
        }

        //private IRacingSessionModel _session;

        //public IRacingSessionModel Session
        //{
        //    get
        //    {
        //        var latest = _sdk.Header.SessionInfoUpdate;
        //        if (latest > _currentSessionUpdate)
        //        {
        //            lock (this)
        //            {
        //                if (latest > _currentSessionUpdate)
        //                {
        //                    _currentSessionUpdate = latest;
        //                    _session = Serialize(_sdk.GetSessionInfo());
        //                }
        //            }
        //        }
        //        return _session;
        //    }
        //}

        //private static IRacingSessionModel Serialize(string yaml)
        //{
        //    using var r = new StringReader(yaml);
        //    var deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
        //    try
        //    {
        //        return deserializer.Deserialize<IRacingSessionModel>(r);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.InnerException.Message);
        //        return null;
        //    }
        //}

        /// <summary>
        /// Density of air at start/finish line
        /// </summary>
        public float AirDensity => _headers.TryGetValue(nameof(AirDensity), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pressure of air at start/finish line
        /// </summary>
        public float AirPressure => _headers.TryGetValue(nameof(AirPressure), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Temperature of air at start/finish line
        /// </summary>
        public float AirTemp => _headers.TryGetValue(nameof(AirTemp), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 0=brake released to 1=max pedal force
        /// </summary>
        public float Brake => _headers.TryGetValue(nameof(Brake), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// true if abs is currently reducing brake force pressure
        /// </summary>
        public bool BrakeABSactive => _headers.TryGetValue(nameof(BrakeABSactive), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Raw brake input 0=brake released to 1=max pedal force
        /// </summary>
        public float BrakeRaw => _headers.TryGetValue(nameof(BrakeRaw), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Active camera number
        /// </summary>
        public int CamCameraNumber => _headers.TryGetValue(nameof(CamCameraNumber), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// State of camera system
        /// </summary>
        public CameraState CamCameraState => _headers.TryGetValue(nameof(CamCameraState), out var header)
            ? (CameraState) _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        private int[] _camCarIdx = new int[64];

        /// <summary>
        /// Active camera's focus car index
        /// </summary>
        public int[] CamCarIdx
        {
            get
            {
                if (_headers.TryGetValue(nameof(CamCarIdx), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _camCarIdx, 0, 64);
                }

                return _camCarIdx;
            }
        }

        /// <summary>
        /// Active camera group number
        /// </summary>
        public int CamGroupNumber => _headers.TryGetValue(nameof(CamGroupNumber), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        private int[] _carIdxBestLapNum = new int[64];

        /// <summary>
        /// Cars best lap number
        /// </summary>
        public int[] CarIdxBestLapNum
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxBestLapNum), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxBestLapNum, 0, 64);
                }

                return _carIdxBestLapNum;
            }
        }

        private float[] _carIdxBestLapTime = new float[64];

        /// <summary>
        /// Cars best lap time
        /// </summary>
        public float[] CarIdxBestLapTime
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxBestLapTime), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxBestLapTime, 0, 64);
                }

                return _carIdxBestLapTime;
            }
        }

        private int[] _carIdxClass = new int[64];

        /// <summary>
        /// Cars class id by car index
        /// </summary>
        public int[] CarIdxClass
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxClass), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxClass, 0, 64);
                }

                return _carIdxClass;
            }
        }

        private int[] _carIdxClassPosition = new int[64];

        /// <summary>
        /// Cars class position in race by car index
        /// </summary>
        public int[] CarIdxClassPosition
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxClassPosition), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxClassPosition, 0, 64);
                }

                return _carIdxClassPosition;
            }
        }

        private float[] _carIdxEstTime = new float[64];

        /// <summary>
        /// Estimated time to reach current location on track
        /// </summary>
        public float[] CarIdxEstTime
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxEstTime), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxEstTime, 0, 64);
                }

                return _carIdxEstTime;
            }
        }

        private float[] _carIdxF2Time = new float[64];

        /// <summary>
        /// Race time behind leader or fastest lap time otherwise
        /// </summary>
        public float[] CarIdxF2Time
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxF2Time), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxF2Time, 0, 64);
                }

                return _carIdxF2Time;
            }
        }

        private int[] _carIdxFastRepairsUsed = new int[64];

        /// <summary>
        /// How many fast repairs each car has used
        /// </summary>
        public int[] CarIdxFastRepairsUsed
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxFastRepairsUsed), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxFastRepairsUsed, 0, 64);
                }

                return _carIdxFastRepairsUsed;
            }
        }

        private int[] _carIdxGear = new int[64];

        /// <summary>
        /// -1=reverse  0=neutral  1..n=current gear by car index
        /// </summary>
        public int[] CarIdxGear
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxGear), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxGear, 0, 64);
                }

                return _carIdxGear;
            }
        }

        private int[] _carIdxLap = new int[64];

        /// <summary>
        /// Laps started by car index
        /// </summary>
        public int[] CarIdxLap
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxLap), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxLap, 0, 64);
                }

                return _carIdxLap;
            }
        }

        private int[] _carIdxLapCompleted = new int[64];

        /// <summary>
        /// Laps completed by car index
        /// </summary>
        public int[] CarIdxLapCompleted
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxLapCompleted), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxLapCompleted, 0, 64);
                }

                return _carIdxLapCompleted;
            }
        }

        private float[] _carIdxLapDistPct = new float[64];

        /// <summary>
        /// Percentage distance around lap by car index
        /// </summary>
        public float[] CarIdxLapDistPct
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxLapDistPct), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxLapDistPct, 0, 64);
                }

                return _carIdxLapDistPct;
            }
        }

        private float[] _carIdxLastLapTime = new float[64];

        /// <summary>
        /// Cars last lap time
        /// </summary>
        public float[] CarIdxLastLapTime
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxLastLapTime), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxLastLapTime, 0, 64);
                }

                return _carIdxLastLapTime;
            }
        }

        private bool[] _carIdxOnPitRoad = new bool[64];

        /// <summary>
        /// On pit road between the cones by car index
        /// </summary>
        public bool[] CarIdxOnPitRoad
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxOnPitRoad), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxOnPitRoad, 0, 64);
                }

                return _carIdxOnPitRoad;
            }
        }

        private int[] _carIdxP2PCount = new int[64];

        /// <summary>
        /// Push2Pass count of usage (or remaining in Race)
        /// </summary>
        public int[] CarIdxP2P_Count
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxP2P_Count), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxP2PCount, 0, 64);
                }

                return _carIdxP2PCount;
            }
        }

        private bool[] _carIdxP2PStatus = new bool[64];

        /// <summary>
        /// Push2Pass active or not
        /// </summary>
        public bool[] CarIdxP2P_Status
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxP2P_Status), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxP2PStatus, 0, 64);
                }

                return _carIdxP2PStatus;
            }
        }

        private PaceFlags[] _carIdxPaceFlags = new PaceFlags[64];

        /// <summary>
        /// Pacing status flags for each car
        /// </summary>
        public PaceFlags[] CarIdxPaceFlags
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxPaceFlags), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxPaceFlags, 0, 64);
                }

                return _carIdxPaceFlags;
            }
        }

        private int[] _carIdxPaceLine = new int[64];

        /// <summary>
        /// What line cars are pacing in  or -1 if not pacing
        /// </summary>
        public int[] CarIdxPaceLine
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxPaceLine), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxPaceLine, 0, 64);
                }

                return _carIdxPaceLine;
            }
        }

        private int[] _carIdxPaceRow = new int[64];

        /// <summary>
        /// What row cars are pacing in  or -1 if not pacing
        /// </summary>
        public int[] CarIdxPaceRow
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxPaceRow), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxPaceRow, 0, 64);
                }

                return _carIdxPaceRow;
            }
        }

        private int[] _carIdxPosition = new int[64];

        /// <summary>
        /// Cars position in race by car index
        /// </summary>
        public int[] CarIdxPosition
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxPosition), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxPosition, 0, 64);
                }

                return _carIdxPosition;
            }
        }

        private int[] _carIdxQualTireCompound = new int[64];

        /// <summary>
        /// Cars Qual tire compound
        /// </summary>
        public int[] CarIdxQualTireCompound
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxQualTireCompound), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxQualTireCompound, 0, 64);
                }

                return _carIdxQualTireCompound;
            }
        }

        private bool[] _carIdxQualTireCompoundLocked = new bool[64];

        /// <summary>
        /// Cars Qual tire compound is locked-in
        /// </summary>
        public bool[] CarIdxQualTireCompoundLocked
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxQualTireCompoundLocked), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxQualTireCompoundLocked, 0,
                        64);
                }

                return _carIdxQualTireCompoundLocked;
            }
        }

        private float[] _carIdxRpm = new float[64];

        /// <summary>
        /// Engine rpm by car index
        /// </summary>
        public float[] CarIdxRPM
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxRPM), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxRpm, 0, 64);
                }

                return _carIdxRpm;
            }
        }

        private float[] _carIdxSteer = new float[64];

        /// <summary>
        /// Steering wheel angle by car index
        /// </summary>
        public float[] CarIdxSteer
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxSteer), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxSteer, 0, 64);
                }

                return _carIdxSteer;
            }
        }

        private int[] _carIdxTireCompound = new int[64];

        /// <summary>
        /// Cars current tire compound
        /// </summary>
        public int[] CarIdxTireCompound
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxTireCompound), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxTireCompound, 0, 64);
                }

                return _carIdxTireCompound;
            }
        }

        private TrackSurface[] _carIdxTrackSurface = new TrackSurface[64];

        /// <summary>
        /// Track surface type by car index
        /// </summary>
        public TrackSurface[] CarIdxTrackSurface
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxTrackSurface), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxTrackSurface, 0, 64);
                }

                return _carIdxTrackSurface;
            }
        }

        private TrackSurfaceMaterial[] _carIdxTrackSurfaceMaterial = new TrackSurfaceMaterial[64];

        /// <summary>
        /// Track surface material type by car index
        /// </summary>
        public TrackSurfaceMaterial[] CarIdxTrackSurfaceMaterial
        {
            get
            {
                if (_headers.TryGetValue(nameof(CarIdxTrackSurfaceMaterial), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _carIdxTrackSurfaceMaterial, 0, 64);
                }

                return _carIdxTrackSurfaceMaterial;
            }
        }

        /// <summary>
        /// Notify if car is to the left or right of driver
        /// </summary>
        public CarLeftRight CarLeftRight => _headers.TryGetValue(nameof(CarLeftRight), out var header)
            ? (CarLeftRight) _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Communications average latency
        /// </summary>
        public float ChanAvgLatency => _headers.TryGetValue(nameof(ChanAvgLatency), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Communications server clock skew
        /// </summary>
        public float ChanClockSkew => _headers.TryGetValue(nameof(ChanClockSkew), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Communications latency
        /// </summary>
        public float ChanLatency => _headers.TryGetValue(nameof(ChanLatency), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Partner communications quality
        /// </summary>
        public float ChanPartnerQuality => _headers.TryGetValue(nameof(ChanPartnerQuality), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Communications quality
        /// </summary>
        public float ChanQuality => _headers.TryGetValue(nameof(ChanQuality), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 0=disengaged to 1=fully engaged
        /// </summary>
        public float Clutch => _headers.TryGetValue(nameof(Clutch), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Percent of available tim bg thread took with a 1 sec avg
        /// </summary>
        public float CpuUsageBG => _headers.TryGetValue(nameof(CpuUsageBG), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Percent of available tim fg thread took with a 1 sec avg
        /// </summary>
        public float CpuUsageFG => _headers.TryGetValue(nameof(CpuUsageFG), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Number of team drivers who have run a stpublic int
        /// </summary>
        public int DCDriversSoFar => _headers.TryGetValue(nameof(DCDriversSoFar), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Status of driver change lap requirements
        /// </summary>
        public int DCLapStatus => _headers.TryGetValue(nameof(DCLapStatus), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// In car trigger car starter
        /// </summary>
        public bool dcStarter => _headers.TryGetValue(nameof(dcStarter), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// In car tear off visor film
        /// </summary>
        public bool dcTearOffVisor => _headers.TryGetValue(nameof(dcTearOffVisor), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Default units for the user public interface 0 = english 1 = metric
        /// </summary>
        public int DisplayUnits => _headers.TryGetValue(nameof(DisplayUnits), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop fast repair set
        /// </summary>
        public float dpFastRepair => _headers.TryGetValue(nameof(dpFastRepair), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop fuel add ammount
        /// </summary>
        public float dpFuelAddKg => _headers.TryGetValue(nameof(dpFuelAddKg), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop fuel fill flag
        /// </summary>
        public float dpFuelFill => _headers.TryGetValue(nameof(dpFuelFill), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop lf tire change request
        /// </summary>
        public float dpLFTireChange => _headers.TryGetValue(nameof(dpLFTireChange), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop lf tire cold pressure adjustment
        /// </summary>
        public float dpLFTireColdPress => _headers.TryGetValue(nameof(dpLFTireColdPress), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop lr tire change request
        /// </summary>
        public float dpLRTireChange => _headers.TryGetValue(nameof(dpLRTireChange), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop lr tire cold pressure adjustment
        /// </summary>
        public float dpLRTireColdPress => _headers.TryGetValue(nameof(dpLRTireColdPress), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop rf tire change request
        /// </summary>
        public float dpRFTireChange => _headers.TryGetValue(nameof(dpRFTireChange), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop rf cold tire pressure adjustment
        /// </summary>
        public float dpRFTireColdPress => _headers.TryGetValue(nameof(dpRFTireColdPress), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop rr tire change request
        /// </summary>
        public float dpRRTireChange => _headers.TryGetValue(nameof(dpRRTireChange), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitstop rr cold tire pressure adjustment
        /// </summary>
        public float dpRRTireColdPress => _headers.TryGetValue(nameof(dpRRTireColdPress), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Driver activated flag
        /// </summary>
        public bool DriverMarker => _headers.TryGetValue(nameof(DriverMarker), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Bitfield for warning lights
        /// </summary>
        public EngineWarnings EngineWarnings => _headers.TryGetValue(nameof(EngineWarnings), out var header)
            ? (EngineWarnings) _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Indicate action the reset key will take 0 enter 1 exit 2 reset
        /// </summary>
        public int EnterExitReset => _headers.TryGetValue(nameof(EnterExitReset), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many fast repairs left  255 is unlimited
        /// </summary>
        public int FastRepairAvailable => _headers.TryGetValue(nameof(FastRepairAvailable), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many fast repairs used so far
        /// </summary>
        public int FastRepairUsed => _headers.TryGetValue(nameof(FastRepairUsed), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Fog level
        /// </summary>
        public float FogLevel => _headers.TryGetValue(nameof(FogLevel), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Average frames per second
        /// </summary>
        public float FrameRate => _headers.TryGetValue(nameof(FrameRate), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many front tire sets are remaining  255 is unlimited
        /// </summary>
        public int FrontTireSetsAvailable => _headers.TryGetValue(nameof(FrontTireSetsAvailable), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many front tire sets used so far
        /// </summary>
        public int FrontTireSetsUsed => _headers.TryGetValue(nameof(FrontTireSetsUsed), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Liters of fuel remaining
        /// </summary>
        public float FuelLevel => _headers.TryGetValue(nameof(FuelLevel), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Percent fuel remaining
        /// </summary>
        public float FuelLevelPct => _headers.TryGetValue(nameof(FuelLevelPct), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine fuel pressure
        /// </summary>
        public float FuelPress => _headers.TryGetValue(nameof(FuelPress), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine fuel used instantaneous
        /// </summary>
        public float FuelUsePerHour => _headers.TryGetValue(nameof(FuelUsePerHour), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// -1=reverse  0=neutral  1..n=current gear
        /// </summary>
        public int Gear => _headers.TryGetValue(nameof(Gear), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Percent of available tim gpu took with a 1 sec avg
        /// </summary>
        public float GpuUsage => _headers.TryGetValue(nameof(GpuUsage), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Raw handbrake input 0=handbrake released to 1=max force
        /// </summary>
        public float HandbrakeRaw => _headers.TryGetValue(nameof(HandbrakeRaw), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 0=disk based telemetry file not being written  1=being written
        /// </summary>
        public bool IsDiskLoggingActive => _headers.TryGetValue(nameof(IsDiskLoggingActive), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 0=disk based telemetry turned off  1=turned on
        /// </summary>
        public bool IsDiskLoggingEnabled => _headers.TryGetValue(nameof(IsDiskLoggingEnabled), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 1=Car in garage physics running
        /// </summary>
        public bool IsInGarage => _headers.TryGetValue(nameof(IsInGarage), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 1=Car on track physics running with player in car
        /// </summary>
        public bool IsOnTrack => _headers.TryGetValue(nameof(IsOnTrack), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 1=Car on track physics running
        /// </summary>
        public bool IsOnTrackCar => _headers.TryGetValue(nameof(IsOnTrackCar), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 0=replay not playing  1=replay playing
        /// </summary>
        public bool IsReplayPlaying => _headers.TryGetValue(nameof(IsReplayPlaying), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Laps started count
        /// </summary>
        public int Lap => _headers.TryGetValue(nameof(Lap), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players best lap number
        /// </summary>
        public int LapBestLap => _headers.TryGetValue(nameof(LapBestLap), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players best lap time
        /// </summary>
        public float LapBestLapTime => _headers.TryGetValue(nameof(LapBestLapTime), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Player last lap in best N average lap time
        /// </summary>
        public int LapBestNLapLap => _headers.TryGetValue(nameof(LapBestNLapLap), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Player best N average lap time
        /// </summary>
        public float LapBestNLapTime => _headers.TryGetValue(nameof(LapBestNLapTime), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Laps completed count
        /// </summary>
        public int LapCompleted => _headers.TryGetValue(nameof(LapCompleted), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Estimate of players current lap time as shown in F3 box
        /// </summary>
        public float LapCurrentLapTime => _headers.TryGetValue(nameof(LapCurrentLapTime), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Delta time for best lap
        /// </summary>
        public float LapDeltaToBestLap => _headers.TryGetValue(nameof(LapDeltaToBestLap), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Rate of change of delta time for best lap
        /// </summary>
        public float LapDeltaToBestLap_DD => _headers.TryGetValue(nameof(LapDeltaToBestLap_DD), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Delta time for best lap is valid
        /// </summary>
        public bool LapDeltaToBestLap_OK => _headers.TryGetValue(nameof(LapDeltaToBestLap_OK), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Delta time for optimal lap
        /// </summary>
        public float LapDeltaToOptimalLap => _headers.TryGetValue(nameof(LapDeltaToOptimalLap), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Rate of change of delta time for optimal lap
        /// </summary>
        public float LapDeltaToOptimalLap_DD =>
            _headers.TryGetValue(nameof(LapDeltaToOptimalLap_DD), out var header)
                ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Delta time for optimal lap is valid
        /// </summary>
        public bool LapDeltaToOptimalLap_OK => _headers.TryGetValue(nameof(LapDeltaToOptimalLap_OK), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Delta time for session best lap
        /// </summary>
        public float LapDeltaToSessionBestLap =>
            _headers.TryGetValue(nameof(LapDeltaToSessionBestLap), out var header)
                ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Rate of change of delta time for session best lap
        /// </summary>
        public float LapDeltaToSessionBestLap_DD =>
            _headers.TryGetValue(nameof(LapDeltaToSessionBestLap_DD), out var header)
                ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Delta time for session best lap is valid
        /// </summary>
        public bool LapDeltaToSessionBestLap_OK =>
            _headers.TryGetValue(nameof(LapDeltaToSessionBestLap_OK), out var header)
                ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Delta time for session last lap
        /// </summary>
        public float LapDeltaToSessionLastlLap =>
            _headers.TryGetValue(nameof(LapDeltaToSessionLastlLap), out var header)
                ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Rate of change of delta time for session last lap
        /// </summary>
        public float LapDeltaToSessionLastlLap_DD =>
            _headers.TryGetValue(nameof(LapDeltaToSessionLastlLap_DD), out var header)
                ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Delta time for session last lap is valid
        /// </summary>
        public bool LapDeltaToSessionLastlLap_OK =>
            _headers.TryGetValue(nameof(LapDeltaToSessionLastlLap_OK), out var header)
                ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Delta time for session optimal lap
        /// </summary>
        public float LapDeltaToSessionOptimalLap =>
            _headers.TryGetValue(nameof(LapDeltaToSessionOptimalLap), out var header)
                ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Rate of change of delta time for session optimal lap
        /// </summary>
        public float LapDeltaToSessionOptimalLap_DD =>
            _headers.TryGetValue(nameof(LapDeltaToSessionOptimalLap_DD), out var header)
                ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Delta time for session optimal lap is valid
        /// </summary>
        public bool LapDeltaToSessionOptimalLap_OK =>
            _headers.TryGetValue(nameof(LapDeltaToSessionOptimalLap_OK), out var header)
                ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Meters traveled from S/F this lap
        /// </summary>
        public float LapDist => _headers.TryGetValue(nameof(LapDist), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Percentage distance around lap
        /// </summary>
        public float LapDistPct => _headers.TryGetValue(nameof(LapDistPct), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Player num consecutive clean laps completed for N average
        /// </summary>
        public int LapLasNLapSeq => _headers.TryGetValue(nameof(LapLasNLapSeq), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players last lap time
        /// </summary>
        public float LapLastLapTime => _headers.TryGetValue(nameof(LapLastLapTime), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Player last N average lap time
        /// </summary>
        public float LapLastNLapTime => _headers.TryGetValue(nameof(LapLastNLapTime), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Lateral acceleration (including gravity)
        /// </summary>
        public float LatAccel => _headers.TryGetValue(nameof(LatAccel), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Lateral acceleration (including gravity) at 360 Hz
        /// </summary>
        public float LatAccel_ST => _headers.TryGetValue(nameof(LatAccel_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many left tire sets are remaining  255 is unlimited
        /// </summary>
        public int LeftTireSetsAvailable => _headers.TryGetValue(nameof(LeftTireSetsAvailable), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many left tire sets used so far
        /// </summary>
        public int LeftTireSetsUsed => _headers.TryGetValue(nameof(LeftTireSetsUsed), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF tire cold pressure  as set in the garage
        /// </summary>
        public float LFcoldPressure => _headers.TryGetValue(nameof(LFcoldPressure), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF shock deflection
        /// </summary>
        public float LFshockDefl => _headers.TryGetValue(nameof(LFshockDefl), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF shock deflection at 360 Hz
        /// </summary>
        public float LFshockDefl_ST => _headers.TryGetValue(nameof(LFshockDefl_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF shock velocity
        /// </summary>
        public float LFshockVel => _headers.TryGetValue(nameof(LFshockVel), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF shock velocity at 360 Hz
        /// </summary>
        public float LFshockVel_ST => _headers.TryGetValue(nameof(LFshockVel_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF tire left carcass temperature
        /// </summary>
        public float LFtempCL => _headers.TryGetValue(nameof(LFtempCL), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF tire middle carcass temperature
        /// </summary>
        public float LFtempCM => _headers.TryGetValue(nameof(LFtempCM), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF tire right carcass temperature
        /// </summary>
        public float LFtempCR => _headers.TryGetValue(nameof(LFtempCR), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many left front tires are remaining  255 is unlimited
        /// </summary>
        public int LFTiresAvailable => _headers.TryGetValue(nameof(LFTiresAvailable), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many left front tires used so far
        /// </summary>
        public int LFTiresUsed => _headers.TryGetValue(nameof(LFTiresUsed), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF tire left percent tread remaining
        /// </summary>
        public float LFwearL => _headers.TryGetValue(nameof(LFwearL), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF tire middle percent tread remaining
        /// </summary>
        public float LFwearM => _headers.TryGetValue(nameof(LFwearM), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LF tire right percent tread remaining
        /// </summary>
        public float LFwearR => _headers.TryGetValue(nameof(LFwearR), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// True if the car_num texture will be loaded
        /// </summary>
        public bool LoadNumTextures => _headers.TryGetValue(nameof(LoadNumTextures), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Longitudinal acceleration (including gravity)
        /// </summary>
        public float LongAccel => _headers.TryGetValue(nameof(LongAccel), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Longitudinal acceleration (including gravity) at 360 Hz
        /// </summary>
        public float LongAccel_ST => _headers.TryGetValue(nameof(LongAccel_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR tire cold pressure  as set in the garage
        /// </summary>
        public float LRcoldPressure => _headers.TryGetValue(nameof(LRcoldPressure), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR shock deflection
        /// </summary>
        public float LRshockDefl => _headers.TryGetValue(nameof(LRshockDefl), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR shock deflection at 360 Hz
        /// </summary>
        public float LRshockDefl_ST => _headers.TryGetValue(nameof(LRshockDefl_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR shock velocity
        /// </summary>
        public float LRshockVel => _headers.TryGetValue(nameof(LRshockVel), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR shock velocity at 360 Hz
        /// </summary>
        public float LRshockVel_ST => _headers.TryGetValue(nameof(LRshockVel_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR tire left carcass temperature
        /// </summary>
        public float LRtempCL => _headers.TryGetValue(nameof(LRtempCL), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR tire middle carcass temperature
        /// </summary>
        public float LRtempCM => _headers.TryGetValue(nameof(LRtempCM), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR tire right carcass temperature
        /// </summary>
        public float LRtempCR => _headers.TryGetValue(nameof(LRtempCR), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many left rear tires are remaining  255 is unlimited
        /// </summary>
        public int LRTiresAvailable => _headers.TryGetValue(nameof(LRTiresAvailable), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many left rear tires used so far
        /// </summary>
        public int LRTiresUsed => _headers.TryGetValue(nameof(LRTiresUsed), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR tire left percent tread remaining
        /// </summary>
        public float LRwearL => _headers.TryGetValue(nameof(LRwearL), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR tire middle percent tread remaining
        /// </summary>
        public float LRwearM => _headers.TryGetValue(nameof(LRwearM), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// LR tire right percent tread remaining
        /// </summary>
        public float LRwearR => _headers.TryGetValue(nameof(LRwearR), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine manifold pressure
        /// </summary>
        public float ManifoldPress => _headers.TryGetValue(nameof(ManifoldPress), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Hybrid manual boost state
        /// </summary>
        public bool ManualBoost => _headers.TryGetValue(nameof(ManualBoost), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Hybrid manual no boost state
        /// </summary>
        public bool ManualNoBoost => _headers.TryGetValue(nameof(ManualNoBoost), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Memory page faults per second
        /// </summary>
        public float MemPageFaultSec => _headers.TryGetValue(nameof(MemPageFaultSec), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine oil level
        /// </summary>
        public float OilLevel => _headers.TryGetValue(nameof(OilLevel), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine oil pressure
        /// </summary>
        public float OilPress => _headers.TryGetValue(nameof(OilPress), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine oil temperature
        /// </summary>
        public float OilTemp => _headers.TryGetValue(nameof(OilTemp), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// True if it is ok to reload car textures at this time
        /// </summary>
        public bool OkToReloadTextures => _headers.TryGetValue(nameof(OkToReloadTextures), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Is the player car on pit road between the cones
        /// </summary>
        public bool OnPitRoad => _headers.TryGetValue(nameof(OnPitRoad), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Are we pacing or not
        /// </summary>
        public PaceMode PaceMode => _headers.TryGetValue(nameof(PaceMode), out var header)
            ? (PaceMode) _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitch orientation
        /// </summary>
        public float Pitch => _headers.TryGetValue(nameof(Pitch), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitch rate
        /// </summary>
        public float PitchRate => _headers.TryGetValue(nameof(PitchRate), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pitch rate at 360 Hz
        /// </summary>
        public float PitchRate_ST => _headers.TryGetValue(nameof(PitchRate_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Time left for optional repairs if repairs are active
        /// </summary>
        public float PitOptRepairLeft => _headers.TryGetValue(nameof(PitOptRepairLeft), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Time left for mandatory pit repairs if repairs are active
        /// </summary>
        public float PitRepairLeft => _headers.TryGetValue(nameof(PitRepairLeft), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// True if pit stop is allowed for the current player
        /// </summary>
        public bool PitsOpen => _headers.TryGetValue(nameof(PitsOpen), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Is the player getting pit stop service
        /// </summary>
        public bool PitstopActive => _headers.TryGetValue(nameof(PitstopActive), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Bitfield of pit service checkboxes
        /// </summary>
        public PitServiceFlags PitSvSessionFlags => _headers.TryGetValue(nameof(PitSvSessionFlags), out var header)
            ? (PitServiceFlags) _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pit service fuel add amount
        /// </summary>
        public float PitSvFuel => _headers.TryGetValue(nameof(PitSvFuel), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pit service left front tire pressure
        /// </summary>
        public float PitSvLFP => _headers.TryGetValue(nameof(PitSvLFP), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pit service left rear tire pressure
        /// </summary>
        public float PitSvLRP => _headers.TryGetValue(nameof(PitSvLRP), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pit service right front tire pressure
        /// </summary>
        public float PitSvRFP => _headers.TryGetValue(nameof(PitSvRFP), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pit service right rear tire pressure
        /// </summary>
        public float PitSvRRP => _headers.TryGetValue(nameof(PitSvRRP), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Pit service pending tire compound
        /// </summary>
        public int PitSvTireCompound => _headers.TryGetValue(nameof(PitSvTireCompound), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Player car class id
        /// </summary>
        public int PlayerCarClass => _headers.TryGetValue(nameof(PlayerCarClass), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players class position in race
        /// </summary>
        public int PlayerCarClassPosition => _headers.TryGetValue(nameof(PlayerCarClassPosition), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Teams current drivers incident count for this session
        /// </summary>
        public int PlayerCarDriverIncidentCount =>
            _headers.TryGetValue(nameof(PlayerCarDriverIncidentCount), out var header)
                ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Players dry tire set limit
        /// </summary>
        public int PlayerCarDryTireSetLimit =>
            _headers.TryGetValue(nameof(PlayerCarDryTireSetLimit), out var header)
                ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Players carIdx
        /// </summary>
        public int PlayerCarIdx => _headers.TryGetValue(nameof(PlayerCarIdx), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players car is properly in there pitstall
        /// </summary>
        public bool PlayerCarInPitStall => _headers.TryGetValue(nameof(PlayerCarInPitStall), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players own incident count for this session
        /// </summary>
        public int PlayerCarMyIncidentCount =>
            _headers.TryGetValue(nameof(PlayerCarMyIncidentCount), out var header)
                ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Players car pit service status bits
        /// </summary>
        public PitServiceStatus PlayerCarPitSvStatus =>
            _headers.TryGetValue(nameof(PlayerCarPitSvStatus), out var header)
                ? (PitServiceStatus) _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Players position in race
        /// </summary>
        public int PlayerCarPosition => _headers.TryGetValue(nameof(PlayerCarPosition), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players power adjust
        /// </summary>
        public float PlayerCarPowerAdjust => _headers.TryGetValue(nameof(PlayerCarPowerAdjust), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players team incident count for this session
        /// </summary>
        public int PlayerCarTeamIncidentCount =>
            _headers.TryGetValue(nameof(PlayerCarTeamIncidentCount), out var header)
                ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Players car is being towed if time is greater than zero
        /// </summary>
        public float PlayerCarTowTime => _headers.TryGetValue(nameof(PlayerCarTowTime), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players weight penalty
        /// </summary>
        public float PlayerCarWeightPenalty => _headers.TryGetValue(nameof(PlayerCarWeightPenalty), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players car number of fast repairs used
        /// </summary>
        public int PlayerFastRepairsUsed => _headers.TryGetValue(nameof(PlayerFastRepairsUsed), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players car current tire compound
        /// </summary>
        public int PlayerTireCompound => _headers.TryGetValue(nameof(PlayerTireCompound), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players car track surface type
        /// </summary>
        public TrackSurface PlayerTrackSurface => _headers.TryGetValue(nameof(PlayerTrackSurface), out var header)
            ? (TrackSurface) _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players car track surface material type
        /// </summary>
        public TrackSurfaceMaterial PlayerTrackSurfaceMaterial =>
            _headers.TryGetValue(nameof(PlayerTrackSurfaceMaterial), out var header)
                ? (TrackSurfaceMaterial) _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Push to pass button state
        /// </summary>
        public bool PushToPass => _headers.TryGetValue(nameof(PushToPass), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Laps completed in race
        /// </summary>
        public int RaceLaps => _headers.TryGetValue(nameof(RaceLaps), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        private int[] _radioTransmitCarIdx = new int[64];

        /// <summary>
        /// The car index of the current person speaking on the radio
        /// </summary>
        public int[] RadioTransmitCarIdx
        {
            get
            {
                if (_headers.TryGetValue(nameof(RadioTransmitCarIdx), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _radioTransmitCarIdx, 0, 64);
                }

                return _radioTransmitCarIdx;
            }
        }

        private int[] _radioTransmitFrequencyIdx = new int[64];

        /// <summary>
        /// The frequency index of the current person speaking on the radio
        /// </summary>
        public int[] RadioTransmitFrequencyIdx
        {
            get
            {
                if (_headers.TryGetValue(nameof(RadioTransmitFrequencyIdx), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _radioTransmitFrequencyIdx, 0, 64);
                }

                return _radioTransmitFrequencyIdx;
            }
        }

        private int[] _radioTransmitRadioIdx = new int[64];

        /// <summary>
        /// The radio index of the current person speaking on the radio
        /// </summary>
        public int[] RadioTransmitRadioIdx
        {
            get
            {
                if (_headers.TryGetValue(nameof(RadioTransmitRadioIdx), out var header))
                {
                    _fileView.ReadArray(_sdk.Header.Offset + header.Offset, _radioTransmitRadioIdx, 0, 64);
                }

                return _radioTransmitRadioIdx;
            }
        }

        /// <summary>
        /// How many rear tire sets are remaining  255 is unlimited
        /// </summary>
        public int RearTireSetsAvailable => _headers.TryGetValue(nameof(RearTireSetsAvailable), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many rear tire sets used so far
        /// </summary>
        public int RearTireSetsUsed => _headers.TryGetValue(nameof(RearTireSetsUsed), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Relative Humidity
        /// </summary>
        public float RelativeHumidity => _headers.TryGetValue(nameof(RelativeHumidity), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// public integer replay frame number (60 per second)
        /// </summary>
        public int ReplayFrameNum => _headers.TryGetValue(nameof(ReplayFrameNum), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// public integer replay frame number from end of tape
        /// </summary>
        public int ReplayFrameNumEnd => _headers.TryGetValue(nameof(ReplayFrameNumEnd), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 0=not slow motion  1=replay is in slow motion
        /// </summary>
        public bool ReplayPlaySlowMotion => _headers.TryGetValue(nameof(ReplayPlaySlowMotion), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Replay playback speed
        /// </summary>
        public int ReplayPlaySpeed => _headers.TryGetValue(nameof(ReplayPlaySpeed), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Replay session number
        /// </summary>
        public int ReplaySessionNum => _headers.TryGetValue(nameof(ReplaySessionNum), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Seconds since replay session start
        /// </summary>
        public double ReplaySessionTime;

        /// <summary>
        /// RF tire cold pressure  as set in the garage
        /// </summary>
        public float RFcoldPressure => _headers.TryGetValue(nameof(RFcoldPressure), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF shock deflection
        /// </summary>
        public float RFshockDefl => _headers.TryGetValue(nameof(RFshockDefl), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF shock deflection at 360 Hz
        /// </summary>
        public float RFshockDefl_ST => _headers.TryGetValue(nameof(RFshockDefl_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF shock velocity
        /// </summary>
        public float RFshockVel => _headers.TryGetValue(nameof(RFshockVel), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF shock velocity at 360 Hz
        /// </summary>
        public float RFshockVel_ST => _headers.TryGetValue(nameof(RFshockVel_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF tire left carcass temperature
        /// </summary>
        public float RFtempCL => _headers.TryGetValue(nameof(RFtempCL), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF tire middle carcass temperature
        /// </summary>
        public float RFtempCM => _headers.TryGetValue(nameof(RFtempCM), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF tire right carcass temperature
        /// </summary>
        public float RFtempCR => _headers.TryGetValue(nameof(RFtempCR), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many right front tires are remaining  255 is unlimited
        /// </summary>
        public int RFTiresAvailable => _headers.TryGetValue(nameof(RFTiresAvailable), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many right front tires used so far
        /// </summary>
        public int RFTiresUsed => _headers.TryGetValue(nameof(RFTiresUsed), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF tire left percent tread remaining
        /// </summary>
        public float RFwearL => _headers.TryGetValue(nameof(RFwearL), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF tire middle percent tread remaining
        /// </summary>
        public float RFwearM => _headers.TryGetValue(nameof(RFwearM), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RF tire right percent tread remaining
        /// </summary>
        public float RFwearR => _headers.TryGetValue(nameof(RFwearR), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many right tire sets are remaining  255 is unlimited
        /// </summary>
        public int RightTireSetsAvailable => _headers.TryGetValue(nameof(RightTireSetsAvailable), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many right tire sets used so far
        /// </summary>
        public int RightTireSetsUsed => _headers.TryGetValue(nameof(RightTireSetsUsed), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Roll orientation
        /// </summary>
        public float Roll => _headers.TryGetValue(nameof(Roll), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Roll rate
        /// </summary>
        public float RollRate => _headers.TryGetValue(nameof(RollRate), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Roll rate at 360 Hz
        /// </summary>
        public float RollRate_ST => _headers.TryGetValue(nameof(RollRate_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine rpm
        /// </summary>
        public float RPM => _headers.TryGetValue(nameof(RPM), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR tire cold pressure  as set in the garage
        /// </summary>
        public float RRcoldPressure => _headers.TryGetValue(nameof(RRcoldPressure), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR shock deflection
        /// </summary>
        public float RRshockDefl => _headers.TryGetValue(nameof(RRshockDefl), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR shock deflection at 360 Hz
        /// </summary>
        public float RRshockDefl_ST => _headers.TryGetValue(nameof(RRshockDefl_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR shock velocity
        /// </summary>
        public float RRshockVel => _headers.TryGetValue(nameof(RRshockVel), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR shock velocity at 360 Hz
        /// </summary>
        public float RRshockVel_ST => _headers.TryGetValue(nameof(RRshockVel_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR tire left carcass temperature
        /// </summary>
        public float RRtempCL => _headers.TryGetValue(nameof(RRtempCL), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR tire middle carcass temperature
        /// </summary>
        public float RRtempCM => _headers.TryGetValue(nameof(RRtempCM), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR tire right carcass temperature
        /// </summary>
        public float RRtempCR => _headers.TryGetValue(nameof(RRtempCR), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many right rear tires are remaining  255 is unlimited
        /// </summary>
        public int RRTiresAvailable => _headers.TryGetValue(nameof(RRTiresAvailable), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many right rear tires used so far
        /// </summary>
        public int RRTiresUsed => _headers.TryGetValue(nameof(RRTiresUsed), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR tire left percent tread remaining
        /// </summary>
        public float RRwearL => _headers.TryGetValue(nameof(RRwearL), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR tire middle percent tread remaining
        /// </summary>
        public float RRwearM => _headers.TryGetValue(nameof(RRwearM), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RR tire right percent tread remaining
        /// </summary>
        public float RRwearR => _headers.TryGetValue(nameof(RRwearR), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Session flags
        /// </summary>
        public SessionFlags SessionFlags => _headers.TryGetValue(nameof(SessionFlags), out var header)
            ? (SessionFlags) _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Old laps left till session ends use SessionLapsRemainEx
        /// </summary>
        public int SessionLapsRemain => _headers.TryGetValue(nameof(SessionLapsRemain), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// New improved laps left till session ends
        /// </summary>
        public int SessionLapsRemainEx => _headers.TryGetValue(nameof(SessionLapsRemainEx), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Total number of laps in session
        /// </summary>
        public int SessionLapsTotal => _headers.TryGetValue(nameof(SessionLapsTotal), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Session number
        /// </summary>
        public int SessionNum => _headers.TryGetValue(nameof(SessionNum), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Session state
        /// </summary>
        public int SessionState => _headers.TryGetValue(nameof(SessionState), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Current update number
        /// </summary>
        public int SessionTick => _headers.TryGetValue(nameof(SessionTick), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Seconds since session start
        /// </summary>
        public double SessionTime;

        /// <summary>
        /// Time of day in seconds
        /// </summary>
        public float SessionTimeOfDay => _headers.TryGetValue(nameof(SessionTimeOfDay), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Seconds left till session ends
        /// </summary>
        public double SessionTimeRemain;

        /// <summary>
        /// Total number of seconds in session
        /// </summary>
        public double SessionTimeTotal;

        /// <summary>
        /// Session ID
        /// </summary>
        public int SessionUniqueID => _headers.TryGetValue(nameof(SessionUniqueID), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// RPM of shifter grinding noise
        /// </summary>
        public float ShiftGrindRPM => _headers.TryGetValue(nameof(ShiftGrindRPM), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// DEPRECATED use DriverCarSLBlinkRPM instead
        /// </summary>
        public float ShiftIndicatorPct => _headers.TryGetValue(nameof(ShiftIndicatorPct), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Friction torque applied to gears when shifting or grinding
        /// </summary>
        public float ShiftPowerPct => _headers.TryGetValue(nameof(ShiftPowerPct), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Skies (0=clear/1=p cloudy/2=m cloudy/3=overcast)
        /// </summary>
        public int Skies => _headers.TryGetValue(nameof(Skies), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// GPS vehicle speed
        /// </summary>
        public float Speed => _headers.TryGetValue(nameof(Speed), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Steering wheel angle
        /// </summary>
        public float SteeringWheelAngle => _headers.TryGetValue(nameof(SteeringWheelAngle), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Steering wheel max angle
        /// </summary>
        public float SteeringWheelAngleMax => _headers.TryGetValue(nameof(SteeringWheelAngleMax), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Force feedback limiter strength limits impacts and oscillation
        /// </summary>
        public float SteeringWheelLimiter => _headers.TryGetValue(nameof(SteeringWheelLimiter), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Force feedback 
        /// </summary>
        public float SteeringWheelPctDamper => _headers.TryGetValue(nameof(SteeringWheelPctDamper), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Force feedback 
        /// </summary>
        public float SteeringWheelPctTorque => _headers.TryGetValue(nameof(SteeringWheelPctTorque), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Force feedback 
        /// </summary>
        public float SteeringWheelPctTorqueSign =>
            _headers.TryGetValue(nameof(SteeringWheelPctTorqueSign), out var header)
                ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Force feedback 
        /// </summary>
        public float SteeringWheelPctTorqueSignStops =>
            _headers.TryGetValue(nameof(SteeringWheelPctTorqueSignStops), out var header)
                ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Peak torque mapping to direct input units for FFB
        /// </summary>
        public float SteeringWheelPeakForceNm =>
            _headers.TryGetValue(nameof(SteeringWheelPeakForceNm), out var header)
                ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
                : default;

        /// <summary>
        /// Output torque on steering shaft
        /// </summary>
        public float SteeringWheelTorque => _headers.TryGetValue(nameof(SteeringWheelTorque), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Output torque on steering shaft at 360 Hz
        /// </summary>
        public float SteeringWheelTorque_ST => _headers.TryGetValue(nameof(SteeringWheelTorque_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// 0=off throttle to 1=full throttle
        /// </summary>
        public float Throttle => _headers.TryGetValue(nameof(Throttle), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Raw throttle input 0=off throttle to 1=full throttle
        /// </summary>
        public float ThrottleRaw => _headers.TryGetValue(nameof(ThrottleRaw), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players LF Tire Sound rumblestrip pitch
        /// </summary>
        public float TireLF_RumblePitch => _headers.TryGetValue(nameof(TireLF_RumblePitch), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players LR Tire Sound rumblestrip pitch
        /// </summary>
        public float TireLR_RumblePitch => _headers.TryGetValue(nameof(TireLR_RumblePitch), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players RF Tire Sound rumblestrip pitch
        /// </summary>
        public float TireRF_RumblePitch => _headers.TryGetValue(nameof(TireRF_RumblePitch), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Players RR Tire Sound rumblestrip pitch
        /// </summary>
        public float TireRR_RumblePitch => _headers.TryGetValue(nameof(TireRR_RumblePitch), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many tire sets are remaining  255 is unlimited
        /// </summary>
        public int TireSetsAvailable => _headers.TryGetValue(nameof(TireSetsAvailable), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// How many tire sets used so far
        /// </summary>
        public int TireSetsUsed => _headers.TryGetValue(nameof(TireSetsUsed), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Deprecated  set to TrackTempCrew
        /// </summary>
        public float TrackTemp => _headers.TryGetValue(nameof(TrackTemp), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Temperature of track measured by crew around track
        /// </summary>
        public float TrackTempCrew => _headers.TryGetValue(nameof(TrackTempCrew), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// X velocity
        /// </summary>
        public float VelocityX => _headers.TryGetValue(nameof(VelocityX), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// X velocity
        /// </summary>
        public float VelocityX_ST => _headers.TryGetValue(nameof(VelocityX_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Y velocity
        /// </summary>
        public float VelocityY => _headers.TryGetValue(nameof(VelocityY), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Y velocity
        /// </summary>
        public float VelocityY_ST => _headers.TryGetValue(nameof(VelocityY_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Z velocity
        /// </summary>
        public float VelocityZ => _headers.TryGetValue(nameof(VelocityZ), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Z velocity
        /// </summary>
        public float VelocityZ_ST => _headers.TryGetValue(nameof(VelocityZ_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Vertical acceleration (including gravity)
        /// </summary>
        public float VertAccel => _headers.TryGetValue(nameof(VertAccel), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Vertical acceleration (including gravity) at 360 Hz
        /// </summary>
        public float VertAccel_ST => _headers.TryGetValue(nameof(VertAccel_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// True if video currently being captured
        /// </summary>
        public bool VidCapActive => _headers.TryGetValue(nameof(VidCapActive), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// True if video capture system is enabled
        /// </summary>
        public bool VidCapEnabled => _headers.TryGetValue(nameof(VidCapEnabled), out var header)
            ? _fileView.ReadBoolean(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine voltage
        /// </summary>
        public float Voltage => _headers.TryGetValue(nameof(Voltage), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine coolant level
        /// </summary>
        public float WaterLevel => _headers.TryGetValue(nameof(WaterLevel), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Engine coolant temp
        /// </summary>
        public float WaterTemp => _headers.TryGetValue(nameof(WaterTemp), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Weather type (0=constant  1=dynamic)
        /// </summary>
        public int WeatherType => _headers.TryGetValue(nameof(WeatherType), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Wind direction at start/finish line
        /// </summary>
        public float WindDir => _headers.TryGetValue(nameof(WindDir), out var header)
            ? _fileView.ReadInt32(_sdk.Header.Offset + header.Offset)
            : default(float);

        /// <summary>
        /// Wind velocity at start/finish line
        /// </summary>
        public float WindVel => _headers.TryGetValue(nameof(WindVel), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Yaw orientation
        /// </summary>
        public float Yaw => _headers.TryGetValue(nameof(Yaw), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Yaw orientation relative to north
        /// </summary>
        public float YawNorth => _headers.TryGetValue(nameof(YawNorth), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Yaw rate
        /// </summary>
        public float YawRate => _headers.TryGetValue(nameof(YawRate), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        /// <summary>
        /// Yaw rate at 360 Hz
        /// </summary>
        public float YawRate_ST => _headers.TryGetValue(nameof(YawRate_ST), out var header)
            ? _fileView.ReadSingle(_sdk.Header.Offset + header.Offset)
            : default;

        public override string ToString()
        {
            return $@"Data:
AirDensity: {AirDensity.ToString()}
AirPressure: {AirPressure.ToString()}
AirTemp: {AirTemp.ToString()}
Brake: {Brake.ToString()}
BrakeABSactive: {BrakeABSactive.ToString()}
BrakeRaw: {BrakeRaw.ToString()}
CamCameraNumber: {CamCameraNumber.ToString()}
CamCameraState: {CamCameraState.ToString()}
CamCarIdx: {string.Join(',', CamCarIdx.Select(e => e.ToString()))}
CamGroupNumber: {CamGroupNumber.ToString()}
CarIdxBestLapNum: {string.Join(',', CarIdxBestLapNum.Select(e => e.ToString()))}
CarIdxBestLapTime: {string.Join(',', CarIdxBestLapTime.Select(e => e.ToString()))}
CarIdxClass: {string.Join(',', CarIdxClass.Select(e => e.ToString()))}
CarIdxClassPosition: {string.Join(',', CarIdxClassPosition.Select(e => e.ToString()))}
CarIdxEstTime: {string.Join(',', CarIdxEstTime.Select(e => e.ToString()))}
CarIdxF2Time: {string.Join(',', CarIdxF2Time.Select(e => e.ToString()))}
CarIdxFastRepairsUsed: {string.Join(',', CarIdxFastRepairsUsed.Select(e => e.ToString()))}
CarIdxGear: {string.Join(',', CarIdxGear.Select(e => e.ToString()))}
CarIdxLap: {string.Join(',', CarIdxLap.Select(e => e.ToString()))}
CarIdxLapCompleted: {string.Join(',', CarIdxLapCompleted.Select(e => e.ToString()))}
CarIdxLapDistPct: {string.Join(',', CarIdxLapDistPct.Select(e => e.ToString()))}
CarIdxLastLapTime: {string.Join(',', CarIdxLastLapTime.Select(e => e.ToString()))}
CarIdxOnPitRoad: {string.Join(',', CarIdxOnPitRoad.Select(e => e.ToString()))}
CarIdxP2P_Count: {string.Join(',', CarIdxP2P_Count.Select(e => e.ToString()))}
CarIdxP2P_Status: {string.Join(',', CarIdxP2P_Status.Select(e => e.ToString()))}
CarIdxPaceFlags: {string.Join(',', CarIdxPaceFlags.Select(e => e.ToString()))}
CarIdxPaceLine: {string.Join(',', CarIdxPaceLine.Select(e => e.ToString()))}
CarIdxPaceRow: {string.Join(',', CarIdxPaceRow.Select(e => e.ToString()))}
CarIdxPosition: {string.Join(',', CarIdxPosition.Select(e => e.ToString()))}
CarIdxQualTireCompound: {string.Join(',', CarIdxQualTireCompound.Select(e => e.ToString()))}
CarIdxQualTireCompoundLocked: {string.Join(',', CarIdxQualTireCompoundLocked.Select(e => e.ToString()))}
CarIdxRPM: {string.Join(',', CarIdxRPM.Select(e => e.ToString()))}
CarIdxSteer: {string.Join(',', CarIdxSteer.Select(e => e.ToString()))}
CarIdxTireCompound: {string.Join(',', CarIdxTireCompound.Select(e => e.ToString()))}
CarIdxTrackSurface: {string.Join(',', CarIdxTrackSurface.Select(e => e.ToString()))}
CarIdxTrackSurfaceMaterial: {string.Join(',', CarIdxTrackSurfaceMaterial.Select(e => e.ToString()))}
CarLeftRight: {CarLeftRight.ToString()}
ChanAvgLatency: {ChanAvgLatency.ToString()}
ChanClockSkew: {ChanClockSkew.ToString()}
ChanLatency: {ChanLatency.ToString()}
ChanPartnerQuality: {ChanPartnerQuality.ToString()}
ChanQuality: {ChanQuality.ToString()}
Clutch: {Clutch.ToString()}
CpuUsageBG: {CpuUsageBG.ToString()}
CpuUsageFG: {CpuUsageFG.ToString()}
DCDriversSoFar: {DCDriversSoFar.ToString()}
DCLapStatus: {DCLapStatus.ToString()}
dcStarter: {dcStarter.ToString()}
dcTearOffVisor: {dcTearOffVisor.ToString()}
DisplayUnits: {DisplayUnits.ToString()}
dpFastRepair: {dpFastRepair.ToString()}
dpFuelAddKg: {dpFuelAddKg.ToString()}
dpFuelFill: {dpFuelFill.ToString()}
dpLFTireChange: {dpLFTireChange.ToString()}
dpLFTireColdPress: {dpLFTireColdPress.ToString()}
dpLRTireChange: {dpLRTireChange.ToString()}
dpLRTireColdPress: {dpLRTireColdPress.ToString()}
dpRFTireChange: {dpRFTireChange.ToString()}
dpRFTireColdPress: {dpRFTireColdPress.ToString()}
dpRRTireChange: {dpRRTireChange.ToString()}
dpRRTireColdPress: {dpRRTireColdPress.ToString()}
DriverMarker: {DriverMarker.ToString()}
EngineWarnings: {EngineWarnings.ToString()}
EnterExitReset: {EnterExitReset.ToString()}
FastRepairAvailable: {FastRepairAvailable.ToString()}
FastRepairUsed: {FastRepairUsed.ToString()}
FogLevel: {FogLevel.ToString()}
FrameRate: {FrameRate.ToString()}
FrontTireSetsAvailable: {FrontTireSetsAvailable.ToString()}
FrontTireSetsUsed: {FrontTireSetsUsed.ToString()}
FuelLevel: {FuelLevel.ToString()}
FuelLevelPct: {FuelLevelPct.ToString()}
FuelPress: {FuelPress.ToString()}
FuelUsePerHour: {FuelUsePerHour.ToString()}
Gear: {Gear.ToString()}
GpuUsage: {GpuUsage.ToString()}
HandbrakeRaw: {HandbrakeRaw.ToString()}
IsDiskLoggingActive: {IsDiskLoggingActive.ToString()}
IsDiskLoggingEnabled: {IsDiskLoggingEnabled.ToString()}
IsInGarage: {IsInGarage.ToString()}
IsOnTrack: {IsOnTrack.ToString()}
IsOnTrackCar: {IsOnTrackCar.ToString()}
IsReplayPlaying: {IsReplayPlaying.ToString()}
Lap: {Lap.ToString()}
LapBestLap: {LapBestLap.ToString()}
LapBestLapTime: {LapBestLapTime.ToString()}
LapBestNLapLap: {LapBestNLapLap.ToString()}
LapBestNLapTime: {LapBestNLapTime.ToString()}
LapCompleted: {LapCompleted.ToString()}
LapCurrentLapTime: {LapCurrentLapTime.ToString()}
LapDeltaToBestLap: {LapDeltaToBestLap.ToString()}
LapDeltaToBestLap_DD: {LapDeltaToBestLap_DD.ToString()}
LapDeltaToBestLap_OK: {LapDeltaToBestLap_OK.ToString()}
LapDeltaToOptimalLap: {LapDeltaToOptimalLap.ToString()}
LapDeltaToOptimalLap_DD: {LapDeltaToOptimalLap_DD.ToString()}
LapDeltaToOptimalLap_OK: {LapDeltaToOptimalLap_OK.ToString()}
LapDeltaToSessionBestLap: {LapDeltaToSessionBestLap.ToString()}
LapDeltaToSessionBestLap_DD: {LapDeltaToSessionBestLap_DD.ToString()}
LapDeltaToSessionBestLap_OK: {LapDeltaToSessionBestLap_OK.ToString()}
LapDeltaToSessionLastlLap: {LapDeltaToSessionLastlLap.ToString()}
LapDeltaToSessionLastlLap_DD: {LapDeltaToSessionLastlLap_DD.ToString()}
LapDeltaToSessionLastlLap_OK: {LapDeltaToSessionLastlLap_OK.ToString()}
LapDeltaToSessionOptimalLap: {LapDeltaToSessionOptimalLap.ToString()}
LapDeltaToSessionOptimalLap_DD: {LapDeltaToSessionOptimalLap_DD.ToString()}
LapDeltaToSessionOptimalLap_OK: {LapDeltaToSessionOptimalLap_OK.ToString()}
LapDist: {LapDist.ToString()}
LapDistPct: {LapDistPct.ToString()}
LapLasNLapSeq: {LapLasNLapSeq.ToString()}
LapLastLapTime: {LapLastLapTime.ToString()}
LapLastNLapTime: {LapLastNLapTime.ToString()}
LatAccel: {LatAccel.ToString()}
LatAccel_ST: {LatAccel_ST.ToString()}
LeftTireSetsAvailable: {LeftTireSetsAvailable.ToString()}
LeftTireSetsUsed: {LeftTireSetsUsed.ToString()}
LFcoldPressure: {LFcoldPressure.ToString()}
LFshockDefl: {LFshockDefl.ToString()}
LFshockDefl_ST: {LFshockDefl_ST.ToString()}
LFshockVel: {LFshockVel.ToString()}
LFshockVel_ST: {LFshockVel_ST.ToString()}
LFtempCL: {LFtempCL.ToString()}
LFtempCM: {LFtempCM.ToString()}
LFtempCR: {LFtempCR.ToString()}
LFTiresAvailable: {LFTiresAvailable.ToString()}
LFTiresUsed: {LFTiresUsed.ToString()}
LFwearL: {LFwearL.ToString()}
LFwearM: {LFwearM.ToString()}
LFwearR: {LFwearR.ToString()}
LoadNumTextures: {LoadNumTextures.ToString()}
LongAccel: {LongAccel.ToString()}
LongAccel_ST: {LongAccel_ST.ToString()}
LRcoldPressure: {LRcoldPressure.ToString()}
LRshockDefl: {LRshockDefl.ToString()}
LRshockDefl_ST: {LRshockDefl_ST.ToString()}
LRshockVel: {LRshockVel.ToString()}
LRshockVel_ST: {LRshockVel_ST.ToString()}
LRtempCL: {LRtempCL.ToString()}
LRtempCM: {LRtempCM.ToString()}
LRtempCR: {LRtempCR.ToString()}
LRTiresAvailable: {LRTiresAvailable.ToString()}
LRTiresUsed: {LRTiresUsed.ToString()}
LRwearL: {LRwearL.ToString()}
LRwearM: {LRwearM.ToString()}
LRwearR: {LRwearR.ToString()}
ManifoldPress: {ManifoldPress.ToString()}
ManualBoost: {ManualBoost.ToString()}
ManualNoBoost: {ManualNoBoost.ToString()}
MemPageFaultSec: {MemPageFaultSec.ToString()}
OilLevel: {OilLevel.ToString()}
OilPress: {OilPress.ToString()}
OilTemp: {OilTemp.ToString()}
OkToReloadTextures: {OkToReloadTextures.ToString()}
OnPitRoad: {OnPitRoad.ToString()}
PaceMode: {PaceMode.ToString()}
Pitch: {Pitch.ToString()}
PitchRate: {PitchRate.ToString()}
PitchRate_ST: {PitchRate_ST.ToString()}
PitOptRepairLeft: {PitOptRepairLeft.ToString()}
PitRepairLeft: {PitRepairLeft.ToString()}
PitsOpen: {PitsOpen.ToString()}
PitstopActive: {PitstopActive.ToString()}
PitSvSessionFlags: {PitSvSessionFlags.ToString()}
PitSvFuel: {PitSvFuel.ToString()}
PitSvLFP: {PitSvLFP.ToString()}
PitSvLRP: {PitSvLRP.ToString()}
PitSvRFP: {PitSvRFP.ToString()}
PitSvRRP: {PitSvRRP.ToString()}
PitSvTireCompound: {PitSvTireCompound.ToString()}
PlayerCarClass: {PlayerCarClass.ToString()}
PlayerCarClassPosition: {PlayerCarClassPosition.ToString()}
PlayerCarDriverIncidentCount: {PlayerCarDriverIncidentCount.ToString()}
PlayerCarDryTireSetLimit: {PlayerCarDryTireSetLimit.ToString()}
PlayerCarIdx: {PlayerCarIdx.ToString()}
PlayerCarInPitStall: {PlayerCarInPitStall.ToString()}
PlayerCarMyIncidentCount: {PlayerCarMyIncidentCount.ToString()}
PlayerCarPitSvStatus: {PlayerCarPitSvStatus.ToString()}
PlayerCarPosition: {PlayerCarPosition.ToString()}
PlayerCarPowerAdjust: {PlayerCarPowerAdjust.ToString()}
PlayerCarTeamIncidentCount: {PlayerCarTeamIncidentCount.ToString()}
PlayerCarTowTime: {PlayerCarTowTime.ToString()}
PlayerCarWeightPenalty: {PlayerCarWeightPenalty.ToString()}
PlayerFastRepairsUsed: {PlayerFastRepairsUsed.ToString()}
PlayerTireCompound: {PlayerTireCompound.ToString()}
PlayerTrackSurface: {PlayerTrackSurface.ToString()}
PlayerTrackSurfaceMaterial: {PlayerTrackSurfaceMaterial.ToString()}
PushToPass: {PushToPass.ToString()}
RaceLaps: {RaceLaps.ToString()}
RadioTransmitCarIdx: {string.Join(',', RadioTransmitCarIdx.Select(e => e.ToString()))}
RadioTransmitFrequencyIdx: {string.Join(',', RadioTransmitFrequencyIdx.Select(e => e.ToString()))}
RadioTransmitRadioIdx: {string.Join(',', RadioTransmitRadioIdx.Select(e => e.ToString()))}
RearTireSetsAvailable: {RearTireSetsAvailable.ToString()}
RearTireSetsUsed: {RearTireSetsUsed.ToString()}
RelativeHumidity: {RelativeHumidity.ToString()}
ReplayFrameNum: {ReplayFrameNum.ToString()}
ReplayFrameNumEnd: {ReplayFrameNumEnd.ToString()}
ReplayPlaySlowMotion: {ReplayPlaySlowMotion.ToString()}
ReplayPlaySpeed: {ReplayPlaySpeed.ToString()}
ReplaySessionNum: {ReplaySessionNum.ToString()}
RFcoldPressure: {RFcoldPressure.ToString()}
RFshockDefl: {RFshockDefl.ToString()}
RFshockDefl_ST: {RFshockDefl_ST.ToString()}
RFshockVel: {RFshockVel.ToString()}
RFshockVel_ST: {RFshockVel_ST.ToString()}
RFtempCL: {RFtempCL.ToString()}
RFtempCM: {RFtempCM.ToString()}
RFtempCR: {RFtempCR.ToString()}
RFTiresAvailable: {RFTiresAvailable.ToString()}
RFTiresUsed: {RFTiresUsed.ToString()}
RFwearL: {RFwearL.ToString()}
RFwearM: {RFwearM.ToString()}
RFwearR: {RFwearR.ToString()}
RightTireSetsAvailable: {RightTireSetsAvailable.ToString()}
RightTireSetsUsed: {RightTireSetsUsed.ToString()}
Roll: {Roll.ToString()}
RollRate: {RollRate.ToString()}
RollRate_ST: {RollRate_ST.ToString()}
RPM: {RPM.ToString()}
RRcoldPressure: {RRcoldPressure.ToString()}
RRshockDefl: {RRshockDefl.ToString()}
RRshockDefl_ST: {RRshockDefl_ST.ToString()}
RRshockVel: {RRshockVel.ToString()}
RRshockVel_ST: {RRshockVel_ST.ToString()}
RRtempCL: {RRtempCL.ToString()}
RRtempCM: {RRtempCM.ToString()}
RRtempCR: {RRtempCR.ToString()}
RRTiresAvailable: {RRTiresAvailable.ToString()}
RRTiresUsed: {RRTiresUsed.ToString()}
RRwearL: {RRwearL.ToString()}
RRwearM: {RRwearM.ToString()}
RRwearR: {RRwearR.ToString()}
SessionFlags: {SessionFlags.ToString()}
SessionLapsRemain: {SessionLapsRemain.ToString()}
SessionLapsRemainEx: {SessionLapsRemainEx.ToString()}
SessionLapsTotal: {SessionLapsTotal.ToString()}
SessionNum: {SessionNum.ToString()}
SessionState: {SessionState.ToString()}
SessionTick: {SessionTick.ToString()}
SessionTimeOfDay: {SessionTimeOfDay.ToString()}
SessionUniqueID: {SessionUniqueID.ToString()}
ShiftGrindRPM: {ShiftGrindRPM.ToString()}
ShiftIndicatorPct: {ShiftIndicatorPct.ToString()}
ShiftPowerPct: {ShiftPowerPct.ToString()}
Skies: {Skies.ToString()}
Speed: {Speed.ToString()}
SteeringWheelAngle: {SteeringWheelAngle.ToString()}
SteeringWheelAngleMax: {SteeringWheelAngleMax.ToString()}
SteeringWheelLimiter: {SteeringWheelLimiter.ToString()}
SteeringWheelPctDamper: {SteeringWheelPctDamper.ToString()}
SteeringWheelPctTorque: {SteeringWheelPctTorque.ToString()}
SteeringWheelPctTorqueSign: {SteeringWheelPctTorqueSign.ToString()}
SteeringWheelPctTorqueSignStops: {SteeringWheelPctTorqueSignStops.ToString()}
SteeringWheelPeakForceNm: {SteeringWheelPeakForceNm.ToString()}
SteeringWheelTorque: {SteeringWheelTorque.ToString()}
SteeringWheelTorque_ST: {SteeringWheelTorque_ST.ToString()}
Throttle: {Throttle.ToString()}
ThrottleRaw: {ThrottleRaw.ToString()}
TireLF_RumblePitch: {TireLF_RumblePitch.ToString()}
TireLR_RumblePitch: {TireLR_RumblePitch.ToString()}
TireRF_RumblePitch: {TireRF_RumblePitch.ToString()}
TireRR_RumblePitch: {TireRR_RumblePitch.ToString()}
TireSetsAvailable: {TireSetsAvailable.ToString()}
TireSetsUsed: {TireSetsUsed.ToString()}
TrackTemp: {TrackTemp.ToString()}
TrackTempCrew: {TrackTempCrew.ToString()}
VelocityX: {VelocityX.ToString()}
VelocityX_ST: {VelocityX_ST.ToString()}
VelocityY: {VelocityY.ToString()}
VelocityY_ST: {VelocityY_ST.ToString()}
VelocityZ: {VelocityZ.ToString()}
VelocityZ_ST: {VelocityZ_ST.ToString()}
VertAccel: {VertAccel.ToString()}
VertAccel_ST: {VertAccel_ST.ToString()}
VidCapActive: {VidCapActive.ToString()}
VidCapEnabled: {VidCapEnabled.ToString()}
Voltage: {Voltage.ToString()}
WaterLevel: {WaterLevel.ToString()}
WaterTemp: {WaterTemp.ToString()}
WeatherType: {WeatherType.ToString()}
WindDir: {WindDir.ToString()}
WindVel: {WindVel.ToString()}
Yaw: {Yaw.ToString()}
YawNorth: {YawNorth.ToString()}
YawRate: {YawRate.ToString()}
YawRate_ST: {YawRate_ST.ToString()}
";
        }
    }
}