using System.Data;

namespace microserver_asp.net_tutorial.Models
{
    /// <summary>
    /// Будет хранить показатели температуры
    /// </summary>
    public class WeatherForecastHolder
    {

        //Коллекция для хранения показателей температуры
        private List<WeatherForecast> _values;

        #region Конструкторы

        public WeatherForecastHolder()
        {
            //Инициализируем коллекцию для хранения показателей температуры
            _values = new List<WeatherForecast>();
        }
        #endregion

        /// <summary>
        /// Добавить новый показатель температуры
        /// </summary>
        /// <param name="date">Дата фиксации температуры</param>
        /// <param name="temperatureC">Показатель температуры</param>
        public void Add(DateTime date, int temperatureC)
        {
            _values.Add(new WeatherForecast() { Date = date, TemperatureC = temperatureC });
        }

        /// <summary>
        /// Обновить показатель температуры 
        /// </summary>
        /// <param name="date">Дата фиксации температуры</param>
        /// <param name="temperatureC">Показатель температуры</param>
        /// <returns>Результат выполнения операции</returns>
        public bool Update(DateTime date, int temperatureC)
        {
            foreach (var item in _values)
            {
                if (item.Date == date)
                {
                    item.TemperatureC = temperatureC;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Получить показатели температуры за временной период 
        /// </summary>
        /// <param name="dateFrom">Начальная дата</param>
        /// <param name="dateTo">Конечная дата</param>
        /// <returns>Коллекция показателей температуры</returns>
        public List<WeatherForecast> Get(DateTime dateFrom, DateTime dateTo)
        {
            //С помощью выборки создаем нужную нам коллекцию
            return _values.FindAll(item => item.Date >= dateFrom && item.Date <= dateTo);
        }

        /// <summary>
        /// Удалить показатель температуры на дату
        /// </summary>
        /// <param name="date">Дата фиксация показателя температуры</param>
        /// <returns>результат выполнения операции</returns>
        public bool Delete(DateTime date)
        {
            var itemForDelete = _values.Find(item => item.Date == date);

            if (itemForDelete == null)
                return false;
            else
            {
                _values.Remove(itemForDelete);
                return true;
            }
        }
    }
}
