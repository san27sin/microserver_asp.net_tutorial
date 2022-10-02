using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using microserver_asp.net_tutorial;
using microserver_asp.net_tutorial.Models;
using microserver_asp.net_tutorial.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerTests
{
    //В конце названия принято вставлять вставлять *Tests
    //модификатор доступа public

    public class AgentsControllerTests
    {

        private AgentsController _agentsController;
        private AgentPool _agentPool;

        AgentsControllerTests()
        {
            _agentPool = AgentPool.Instance;
            _agentsController = new AgentsController(_agentPool);
        }

        [Theory]//этот атрибут тестирования, принимает на вход параметр идентификатора объекта, так же благодаря [Theory] мы можем добавить атрибут [InlineData()] 
        [InlineData(5)]//данная конструкция позволяет выполнить метод тестирования регистра agent test сколько атрибутов мы указали
        [InlineData(15)]//конструкция позваляет вставить параметр на вход
        [InlineData(25)]
        public void RegisterAgentTest(int agentId)
        {
            AgentInfo agentInfo = new AgentInfo() { AgentId = agentId, Enable = true };
            var actionResult = _agentsController.RegisterAgent(agentInfo);//получаем результат тестирования
            Assert.IsAssignableFrom<IActionResult>(actionResult);
        }

        [Theory,InlineData(0)]
        public void EnableAgentByIdTest(int agentId)
        {
            var actionResult = _agentsController.EnableAgentById(agentId);
            Assert.IsAssignableFrom<IActionResult>(actionResult);
        }

        [Theory, InlineData(5)]
        public void DisableAgentByIdTest( int agentId)
        {
            var actionResult = _agentsController.DisableAgentById(agentId);
            Assert.IsAssignableFrom<IActionResult>(actionResult);
        }

        [Fact]
        public void GetAllAgentsTest()
        {
            var actionResult = _agentsController.GetAllAgents();
            Assert.IsAssignableFrom<IActionResult>(actionResult);
        }
    }
}
