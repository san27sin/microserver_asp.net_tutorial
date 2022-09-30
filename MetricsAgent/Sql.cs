using System.Data.SQLite;

namespace MetricsAgent
{
    public static class Sql
    {
        /// <summary>
        /// Данный метод подготавливает нам локальную базу данных metrics.db
        /// </summary>
        public static void ConfigerSqlLiteConnection()
        {
            const string connectionString = "Data Source = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100;";
            var connection = new SQLiteConnection(connectionString);//передаем объект который установил соединение с базой данных
            connection.Open();
        }

        private static void PrepareSchema(SQLiteConnection connection)
        {
            using(var command = new SQLiteCommand(connection))
            {
                //задаем новый текст команды для выполнения
                //удаляем таблицу с метриками, если она есть в базе данных
                command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                //отправляем запрос в базу данных
                command.ExecuteNonQuery();
                command.CommandText =
                    @"CREATE TABLE cpumetrics(id INTEGER" +
                    @"PRIMARY KEY," +
                    @"value INT, time INT)";
                command.ExecuteNonQuery();
            }
        }
    }
}
