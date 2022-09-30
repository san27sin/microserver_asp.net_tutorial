using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface IHddMetricsRepository:IRepository<HddMetrics>
    {
        IList<HddMetrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
