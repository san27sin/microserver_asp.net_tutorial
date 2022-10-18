namespace MetricsAgent.Models.Request
{
    public class DotNetMetricsCreateRequest
    {
        public int Value { get; set; }
        public TimeSpan Time { get; set; }
    }
}
