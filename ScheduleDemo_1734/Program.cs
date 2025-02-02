﻿using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using ScheduleDemo_1734.JobClass;

namespace ScheduleDemo_1734
{
    public class Program
    {
        //anync標註為非同步
        static async Task Main(string[] args)
        {


            int intervaSeconds = 0;
            TimeSpan specifiedTime = TimeSpan.Zero;
            bool isValid = false;
            bool isValidTime = false;   

            //while (!isValid)
            //{
            //    try
            //    {
            //        Console.WriteLine("請輸入間格秒數");
            //        intervaSeconds = int.Parse(Console.ReadLine());
            //    }
            //    catch (FormatException)
            //    {
            //        Console.WriteLine("輸入無效，請輸入有效的整數值。");
            //    }
            //}
            
            //使用while確保輸入的秒數正確
            while (!isValid) 
            {
                Console.WriteLine("請輸入整數的間格秒數");
                string input = Console.ReadLine();  
                if (int.TryParse(input,out intervaSeconds))
                {
                    isValid = true;
                }
                else 
                {
                    Console.WriteLine("請輸入有效的時間(整數)");
                }
            }

            //使用while確保輸入的秒數正確
            while (!isValidTime) 
            {
                Console.WriteLine("請輸入你想指定的時間（HH:mm）：");
                string inputTime = Console.ReadLine();
                if (TimeSpan.TryParseExact(inputTime, "hh\\:mm", null, out specifiedTime))
                {
                    isValidTime = true;
                }
                else 
                {
                    Console.WriteLine("請輸入有效的時間(例如13:34)");
                }
            }
            
            
            


            //建立排成
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            //第一個工作 : N秒打印一次
            IJobDetail job1 = JobBuilder.Create<PrintJob>()
                .WithIdentity("job1", "group1")
                .Build();
            ITrigger trigger1 = TriggerBuilder.Create()
                .WithIdentity("trigger1","group1")
                .StartNow()
                .WithSimpleSchedule(x => x  
                .WithIntervalInSeconds(intervaSeconds)
                .RepeatForever())
                .Build();

            //await等待非同步完成後執行。根據trigger1的規則來觸發並執行job1
            await scheduler.ScheduleJob(job1 , trigger1);

            // 第二個工作：在指定時間執行一次
            IJobDetail job2 = JobBuilder.Create<PrintJob2>()
                .WithIdentity("job2", "group2")
                .Build();

            DateTimeOffset runTime = DateBuilder.TodayAt((int)specifiedTime.Hours, (int)specifiedTime.Minutes, 0);
            ITrigger trigger2 = TriggerBuilder.Create()
                .WithIdentity("trigger2", "group2")
                .StartAt(runTime)
                .Build();


            await scheduler.ScheduleJob(job2, trigger2);


            Console.WriteLine("排程已啟動，按下任意鍵結束...");
            Console.ReadKey();
        }

        
    }
}
