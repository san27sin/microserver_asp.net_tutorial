using MetricsAgent.Models;
using MetricsAgent.Models.Request;
using MetricsAgent.Services;
using MetricsAgent.Services.impl;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : Controller
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IDotNetMetricsRepository _dotNetMetricsRepository;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository dotNetMetricsRepository)
        {
            _logger = logger;
            _dotNetMetricsRepository = dotNetMetricsRepository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricsCreateRequest request)
        {
            _logger.LogInformation("Create dotnetmetrics");
            _dotNetMetricsRepository.Create(new Models.DotNetMetrics
            {
                Value = request.Value,
                Time = (int)request.Time.TotalSeconds
            });
            return Ok();
        }


        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsDotNetError([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get all dotnetmetrics.");
            return Ok(_dotNetMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }
    }
}
