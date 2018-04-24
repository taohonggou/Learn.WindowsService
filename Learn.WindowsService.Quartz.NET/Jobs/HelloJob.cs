using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.WindowsService.Quartz.NET.Jobs
{
    public class HelloJob : IJob
    {
        private ILog logger = LogManager.GetLogger(typeof(HelloJob));

        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => logger.Info("Greetings from HelloJob!"));
        }
    }
}
