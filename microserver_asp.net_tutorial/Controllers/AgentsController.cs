using microserver_asp.net_tutorial.Models;
using Microsoft.AspNetCore.Mvc;

namespace microserver_asp.net_tutorial.Controllers
{
    //[FromBody] - информация будет передаваться в теле самого запроса
    //[FromQuery] - данные берутся из строки запроса
    //[FromRoute] - данные берутся из значения маршрута


    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : Controller
    {
        private AgentPool _agentPool;

        public AgentsController(AgentPool agentPool)
        {
            _agentPool = agentPool;
        }   


        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] Models.AgentInfo agentInfo)
        {
            if (agentInfo != null)
                _agentPool.Add(agentInfo);

            return Ok();
        }


        //можно сделать активным агента
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            if(_agentPool.Values.ContainsKey(agentId))
                _agentPool.Values[agentId].Enable = true;   

            return Ok();    
        }
        
        //дезактивируем агента
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            if (_agentPool.Values.ContainsKey(agentId))
                _agentPool.Values[agentId].Enable = false;

            return Ok();
        }

        //Первый пункт дз
        /// <summary>
        /// получает список зарегистрированных объектов
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public IActionResult GetAllAgents()
        {
            return Ok(_agentPool.Get());
        }
    }
}
