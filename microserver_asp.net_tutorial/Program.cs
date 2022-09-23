using microserver_asp.net_tutorial.Models;

namespace microserver_asp.net_tutorial
{
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /*ѕри добавление в майн классов - они автоматически станов€тс€ сервесами.
             * ћы можем сюда добавл€ть в конфигурацию свои сервесы.
             * ¬се типы которые мы регистрируем - называютс€ сервесами.
             */

            // Add services to the container.


            //ѕервое, что мы должны выучить, обращаемс€ к базовому свойству сервеса, помогает нам зарегистрировать вспомагательные сервисы
            //в рамках нашего приложени€ мы можем регистрировать различные сервесы
            builder.Services.AddControllers();

            //ћожем указывать врем€ жизни сервера

            builder.Services.AddSingleton<WeatherForecastHolder>();//«арегистрировали сервис




            /* онтроллеры нужны дл€ 
             * ѕапка controllers - эстетически создана дл€ хранени€ всех классов контроллеров
             */


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();//конечный метод build возвращает объект web application 

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run(); //по итогу запускаем метод run, запускаетс€ web server на выполнение
        }
    }
}