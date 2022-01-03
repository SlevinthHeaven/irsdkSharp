using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using irsdkSharp.Serialization;
using irsdkSharp.Serialization.Models.Data;
using irsdkSharp.Serialization.Models.Fastest;
using irsdkSharp.Serialization.Models.Session;

namespace irsdkSharp.Benchmark
{
    [MemoryDiagnoser]
    public class Runner
    {
        private readonly IRacingSDK sdk;
        private readonly IRacingDataModel _dataModel;
        private readonly Data _data;
        public Runner()
        {
            var memMap = MemoryMappedFile.CreateFromFile(Path.Combine("data", "session.ibt"));
            sdk = new IRacingSDK(memMap.CreateViewAccessor());

            _dataModel = sdk.GetSerializedData();
            _data = sdk.GetData();
        }

        public IRacingSessionModel SerializeSessionInformation() => sdk.GetSerializedSessionInfo();

        [Benchmark]
        public IRacingDataModel SerializeDataModel() => sdk.GetSerializedData();

        [Benchmark]
        public void AccessDataModel()
        {
            _ = _dataModel.Data.AirPressure;
        }

        [Benchmark]
        public void AccessDataModelAirDensity()
        {
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
            _ = _dataModel.Data.AirDensity;
        }


        [Benchmark]
        public Data Data() => sdk.GetData();

        [Benchmark]
        public void AccessData()
        {
            _ = _data.AirPressure;
        }

        [Benchmark]
        public void AccessDataAirDensity()
        {
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
            _ = _data.AirDensity;
        }

        //[Benchmark]
        public List<CarModel> Positions() => sdk.GetPositions(out var sessionTime);

        //[Benchmark]
        public List<PositionModel> PositionsNew() => sdk.GetPositionsNew();

    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Runner>();
        }
    }
}
