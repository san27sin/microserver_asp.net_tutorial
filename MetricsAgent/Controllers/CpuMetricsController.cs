using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : Controller
    {
        private readonly ILogger<CpuMetricsController> _logger;

        //на уровне контроллера добавляем зависимость
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private readonly IMapper _mapper;

        public CpuMetricsController(ICpuMetricsRepository cpuMetricRepository, ILogger<CpuMetricsController> logger, IMapper mapper)
        {
            _cpuMetricsRepository = cpuMetricRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricsCreateRequest request)
        {
            _logger.LogInformation("Create cpu metrics.");
            _cpuMetricsRepository.Create(_mapper.Map<CpuMetric>(request));             
            return Ok();
        }

        

        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsCpuPersentiles([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] double percentile)
        {
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<CpuMetricsDto>> GetMetricsCpu([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get all cpu metrics fromTime to toTime.");
            return Ok(_mapper.Map<List<CpuMetricsDto>>(_cpuMetricsRepository.GetByTimePeriod(fromTime, toTime)));
        }        

        [HttpGet("all")]
        public ActionResult<IList<CpuMetricsDto>> GetAllMetricsCpu()
        {
            _logger.LogInformation("Get all cpu metrics.");
            return Ok(_mapper.Map<List<CpuMetricsDto>>(_cpuMetricsRepository.GetAll()));
        }


    }
}
