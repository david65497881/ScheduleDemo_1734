using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace ScheduleDemo_1734.JobClass
{
    public class PrintJob : IJob
    {
        /// <summary>
        /// N秒打印一次
        /// </summary>
        public Task Execute(IJobExecutionContext context) 
        {
            Console.WriteLine($"第一個排程執行中(N秒打印一次)：{DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
