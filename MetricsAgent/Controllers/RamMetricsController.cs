using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models.Dto;
using MetricsAgent.Models;
using MetricsAgent.Models.Request;
using MetricsAgent.Services.impl;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : Controller
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _iRamMetricsRepository;
        private readonly IMapper _mapper;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository iRamMetricsRepository, IMapper mapper)
        {
            _logger = logger;
            _iRamMetricsRepository = iRamMetricsRepository;
            _mapper = mapper;   
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricsCreateRequest request)
        {
            _logger.LogInformation("Create rammetrics");
            _iRamMetricsRepository.Create(_mapper.Map<RamMetrics>(request));
            return Ok();
        }


        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsRam([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get all rammetrics.");
            return Ok(_mapper.Map<List<RamMetricsDto>>(_iRamMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }
    }
}
