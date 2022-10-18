using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog;

namespace microserver_asp.net_tutorial
{
    
    public class Program
    {
        //����������� - ��� ������ �������������� ������� �������� ���������������� �� ����������� � ��������� ���������� ������, ��� ������������� �����
        //����� ���������� ������ � ���������� ������ �������, ����������� ��� ������ ���� ����������
        //��� ���� ������ ������ �������� �� ����������
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                      

            builder.Services.AddSingleton<Models.AgentPool>();//������������ ������ � ������ ������� singleton 
            //������������ ������� ������������ ������������������ 
            //������ ����� ������ ������ ���� ���



            /*��� ���������� � ���� ������� - ��� ������������� ���������� ���������.
             * �� ����� ���� ��������� � ������������ ���� �������.
             * ��� ���� ������� �� ������������ - ���������� ���������.
             */

            // Add services to the container.


            //������, ��� �� ������ �������, ���������� � �������� �������� �������, �������� ��� ���������������� ��������������� �������
            //� ������ ������ ���������� �� ����� �������������� ��������� �������

            //����� ��������� ����� ����� �������

            /*����������� ����� ��� 
             * ����� controllers - ����������� ������� ��� �������� ���� ������� ������������
             */


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();


            //��������� �� ���� ������� �� ��������� ������
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsManager", Version = "v1" });

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


            app.MapControllers();
            
            app.Run();
        }
    }
}