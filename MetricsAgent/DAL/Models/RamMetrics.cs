﻿namespace MetricsAgent.Models
{
    //в дальнейшем изменится
    public class RamMetrics
    {
        public int Id { get; set; } //уникальный идентификатор метрики
        public int Value { get; set; }//значение
        public long Time { get; set; }//кол-во секунд от определенной даты
    }
}
