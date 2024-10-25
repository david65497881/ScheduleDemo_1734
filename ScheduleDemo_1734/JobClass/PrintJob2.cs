using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleDemo_1734.JobClass
{
    /// <summary>
    /// 指定時間打印一次
    /// </summary>
    public class PrintJob2 : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"第二個排程執行中(指定時間打印)：{DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
