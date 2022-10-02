using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.Services.impl
{
    public class RamMetricsRepository : IRamMetricsRepository
    {

        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public RamMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(RamMetrics item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("INSERT INTO rammetrics(value,time) VALUES(@value,@time)",
                new
                {
                    value = item.Value,
                    time = item.Time
                });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            connection.Execute("DELETE FROM rammetrics WHERE id=@id",
                new
                {
                    id = id
                });
        }

        public IList<RamMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.Query<RamMetrics>("SELECT * FROM * rammetrics").ToList();
        }

        public RamMetrics GetById(int id)
        {

            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.QuerySingle<RamMetrics>("SELECT * FROM rammetrics WHERE id=@id",
                new
                {
                    id = id
                });
        }

        public IList<RamMetrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {

            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);
            return connection.Query<RamMetrics>("SELECT * FROM rammetrics where time >= @timeFrom and time <= @timeTo",
                new
                {
                    timeFrom = timeFrom.TotalSeconds,
                    timeTo = timeTo.TotalSeconds
                }).ToList();
        }

        public void Update(RamMetrics item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);//подсоединился к базе
            connection.Execute("UPDATE rammetrics SET value = @value, time = @time WHERE id = @id; ",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
        }
    }
}

