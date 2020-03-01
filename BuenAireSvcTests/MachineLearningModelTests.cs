using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuenAireSvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BuenAireSvc.Tests
{
    [TestClass()]
    public class MachineLearningModelTests
    {
        [TestMethod()]
        public void IngestDataTest()
        {
            MachineLearningModel model = new MachineLearningModel();

            _ = model.IngestData();

            Thread.Sleep(3000);

            Assert.IsTrue(model.dataStored());
        }
    }
}
