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
    public class NetworkMetricControllerTest
    {
        private NetworkMetricsController _networkMetricsController;

        public NetworkMetricControllerTest()
        {
            _networkMetricsController = new NetworkMetricsController();
        }

        [Fact]
        public void GetMetricsNetworkFromAllTest()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            var result = _networkMetricsController.GetMetricsNetworkFromAll(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsNetworkFromAgentTest()
        {
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            var result = _networkMetricsController.GetMetricsNetworkFromAgent(agentId, fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
