using MetricsAgent.Models;
using Core;

namespace MetricsAgent.DAL.Interfaces
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {
        IList<NetworkMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
