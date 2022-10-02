using Microsoft.AspNetCore.Mvc;
using microserver_asp.net_tutorial.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void GetMetricsErrorFromAgentTest()
        {
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _dotNetMetricsController.GetMetricsErrorFromAgent(agentId, fromTime, toTime);  
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsErrorFromAllTest()
        {            
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _dotNetMetricsController.GetMetricsErrorFromAll(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
