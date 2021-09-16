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

        public int CarIdx { get; }

        public int CarIdxBestLapNum => ValueSerializer.GetIntValue(nameof(CarIdxBestLapNum), _data, _headers, CarIdx);

        public float CarIdxBestLapTime => ValueSerializer.GetFloatValue(nameof(CarIdxBestLapTime), _data, _headers, CarIdx);

        public int CarIdxClassPosition => ValueSerializer.GetIntValue(nameof(CarIdxClassPosition), _data, _headers, CarIdx);

        public float CarIdxEstTime => ValueSerializer.GetFloatValue(nameof(CarIdxEstTime), _data, _headers, CarIdx);

        public float CarIdxF2Time => ValueSerializer.GetFloatValue(nameof(CarIdxF2Time), _data, _headers, CarIdx);

        public int CarIdxGear => ValueSerializer.GetIntValue(nameof(CarIdxGear), _data, _headers, CarIdx);

        public int CarIdxLap => ValueSerializer.GetIntValue(nameof(CarIdxLap), _data, _headers, CarIdx);

        public int CarIdxLapCompleted => ValueSerializer.GetIntValue(nameof(CarIdxLapCompleted), _data, _headers, CarIdx);

        public float CarIdxLapDistPct => ValueSerializer.GetFloatValue(nameof(CarIdxLapDistPct), _data, _headers, CarIdx);

        public float CarIdxLastLapTime => ValueSerializer.GetFloatValue(nameof(CarIdxLastLapTime), _data, _headers, CarIdx);

        public bool CarIdxOnPitRoad => ValueSerializer.GetBoolValue(nameof(CarIdxOnPitRoad), _data, _headers, CarIdx);

        public int CarIdxP2P_Count => ValueSerializer.GetIntValue(nameof(CarIdxP2P_Count), _data, _headers, CarIdx);

        public bool CarIdxP2P_Status => ValueSerializer.GetBoolValue(nameof(CarIdxP2P_Status), _data, _headers, CarIdx);

        public int CarIdxPosition => ValueSerializer.GetIntValue(nameof(CarIdxPosition), _data, _headers, CarIdx);

        public float CarIdxRPM => ValueSerializer.GetFloatValue(nameof(CarIdxRPM), _data, _headers, CarIdx);

        public float CarIdxSteer => ValueSerializer.GetFloatValue(nameof(CarIdxSteer), _data, _headers, CarIdx);

        public int CarIdxTrackSurface => _headers.TryGetValue(nameof(CarIdxTrackSurface), out var header)
            ? BitConverter.ToInt32(_data, header.Offset + (4 * CarIdx))
            : -1;

        public int CarIdxTrackSurfaceMaterial => _headers.TryGetValue(nameof(CarIdxTrackSurfaceMaterial), out var header)
            ? BitConverter.ToInt32(_data, header.Offset + (4 * CarIdx))
            : -1;
    }
}
