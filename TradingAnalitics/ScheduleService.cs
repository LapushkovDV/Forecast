using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using TradingAnalitics.Quartz;

namespace TradingAnalitics
{
    public class ScheduleService
    {
        private readonly IScheduler scheduler;

        public ScheduleService()
        {
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" },
                { "quartz.scheduler.instanceName", "MyScheduler" },
                { "quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz" },
                { "quartz.threadPool.threadCount", "3" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            scheduler = factory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void Start()
        {
            scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();

            ScheduleJobs();
        }
        
        public void ScheduleJobs()
        {
            IJobDetail job = JobBuilder.Create<TradeJob>()
                .WithIdentity("job1", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(22)
                    .RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            scheduler.ScheduleJob(job, trigger).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        public void Stop()
        {
            scheduler.Shutdown().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
