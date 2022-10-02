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
            PrepareSchema(connection);
        }

        private static void PrepareSchema(SQLiteConnection connection)
        {
            using(var command = new SQLiteCommand(connection))
            {
                //задаем новый текст команды для выполнения
                //удаляем таблицу с метриками, если она есть в базе данных
                command.CommandText =
                    "DROP TABLE IF EXISTS cpumetrics;" +
                    "DROP TABLE IF EXISTS dotnetmetrics;" +
                    "DROP TABLE IF EXISTS hddmetrics;" +
                    "DROP TABLE IF EXISTS networkmetrics;" +
                    "DROP TABLE IF EXISTS rammetrics";
                //отправляем запрос в базу данных
                command.ExecuteNonQuery();

                command.CommandText =
                    @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY," +
                    @"value INT, time INT);" +
                    @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY," +
                    @"value INT, time INT);" +
                    @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY," +
                    @"value INT, time INT);" +
                    @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY," +
                    @"value INT, time INT);" +
                    @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY," +
                    @"value INT, time INT);";

                command.ExecuteNonQuery();
            }
        }
    }
}
