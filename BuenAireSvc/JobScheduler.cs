using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuenAireSvc
{
    public class JobScheduler
    {
        public async static void Start()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<SensorFetchJob>()
                .WithIdentity("purpleair-sensor-fetch")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                  )
                .Build();

            await scheduler.ScheduleJob(job, trigger);
            await scheduler.TriggerJob(new JobKey("purpleair-sensor-fetch"));
        }
    }
}
