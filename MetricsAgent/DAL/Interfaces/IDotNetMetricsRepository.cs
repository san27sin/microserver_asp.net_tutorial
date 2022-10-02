using MetricsAgent.Models;
using Core;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetrics>
    {
        IList<DotNetMetrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
