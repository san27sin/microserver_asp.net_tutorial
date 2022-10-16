using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models.Dto;
using MetricsAgent.Models;
using MetricsAgent.Models.Request;
using MetricsAgent.Services.impl;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : Controller
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _networkMetricRepository;
        private readonly IMapper _mapper;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository iNetworkMetricRepository, IMapper mapper)
        {
            _logger = logger;
            _networkMetricRepository = iNetworkMetricRepository;
            _mapper = mapper;
        }

        
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsNetwork([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get all networkmetrics.");            
            return Ok(_mapper.Map<List<NetworkMetricsDto>>(_networkMetricRepository.GetByTimePeriod(fromTime, toTime)));
        }
    }
}
