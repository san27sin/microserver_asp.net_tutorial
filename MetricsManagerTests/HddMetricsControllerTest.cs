using microserver_asp.net_tutorial.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void GetMetricsHddFromAllTest()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _hddMetricsController.GetMetricsHddFromAll(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsHddFromAgentTest()
        {
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);


            var result = _hddMetricsController.GetMetricsHddFromAgent(agentId,fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
