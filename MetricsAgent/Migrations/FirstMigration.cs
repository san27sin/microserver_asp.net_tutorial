using FluentMigrator;
using FluentMigrator.Infrastructure;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;

namespace MetricsAgent.Migrations
{
    [Migration(1)]//второй уровень мигратора
    public class FirstMigration : Migration
    {
        public override void Down()
        {
            Delete.Table("cpumetrics");
            
        }


        //Когда откатываем, то происходитSystem.InvalidOperationException: 'The context is not set'

        public override void Up()
        {
            //меняем миграцию и выполняется команда
            //задается таблица, колонка, тип данных, идентификационный ключ, идентификатор
            Create.Table("cpumetrics").WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Value").AsInt32().WithColumn("Time").AsInt64();
        }
    }
}
