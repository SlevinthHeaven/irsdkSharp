using System.IO.MemoryMappedFiles;
using System.Reflection;
using irsdkSharp.Models;
using NUnit.Framework;

namespace irsdkSharp.Tests
{
    public class Tests
    {
        IRacingSDK sdk;

        [OneTimeSetUp]
        public void Setup()
        {
            var memMap = MemoryMappedFile.CreateFromFile("..\\..\\..\\testdata\\session.ibt");
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
            StringAssert.Contains(session, "WeekendInfo:");
        }

        [Test]
        public void GetSessionProperty()
        {
            Assert.NotZero(sdk.Session.SessionTick);
        }

        [Test]
        public void GetData()
        {
            Assert.NotZero((int)sdk.GetData(nameof(sdk.Session.SessionTick)));
        }

        [Test]
        public void GetAllSessionProperties()
        {
            var props = typeof(Session).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                var val = prop.GetValue(sdk.Session);
                TestContext.WriteLine($"{prop.Name}: {val.ToString()}");
            }
        }
    }
}