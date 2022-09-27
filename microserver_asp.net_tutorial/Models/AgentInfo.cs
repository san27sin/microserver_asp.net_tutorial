namespace microserver_asp.net_tutorial.Models
{
    public class AgentInfo
    {
        public int AgentId { get; set; }//идентификатор самого агента
        public Uri? AgentAddress { get; set; }//url адрес

        public bool Enable { get; set; }//вкл/выкл агента
    }
}
