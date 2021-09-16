using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using irsdkSharp.Enums;
using irsdkSharp.Models;
using irsdkSharp.Serialization;
using irsdkSharp.Serialization.Models.Data;
using irsdkSharp.Serialization.Models.Session;
using NUnit.Framework;

namespace irsdkSharp.Tests
{
    public class Tests
    {
        IRacingSDK sdk;
        IRacingDataModel data;
        IRacingSessionModel session;

        [OneTimeSetUp]
        public void Setup()
        {
            var memMap = MemoryMappedFile.CreateFromFile(Path.Combine("testdata", "session.ibt"));
            sdk = new IRacingSDK(memMap.CreateViewAccessor());
            Assert.IsTrue(sdk.Startup(false));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            sdk.Shutdown();
        }

        [Test, Order(1)]
        public void GetSerializedSession()
        {
            session = sdk.GetSerializedSessionInfo();
            Assert.NotNull(session);
        }

        [Test, Order(1)]
        public void GetSerializedData()
        {
            data = sdk.GetSerializedData();
            Assert.NotNull(data);
        }

        [Test]
        public void SerializedSessionMulti()
        {
            Stopwatch stopWatch = new();
            stopWatch.Start();
            for (var i = 0; i < 1000; i++)
            {
                GetSerializedSession();
            }
            stopWatch.Stop();
            Console.WriteLine($"{nameof(SerializedSessionMulti)}: {stopWatch.ElapsedTicks / 1000}");

        }

        [Test]
        public void SerializedDataMulti()
        {
            Stopwatch stopWatch = new();
            stopWatch.Start();
            for (var i = 0; i < 1000; i++)
            {
                GetSerializedData();
            }
            stopWatch.Stop();
            Console.WriteLine($"{nameof(SerializedDataMulti)}: {stopWatch.ElapsedTicks / 1000}");
          
        }

        [Test]
        public void GetSessionInfo()
        {
            var session = sdk.GetSessionInfo();
            Assert.That(session, Does.Contain("WeekendInfo"));
        }

        [Test]
        public void GetDataProperty()
        {
            Assert.NotZero(data.Data.SessionTick);
        }

        [Test]
        public void GetData()
        {
            TestContext.WriteLine(data.Data.ToString());

        }

        [Test]
        public void GetPositions()
        {
            var positions = sdk.GetPositions(out double sessionTime);
            Assert.IsNotEmpty(positions);
            Assert.NotZero(sessionTime);
        }

        [Test]
        public void GetPositionsMulti()
        {
            Stopwatch stopWatch = new();
            stopWatch.Start();
            for (var i = 0; i < 1000; i++)
            {
                GetPositions();
            }
            stopWatch.Stop();
            Console.WriteLine($"{nameof(GetPositionsMulti)}: {stopWatch.ElapsedTicks / 1000}");
        }
    }
}