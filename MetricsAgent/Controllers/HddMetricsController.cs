using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models.Dto;
using MetricsAgent.Models;
using MetricsAgent.Models.Request;
using MetricsAgent.Services.impl;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : Controller
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _hddMetricsRepository;
        private readonly IMapper _mapper;

        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository hddMetricsRepository, IMapper mapper)
        {
            _logger = logger;
            _hddMetricsRepository = hddMetricsRepository;
            _mapper = mapper;
        }

       

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsHdd([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get all hddmetrics.");
            return Ok(_mapper.Map<List<HddMetricsDto>>(_hddMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }


        [HttpGet("all")]
        public ActionResult<IList<HddMetricsDto>> GetAllHddMetrics()
        {
            _logger.LogInformation("Get all hddmetrics.");
            return Ok(_mapper.Map<List<HddMetricsDto>>(_hddMetricsRepository.GetAll()));
        }
    }
}
