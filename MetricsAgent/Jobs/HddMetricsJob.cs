using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using Quartz;
using System.Diagnostics;

namespace MetricsAgent.Jobs
{
    public class HddMetricsJob:IJob
    {
        private IHddMetricsRepository _hddMetricsRepository;
        private PerformanceCounter _hddCounter;//из библиотеки performance, нужна для сбора метрик

        public HddMetricsJob(IHddMetricsRepository hddMetricsRepository)
        {
            _hddMetricsRepository = hddMetricsRepository;
            _hddCounter = new PerformanceCounter("PhysicalDisk", "Disk Bytes/sec", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            //Получаем значение занятости CPU
            float hddUsageInPercents = _hddCounter.NextValue();//возвращает процент загрузки нашего процессора

            //Узнаем, когда мы сняли значение метрики
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            //теперь записать что-то посредством репозитория
            _hddMetricsRepository.Create(new HddMetrics
            {
                Time = (long)time.TotalSeconds,
                Value = (int)hddUsageInPercents
            });

            return Task.CompletedTask;
        }
    }
}
