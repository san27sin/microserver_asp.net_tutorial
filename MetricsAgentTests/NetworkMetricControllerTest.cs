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
    public class NetworkMetricControllerTest
    {
        private NetworkMetricsController _networkMetricsController;

        public NetworkMetricControllerTest()
        {
            _networkMetricsController = new NetworkMetricsController();
        }

        [Fact]
        public void GetMetricsNetworkTest()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _networkMetricsController.GetMetricsNetwork(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
