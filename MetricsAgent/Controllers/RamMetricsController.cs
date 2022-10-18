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
        private readonly IRamMetricsRepository _ramMetricsRepository;
        private readonly IMapper _mapper;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository iRamMetricsRepository, IMapper mapper)
        {
            _logger = logger;
            _ramMetricsRepository = iRamMetricsRepository;
            _mapper = mapper;   
        }



        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsRam([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get all rammetrics.");
            return Ok(_mapper.Map<List<RamMetricsDto>>(_ramMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }


        [HttpGet("all")]
        public ActionResult<IList<RamMetricsDto>> GetAllRamMetrics()
        {
            _logger.LogInformation("Get all hddmetrics.");
            return Ok(_mapper.Map<List<RamMetricsDto>>(_ramMetricsRepository.GetAll()));
        }
    }
}
