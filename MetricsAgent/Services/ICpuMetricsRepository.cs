using MetricsAgent.Services.impl;
using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    //наследуемся от интерфейса базового
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {
        IList<CpuMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
