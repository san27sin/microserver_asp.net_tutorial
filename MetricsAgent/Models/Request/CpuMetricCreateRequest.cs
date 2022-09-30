namespace MetricsAgent.Models.Request
{
    public class CpuMetricCreateRequest
    {
        public int Value { get; set; }
        public TimeSpan Time { get; set; }  
    }
}
