using MetricsAgent.Services.impl;
using MetricsAgent.Models;
using Core;

namespace MetricsAgent.DAL.Interfaces
{
    //наследуемся от интерфейса базового
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {
        IList<CpuMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
