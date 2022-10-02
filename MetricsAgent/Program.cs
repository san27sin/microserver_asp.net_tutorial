using AutoMapper;
using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Models;
using MetricsAgent.Models.Mapper;
using MetricsAgent.Services.impl;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog.Web;



var builder = WebApplication.CreateBuilder(args);

#region Configure Options


//ѕрописываем значение переменной из config file дл€ нашей переменной
builder.Services.Configure<DatabaseOptions>(options =>
{
    builder.Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
});


#endregion

#region Configure Mapping

var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
var mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion



#region Configure logging

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
}).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.RequestHeaders.Add("Authorization");
    logging.RequestHeaders.Add("X-Real-IP");
    logging.RequestHeaders.Add("X-Forwarded-For");
});

#endregion



// Add services to the container.
#region Configure Scoped
//можем добавл€ть нетолько сами модули как сервесы, но в рамках сервеса так же может выступать наша абстракци€
//делаем зависимость нашего контроллера от абстракции нашего контроллера

builder.Services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
builder.Services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
builder.Services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
builder.Services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
builder.Services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();

//singletone - живет вечно
//scope живет по времи обработки запроса
#endregion
Sql.ConfigerSqlLiteConnection();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//ѕоговорим об этом €вление на следующей недели
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });

    //ѕоддержка TimeSpan
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

app.UseHttpLogging();//добавл€ем дл€ использовани€ логировани€

app.MapControllers();

app.Run();







