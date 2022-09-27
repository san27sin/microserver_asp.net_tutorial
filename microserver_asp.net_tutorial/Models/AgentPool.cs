namespace microserver_asp.net_tutorial.Models
{
    public class AgentPool
    {
        //Пример создания singleton ручным способом, нужно для тестирования
        private static AgentPool _instance;
        public static AgentPool Instance 
        {
            get
            {
                if (_instance == null)
                    _instance = new AgentPool();
                return _instance;
            }        
        }
        ///Только на случай тестирования 


        private Dictionary<int, AgentInfo> _values;//ключ идентификатор агента

        public AgentPool()
        {
            _values = new Dictionary<int, AgentInfo>();
        }

        public void Add(AgentInfo value)
        {
            if(!_values.ContainsKey(value.AgentId))
            {
                _values.Add(value.AgentId, value);//добавим нового агента
            }
        }

        /// <summary>
        /// Получить список агентов
        /// </summary>
        /// <returns>список агентов</returns>
        public AgentInfo[] Get()
        {
            return _values.Values.ToArray();
        }

        /// <summary>
        /// Напрямую обращение к словарю
        /// </summary>
        public Dictionary<int, AgentInfo> Values
        {
            get { return _values; }
            set { _values = value; }    
        }
    }
}
