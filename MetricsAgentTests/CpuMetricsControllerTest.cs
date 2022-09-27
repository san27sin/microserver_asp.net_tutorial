using MetricsAgentTests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MetricsAgent.Controllers;


namespace MetricsAgentTests
{
    public class CpuMetricsControllerTest
    {
        private CpuMetricsController _cpuMetricsController;

        public CpuMetricsControllerTest()
        {
            _cpuMetricsController = new CpuMetricsController();
        }

        [Fact]
        public void GetMetricsCpuPersentilesTest()
        {
            double percentile = 10;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _cpuMetricsController.GetMetricsCpuPersentiles(fromTime, toTime, percentile);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsCpuTest()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _cpuMetricsController.GetMetricsCpu(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
