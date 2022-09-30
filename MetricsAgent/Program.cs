using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.Models;
using MetricsAgent.Services;
using MetricsAgent.Services.impl;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog.Web;



var builder = WebApplication.CreateBuilder(args);

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

//����� ��������� �������� ���� ������ ��� �������, �� � ������ ������� ��� �� ����� ��������� ���� ����������
//������ ����������� ������ ����������� �� ���������� ������ �����������

builder.Services.AddScoped<ICpuMetricsRepository,CpuMetricsRepository>();
//singletone - ����� �����
//scope ����� �� ����� ��������� �������

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







