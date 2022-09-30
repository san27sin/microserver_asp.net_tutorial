using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetrics>
    {
        IList<DotNetMetrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
