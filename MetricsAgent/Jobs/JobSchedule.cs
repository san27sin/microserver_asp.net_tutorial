namespace MetricsAgent.Jobs
{
    public class JobSchedule
    {
        //класс представляет некоторую настройку
        public Type JobType { get; }
        public string CronExpression { get; }
        public JobSchedule(Type jobType,string cronExpression)
        {
            JobType = jobType;
            CronExpression = cronExpression;
        }
    }
}
