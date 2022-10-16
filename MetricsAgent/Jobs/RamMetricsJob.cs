using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using MetricsAgent.Services.impl;
using Quartz;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;

namespace MetricsAgent.Jobs
{
    public class RamMetricsJob:IJob
    {
        private IRamMetricsRepository _ramMetricsRepository;
        private PerformanceCounter _ramCounter;//из библиотеки performance, нужна для сбора метрик

        public RamMetricsJob(IRamMetricsRepository RamMetricsRepository)
        {
            _ramMetricsRepository = RamMetricsRepository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            //Получаем значение занятости CPU
            float ramUsageInPercents = _ramCounter.NextValue();//возвращает процент загрузки нашего процессора

            //Узнаем, когда мы сняли значение метрики
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            //теперь записать что-то посредством репозитория
            _ramMetricsRepository.Create(new RamMetrics
            {
                Time = (long)time.TotalSeconds,
                Value = (int)ramUsageInPercents
            });

            return Task.CompletedTask;
        }
    }
}
