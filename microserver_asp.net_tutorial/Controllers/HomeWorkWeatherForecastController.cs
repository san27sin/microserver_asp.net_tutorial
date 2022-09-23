using microserver_asp.net_tutorial.Models;
using Microsoft.AspNetCore.Mvc;

namespace microserver_asp.Controllers
{
    //создадим класс для разбора
    //в конце название класса лучше добавлять Controller
    //каждый контроллер содержит определенный метод

    //http запрос помимо заголовка может еще содержать тело сообщения

    //На каждом контроллере у нас могут быть какие-то методы которые обрабатывают http запросы

    /*Для того чтобы http запрос определился где вызывать ему метод (одинаковый метод может быть в разных контроллерах),
     * есть понятие маршрутизатор который мы прописываем ввиде атрибута указывает на определенный контролер и метод (прописываем
     * его в атрибуте route[])?, то есть каждому контроллеру мы можем прописать уникальное имя в маршрутизаторе
     * 
     * Добавление CRUD методов - как с базой данных
     * 
     * RestFullApi - это некие обязательства
     * 
     * !!! Если не указываем маршрутизацию, то тогда конечный маршрут составляется именем метода
     *  
     *  IActionResult - интерфейс представляет из себя служебный интерфейс который имплментирует объект содержащий статус код ответа на запрос
     *
     * [FromQuery] - атрибут, есть еще FromBody, FromRoute
     * 
     * Если у нас публичный метод в контроллере, то мы обязаны указать какого он типа, надо добавить атрибут
     * httpGet/httpPost. Если это не сделать то swagger в качестве open api не сможет коммуницировать, 
     * произойдет исключение.
     * 
     * При запуске, swagger создает клиента в браузере
     * 
     * AddOpenAPI - дает возможность генерации JSON файла с помощью которого строится клиент
     *
     *Если к нашему сервесу обращаются 100 клиентов, то для каждого запроса создается новый
     *экземпляр контроллера
     *
     *
     */
    [Route("api/weather")]
    [ApiController]//атрибут можно использовать вместо наследования
    public class HomeWorkWeatherForecastController : ControllerBase//наследуемся от класса который содержит вспомагательные инструменты
    {
        private readonly WeatherForecastHolder _holder;

        public HomeWorkWeatherForecastController(WeatherForecastHolder holder)
        {
            _holder = holder;//инициализируем наше хранилище
        }

        [HttpPost("add")]//после этого атрибута у нас будет обращение к методу по маршруту "add", можно их и не добавлять
        public IActionResult Add([FromQuery]DateTime date, [FromQuery] int tempC)
        {
            //При вызове нашего метода добавляем данные в _holder
            _holder.Add(date, tempC);
            return Ok();//возвращяет ответ окей на запрос http 200
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date, [FromQuery] int tempC)
        {
            _holder.Update(date, tempC);
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime date)
        {
            _holder.Delete(date);
            return Ok();
        }

        [HttpGet("get")]
        public IActionResult Get([FromQuery] DateTime from, [FromQuery] DateTime to)
        {            
            //При возврате объекта, он будет сериализован в формате JSON
            return Ok(_holder.Get(from, to));//имеет 2 перегрузки
        }
    }
}
