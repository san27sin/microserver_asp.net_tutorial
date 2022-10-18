using Quartz;
using Quartz.Spi;

namespace MetricsAgent.Jobs
{
    //будущий сервес нашего проекта
    public class SingletonJobFactory : IJobFactory//пораждение класса job 
    { 
        private readonly IServiceProvider _serviceProvider; 

        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;

        }

        public void ReturnJob(IJob job)
        {
            //он и так job отдаст 
        }
    }
}
