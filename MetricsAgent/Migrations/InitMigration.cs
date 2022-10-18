using FluentMigrator;

namespace MetricsAgent.Migrations
{
    //сами реализуем абстрактные методы
    //up - вызывается когда применяем миграцию
    //down - откатываем
    [Migration(0)]//level migration 0 
    public class InitMigration:Migration 
    {
        public override void Down()
        {
        }
        

        public override void Up()
        {

        }
    }
}
