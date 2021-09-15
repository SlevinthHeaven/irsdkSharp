using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using irsdkSharp.Enums;
using irsdkSharp.Models;
using irsdkSharp.Serialization;
using irsdkSharp.Serialization.Models.Session;
using NUnit.Framework;

namespace irsdkSharp.Tests
{
    public class Tests
    {
        IRacingSDK sdk;

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


        [Test]
        public void GetSessionInfo()
        {
            var session = sdk.GetSessionInfo();
            Assert.That(session, Does.Contain("WeekendInfo"));
        }

        [Test]
        public void GetDataProperty()
        {
            Assert.NotZero(sdk.Data.SessionTick);
        }

        [Test]
        public void GetData()
        {
            TestContext.WriteLine(sdk.Data.ToString());
        }

        [Test]
        public void GetAllSessionProperties()
        {
            var props = typeof(Data).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                var val = prop.GetValue(sdk.Data);

                val = val switch
                {
                    int[] i when !val.GetType().GetElementType().IsEnum => string.Join(',', i),
                    Single[] s => string.Join(',', s),
                    bool[] b => string.Join(',', b),
                    TrackSurfaceMaterial[] s => string.Join(',', s.Select(e => e.ToString())),
                    TrackSurface[] s => string.Join(',', s.Select(e => e.ToString())),
                    _ => val
                };

                TestContext.WriteLine($"{prop.Name}: {val?.ToString()}");
            }
        }

        [Test]
        public void SessionDataSerialize()
        {
            string sessionYAML = sdk.GetSessionInfo();

            var model = IRacingSessionModel.Serialize(sessionYAML);
            Assert.NotNull(model);
        }

        [Test]
        public void GetPositions()
        {
            var positions = sdk.GetPositions();
            
            Assert.IsNotEmpty(positions);
        }
    }
}