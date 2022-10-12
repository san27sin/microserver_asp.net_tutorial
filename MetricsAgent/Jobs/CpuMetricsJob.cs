using MetricsAgent.DAL.Interfaces;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _cpuMetricsRepository;

        public CpuMetricJob(ICpuMetricsRepository cpuMetricsRepository)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
