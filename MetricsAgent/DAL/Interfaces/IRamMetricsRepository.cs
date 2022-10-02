using MetricsAgent.Models;
using Core;

namespace MetricsAgent.DAL.Interfaces
{
    //расширение интерфейса
    public interface IRamMetricsRepository : IRepository<RamMetrics>
    {
        IList<RamMetrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
