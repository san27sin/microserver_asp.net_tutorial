using MetricsAgent.Models;
using MetricsAgent.Models.Request;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : Controller
    {
        private readonly ILogger<CpuMetricsController> _logger;

        //на уровне контроллера добавляем зависимость
        private readonly ICpuMetricsRepository _cpuMetricRepository;

        public CpuMetricsController(ICpuMetricsRepository cpuMetricRepository, ILogger<CpuMetricsController> logger)
        {
            _cpuMetricRepository = cpuMetricRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogInformation("Create cpu metrics.");
            _cpuMetricRepository.Create(new Models.CpuMetric
            {
                Value = request.Value,
                Time = (int)request.Time.TotalSeconds
            });
            return Ok();
        }

        

        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsCpuPersentiles([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] double percentile)
        {
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<CpuMetric>> GetMetricsCpu([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get all cpu metrics.");
            return Ok(_cpuMetricRepository.GetByTimePeriod(fromTime, toTime));
        }


    }
}
