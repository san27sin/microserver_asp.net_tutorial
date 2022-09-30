using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public interface INetworkMetricRepository:IRepository<NetworkMetric>
    {
        IList<NetworkMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
