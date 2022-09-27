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
    public class HddMetricsControllerTest
    {
        private HddMetricsController _hddMetricsController;

        public HddMetricsControllerTest()
        {
           _hddMetricsController = new HddMetricsController();
        }

        [Fact]
        public void GetMetricsHddTest()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _hddMetricsController.GetMetricsHdd(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
