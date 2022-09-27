using Microsoft.AspNetCore.Mvc;

namespace microserver_asp.net_tutorial.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : Controller
    {
        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsRamFromAll([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }

        [HttpGet("available/agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsRamFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
