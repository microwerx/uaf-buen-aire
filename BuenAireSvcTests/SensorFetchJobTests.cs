using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuenAireSvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Quartz;
using NSubstitute;
using System.Threading;

namespace BuenAireSvc.Tests
{
    [TestClass()]
    public class SensorFetchJobTests
    {
        [TestMethod()]
        [Ignore]
        public async System.Threading.Tasks.Task ExecuteTestAsync()
        {
            var oldFile = new FileInfo(".\\Data\\purpleair_sensors.json");

            SensorFetchJob sensorFetchJob = new SensorFetchJob();
            var context = Substitute.For<IJobExecutionContext>();
            await sensorFetchJob.Execute(context);
            var newFile = new FileInfo(".\\Data\\purpleair_sensors.json");
            
            Assert.IsTrue(newFile.LastWriteTimeUtc != oldFile.LastWriteTimeUtc);
        }
    }
}
