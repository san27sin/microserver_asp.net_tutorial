using MetricsAgent.Models;
using Core;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IHddMetricsRepository : IRepository<HddMetrics>
    {
        IList<HddMetrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
