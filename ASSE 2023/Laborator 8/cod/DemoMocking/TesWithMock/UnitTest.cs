using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using DemoMocking;
using System.Diagnostics.CodeAnalysis;

namespace TesWithMock
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethodDay()
        {
            MockRepository mocks = new MockRepository();
            IDateTime timeController = mocks.StrictMock<IDateTime>();

            using (mocks.Record())
            { 
                Expect.Call(timeController.GetHour()).Return(9);
            }

            using (mocks.Playback())
            {
                string expectedImagePath = "sun.jpg";
                ImageManagement image = new ImageManagement();
                string path = image.GetImageForTimeOfDay(timeController);
                Assert.AreEqual(expectedImagePath, path);
            }
        }

        /// <summary>Tests the method for night time.</summary>
        [TestMethod]
        public void TestMethodNight()
        {
            MockRepository mocks = new MockRepository();
            IDateTime timeController = mocks.StrictMock<IDateTime>();

            using (mocks.Record())
            {
                Expect.Call(timeController.GetHour()).Return(22);
            }

            using (mocks.Playback())
            {
                string expectedImagePath = "moon.jpg";
                ImageManagement image = new ImageManagement();
                string path = image.GetImageForTimeOfDay(timeController);
                Assert.AreEqual(expectedImagePath, path);
            }
        }

        /// <summary>Tests the method for early time.</summary>
        [TestMethod]
        public void TestMethodEarly()
        {
            MockRepository mocks = new MockRepository();
            IDateTime timeController = mocks.StrictMock<IDateTime>();

            using (mocks.Record())
            {
                Expect.Call(timeController.GetHour()).Return(5);
            }

            using (mocks.Playback())
            {
                string expectedImagePath = "moon.jpg";
                ImageManagement image = new ImageManagement();
                string path = image.GetImageForTimeOfDay(timeController);
                Assert.AreEqual(expectedImagePath, path);
            }
        }
    }
}
