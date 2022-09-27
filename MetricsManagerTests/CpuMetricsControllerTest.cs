using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using microserver_asp.net_tutorial.Controllers;//подключаем пространства имен тестироемого проекта
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace MetricsAgentTests
{
    public class CpuMetricsControllerTest
    {
        private CpuMetricsController _cpuMetricsController;

        public CpuMetricsControllerTest()
        {
            _cpuMetricsController = new CpuMetricsController();
        }

        //Надо придумать наименование для тестированного метода
        //В конце слова говорит о том что возвращает корректный результат
        //Каждый метод должен быть помечен атрибутом факт, говорит о том что текущий
        //метод является методом тестирования

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            //Чтобы выполнить метод надо передать 3 параметра

            //1) Подготовка данных для тестирования
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            //2)Исполнить тестируемый метод
            var result = _cpuMetricsController.GetMetricsCpuFromAgent(agentId, fromTime, toTime);

            //3)Assert - статический класс который позволяет выполнять некоторое сравнение 
            //полученного результата и эталонного результата
            //происходит сравнение
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            var result = _cpuMetricsController.GetMetricsCpuFromAll( fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
