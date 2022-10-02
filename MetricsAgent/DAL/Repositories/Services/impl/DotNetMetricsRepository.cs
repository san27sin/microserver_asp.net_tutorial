using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.impl
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public DotNetMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(DotNetMetrics item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("INSERT INTO dotnetmetrics(value,time) VALUES(@value,@time)",
                new
                {
                    value = item.Value,
                    time = item.Time
                });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("DELETE FROM dotnetmetrics WHERE id=@id",
                new
                {
                    id = id
                });
            connection.Open();
        }

        public IList<DotNetMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.Query<DotNetMetrics>("SELECT * FROM dotnetmetrics").ToList();            
        }

        public DotNetMetrics GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.QuerySingle<DotNetMetrics>("SELECT id, Time, Value FROM dotnetmetrics WHERE id=@id",
                new
                {
                    id = id
                });            
        }

        public IList<DotNetMetrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.Query<DotNetMetrics>("SELECT * FROM dotnetmetrics where time >= @timeFrom and time <= @timeTo",
                new
                {
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();
        }

        public void Update(DotNetMetrics item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);//подсоединился к базе
            connection.Execute("UPDATE dotnetmetrics SET value = @value, time = @time WHERE id = @id; ",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
        }
    }
}
