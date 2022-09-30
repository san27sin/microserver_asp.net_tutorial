using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    //расширение интерфейса
    public interface IRamMetricsRepository:IRepository<RamMetrics>
    {
        IList<RamMetrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
