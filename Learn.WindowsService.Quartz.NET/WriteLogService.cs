using Learn.WindowsService.Quartz.NET.Jobs;
using log4net;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Learn.WindowsService.Quartz.NET
{
    partial class WriteLogService : ServiceBase
    {
        private ILog logger = LogManager.GetLogger(typeof(WriteLogService));

        private IScheduler scheduler;

        public WriteLogService()
        {
            InitializeComponent();

            NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            scheduler = factory.GetScheduler().Result;

            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("job1", "group1").Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x =>
                    x.WithIntervalInSeconds(2)
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);

        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            scheduler.Start();
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            scheduler.Shutdown();
        }
    }
}
