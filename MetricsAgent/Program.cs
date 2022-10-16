using AutoMapper;
using FluentMigrator.Runner;
using MetricsAgent;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Jobs;
using MetricsAgent.Migrations;
using MetricsAgent.Models;
using MetricsAgent.Models.Mapper;
using MetricsAgent.Services.impl;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

var builder = WebApplication.CreateBuilder(args);

#region Configure Options

var serviceProvider = builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
    //��������� ��������� SQLite
    .AddSQLite()
    .WithGlobalConnectionString(builder.Configuration.GetSection("Settings:DatabaseOptions:ConnectionString").Value)
    .ScanIn(typeof(Program).Assembly).For.Migrations())
    .AddLogging(lb => lb
    .AddFluentMigratorConsole());

//typeof(Program).Assembly - ���������� ���������, ���������� ��� ��� � ������ ���� ������

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

builder.Services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
builder.Services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
builder.Services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
builder.Services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
builder.Services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();

//singletone - ����� �����
//scope ����� �� ����� ��������� �������
#endregion
//Sql.ConfigerSqlLiteConnection(); ������ ��� �������� �������� 

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






//�� ������ job �������������� ������
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddSingleton<IJobFactory, SingletonJobFactory>();

builder.Services.AddSingleton<CpuMetricJob>();
builder.Services.AddSingleton(new JobSchedule(
    typeof(CpuMetricJob),
    "0/5 * * ? * * *"));

builder.Services.AddSingleton<RamMetricsJob>();
builder.Services.AddSingleton(new JobSchedule(
    typeof(RamMetricsJob),
    "0/5 * * ? * * *"));


builder.Services.AddHostedService<QuartzHostedService>();//���� ������ ���������� ����

var app = builder.Build();

var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using (IServiceScope serviceScope = serviceScopeFactory.CreateScope())
{
    var migrationRunner = serviceScope.ServiceProvider.GetService<IMigrationRunner>();
    migrationRunner.MigrateUp(1);//����� ��������� �������� down, �� ������ ������� � ��������� ������ ����� ������ ��������    
}

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






