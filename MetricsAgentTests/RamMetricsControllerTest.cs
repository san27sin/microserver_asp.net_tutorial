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
    public class RamMetricsControllerTest
    {
        private RamMetricsController _ramMetricsController;

        public RamMetricsControllerTest()
        {
            _ramMetricsController = new RamMetricsController();
        }

        [Fact]
        public void GetMetricsRamTest()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _ramMetricsController.GetMetricsRam(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
