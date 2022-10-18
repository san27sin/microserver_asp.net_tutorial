using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricsJob:IJob
    {
        private readonly IDotNetMetricsRepository _dotNetMetricsRepository;
        private PerformanceCounter _dotNetCounter;//из библиотеки performance, нужна для сбора метрик

        public DotNetMetricsJob(IDotNetMetricsRepository dotNetMetricsRepository)
        {
            _dotNetMetricsRepository = dotNetMetricsRepository;
            _dotNetCounter = new PerformanceCounter(".NET CLR Memory", "% Time in GC","_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            //Debug.WriteLine($"{DateTime.Now} > CpuMetricJob");

            //Получаем значение занятости CPU
            float cpuUsageInPercents = _dotNetCounter.NextValue();//возвращает процент загрузки нашего процессора

            //Узнаем, когда мы сняли значение метрики
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            //теперь записать что-то посредством репозитория
            _dotNetMetricsRepository.Create(new DotNetMetrics
            {
                Time = (long)time.TotalSeconds,
                Value = (int)cpuUsageInPercents
            });

            return Task.CompletedTask;
        }
    }
}
