using MetricsAgent.Models.Request;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : Controller
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _iRamMetricsRepository;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository iRamMetricsRepository)
        {
            _logger = logger;
            _iRamMetricsRepository = iRamMetricsRepository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricsCreateRequest request)
        {
            _logger.LogInformation("Create rammetrics");
            _iRamMetricsRepository.Create(new Models.RamMetrics
            {
                Value = request.Value,
                Time = (int)request.Time.TotalSeconds
            });
            return Ok();
        }


        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsRam([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get all rammetrics.");
            return Ok(_iRamMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }
    }
}
