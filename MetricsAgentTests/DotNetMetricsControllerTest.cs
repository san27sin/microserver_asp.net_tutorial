using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTests
{
    public class DotNetMetricsControllerTest
    {
        private DotNetMetricsController _dotNetMetricsController;

        public DotNetMetricsControllerTest()
        {
            _dotNetMetricsController = new DotNetMetricsController();
        }

        [Fact]
        public void GetMetricsDotNetErrorTest()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _dotNetMetricsController.GetMetricsDotNetError(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
