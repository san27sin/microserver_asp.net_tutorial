using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private PerformanceCounter _cpuCounter;//из библиотеки performance, нужна для сбора метрик

        public CpuMetricJob(ICpuMetricsRepository cpuMetricsRepository)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
            _cpuCounter = new PerformanceCounter("Processor","% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            //Debug.WriteLine($"{DateTime.Now} > CpuMetricJob");

            //Получаем значение занятости CPU
            float cpuUsageInPercents = _cpuCounter.NextValue();//возвращает процент загрузки нашего процессора

            //Узнаем, когда мы сняли значение метрики
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            //теперь записать что-то посредством репозитория
            _cpuMetricsRepository.Create(new CpuMetric
            {
                Time = (long)time.TotalSeconds,
                Value = (int)cpuUsageInPercents
            });

            return Task.CompletedTask;
        }
    }
}
