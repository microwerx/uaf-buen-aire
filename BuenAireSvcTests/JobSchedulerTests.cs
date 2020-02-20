using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuenAireSvc;
using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using Quartz.Impl;

namespace BuenAireSvc.Tests
{
    [TestClass()]
    public class JobSchedulerTests
    {
        [TestMethod()]
        public async System.Threading.Tasks.Task StartTestAsync()
        {
            JobScheduler.Start();
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            
            Assert.IsTrue(await scheduler.CheckExists(new JobKey("purpleair-sensor-fetch")));
        }
    }
}
