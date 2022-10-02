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


//����������� �������� ���������� �� config file ��� ����� ����������
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
//����� ��������� �������� ���� ������ ��� �������, �� � ������ ������� ��� �� ����� ��������� ���� ����������
//������ ����������� ������ ����������� �� ���������� ������ �����������

builder.Services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
builder.Services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
builder.Services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
builder.Services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
builder.Services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();

//singletone - ����� �����
//scope ����� �� ����� ��������� �������
#endregion
Sql.ConfigerSqlLiteConnection();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//��������� �� ���� ������� �� ��������� ������
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });

    //��������� TimeSpan
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

app.UseHttpLogging();//��������� ��� ������������� �����������

app.MapControllers();

app.Run();







