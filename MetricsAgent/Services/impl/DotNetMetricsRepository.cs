using MetricsAgent.Models;
using System.Data.SQLite;

namespace MetricsAgent.Services.impl
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private const string _connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        public void Create(DotNetMetrics item)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            //создаем команду
            using var cmd = new SQLiteCommand(connection);
            //Прописываем команду sql - запрос на вставку данных
            //так же мы не добавляем id в качестве параметра потому что он создается автоматически
            cmd.CommandText = "INSERT INTO dotnetmetrics(value,time) VALUES(@value,@time)";//параметр помечается @
            //добавляем параметры в запрос из нашего объекта
            cmd.Parameters.AddWithValue("@value", item.Value);
            //в таблице будем хранить время в секундах
            cmd.Parameters.AddWithValue("@time", item.Time);
            //подготовка команды к выполнению
            cmd.Prepare();
            //Выполнение команды
            cmd.ExecuteNonQuery();//выполнение команды без возврата результата
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            using var cmd = new SQLiteCommand(connection);
            //Прописываем в sql запрос удаление данных
            cmd.CommandText = "DELETE FROM dotnetmetrics WHERE id=@id";//where указывает условия удаления
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<DotNetMetrics> GetAll()
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            //прописываем запрос на получение всех данных
            cmd.CommandText = "SELECT * FROM dotnetmetrics";
            var returnList = new List<DotNetMetrics>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                //Пока есть что читать - читаем
                while (reader.Read())
                {
                    //Добавляем объект в список возврата
                    returnList.Add(new DotNetMetrics
                    {
                        Id = reader.GetInt32(0),//в скобочках указывается номер столбца
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt32(2)
                    });
                }
            }
            return returnList;
        }

        public DotNetMetrics GetById(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            //прописываем запрос на получение всех данных
            cmd.CommandText = "SELECT * FROM dotnetmetrics WHERE id=@id";//по айди
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                //Пока есть что читать - читаем
                if (reader.Read())//читает нашу таблицу до конца
                {
                    //Добавляем объект в список возврата
                    return new DotNetMetrics
                    {
                        Id = reader.GetInt32(0),//в скобочках указывается номер столбца
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt32(2)
                    };
                }
                else
                {
                    //если не нашел
                    return null;
                }
            }
        }

        public IList<DotNetMetrics> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            //прописываем запрос на получение всех данных
            cmd.CommandText = "SELECT * FROM dotnetmetrics where time >= @timeFrom and time <= @timeTo";
            cmd.Parameters.AddWithValue("@timeFrom", timeFrom.TotalSeconds);
            cmd.Parameters.AddWithValue("@timeTo", timeTo.TotalSeconds);
            var returnList = new List<DotNetMetrics>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                //Пока есть что читать - читаем
                while (reader.Read())
                {
                    //Добавляем объект в список возврата
                    returnList.Add(new DotNetMetrics
                    {
                        Id = reader.GetInt32(0),//в скобочках указывается номер столбца
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt32(2)
                    });
                }
            }
            return returnList;
        }

        public void Update(DotNetMetrics item)
        {
            using var connection = new SQLiteConnection(_connectionString);//подсоединился к базе
            using var cmd = new SQLiteCommand(connection);  //создал команду
            //Прописываем в команду sql-запрос на обновление данных
            cmd.CommandText = "UPDATE dotnetmetrics SET value = @value, time = @time WHERE id = @id; ";
            cmd.Parameters.AddWithValue("@id", item.Id);
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}
