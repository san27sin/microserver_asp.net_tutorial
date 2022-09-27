using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace microserver_asp.net_tutorial
{
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<Models.AgentPool>();//Регистрируем сервес в рамках шаблона singleton 
            //Подключенная функция обеспечивает потокобезопасность 
            //Объект будет создан тольки один раз



            /*При добавление в майн классов - они автоматически становятся сервесами.
             * Мы можем сюда добавлять в конфигурацию свои сервесы.
             * Все типы которые мы регистрируем - называются сервесами.
             */

            // Add services to the container.


            //Первое, что мы должны выучить, обращаемся к базовому свойству сервеса, помогает нам зарегистрировать вспомагательные сервисы
            //в рамках нашего приложения мы можем регистрировать различные сервесы

            //Можем указывать время жизни сервера

            /*Контроллеры нужны для 
             * Папка controllers - эстетически создана для хранения всех классов контроллеров
             */


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();


            //Поговорим об этом явление на следующей недели
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });

                //Поддержка TimeSpan
                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
            });


            var app = builder.Build();            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            
            app.Run();
        }
    }
}