using MetricsAgent.Models.Request;
using MetricsAgent.Services;
using MetricsAgent.Services.impl;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : Controller
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricRepository _iNetworkMetricRepository;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricRepository iNetworkMetricRepository)
        {
            _logger = logger;
            _iNetworkMetricRepository = iNetworkMetricRepository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricsCreateRequest request)
        {
            _logger.LogInformation("Create networkmetrics");
            _iNetworkMetricRepository.Create(new Models.NetworkMetric
            {
                Value = request.Value,
                Time = (int)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsNetwork([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get all networkmetrics.");
            return Ok(_iNetworkMetricRepository.GetByTimePeriod(fromTime, toTime));
        }
    }
}
