using irsdkSharp.Models;
using System;
using System.Collections.Generic;

namespace irsdkSharp.Serialization.Models.Data
{
    public class CarModel
    {
        private readonly byte[] _data;
        private readonly Dictionary<string, VarHeader> _headers;
        public CarModel(int idx, byte[] data, Dictionary<string, VarHeader> headers)
        {
            CarIdx = idx;
            _data = data;
            _headers = headers;

        }

        public int CarIdx { get; set; }

        public int CarIdxBestLapNum => ValueSerializer.GetIntValue(nameof(CarIdxBestLapNum), _data, _headers);

        public float CarIdxBestLapTime => ValueSerializer.GetFloatValue(nameof(CarIdxBestLapTime), _data, _headers);

        public int CarIdxClassPosition => ValueSerializer.GetIntValue(nameof(CarIdxClassPosition), _data, _headers);

        public float CarIdxEstTime => ValueSerializer.GetFloatValue(nameof(CarIdxEstTime), _data, _headers);

        public float CarIdxF2Time => ValueSerializer.GetFloatValue(nameof(CarIdxF2Time), _data, _headers);

        public int CarIdxGear => ValueSerializer.GetIntValue(nameof(CarIdxGear), _data, _headers);

        public int CarIdxLap => ValueSerializer.GetIntValue(nameof(CarIdxLap), _data, _headers);

        public int CarIdxLapCompleted => ValueSerializer.GetIntValue(nameof(CarIdxLapCompleted), _data, _headers);

        public float CarIdxLapDistPct => ValueSerializer.GetFloatValue(nameof(CarIdxLapDistPct), _data, _headers);

        public float CarIdxLastLapTime => ValueSerializer.GetFloatValue(nameof(CarIdxLastLapTime), _data, _headers);

        public bool CarIdxOnPitRoad => ValueSerializer.GetBoolValue(nameof(CarIdxOnPitRoad), _data, _headers);

        public int CarIdxP2P_Count => ValueSerializer.GetIntValue(nameof(CarIdxP2P_Count), _data, _headers);

        public bool CarIdxP2P_Status => ValueSerializer.GetBoolValue(nameof(CarIdxP2P_Status), _data, _headers);

        public int CarIdxPosition => ValueSerializer.GetIntValue(nameof(CarIdxPosition), _data, _headers);

        public float CarIdxRPM => ValueSerializer.GetFloatValue(nameof(CarIdxRPM), _data, _headers);

        public float CarIdxSteer => ValueSerializer.GetFloatValue(nameof(CarIdxSteer), _data, _headers);

        public int CarIdxTrackSurface => _headers.TryGetValue(nameof(CarIdxTrackSurface), out var header)
            ? BitConverter.ToInt32(_data, header.Offset + ((header.Length / header.Count) * CarIdx))
            : -1;

        public int CarIdxTrackSurfaceMaterial => _headers.TryGetValue(nameof(CarIdxTrackSurfaceMaterial), out var header)
            ? BitConverter.ToInt32(_data, header.Offset + ((header.Length / header.Count) * CarIdx))
            : -1;
    }
}
