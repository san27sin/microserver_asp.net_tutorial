using microserver_asp.net_tutorial.Models;

namespace microserver_asp.net_tutorial
{
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /*��� ���������� � ���� ������� - ��� ������������� ���������� ���������.
             * �� ����� ���� ��������� � ������������ ���� �������.
             * ��� ���� ������� �� ������������ - ���������� ���������.
             */

            // Add services to the container.


            //������, ��� �� ������ �������, ���������� � �������� �������� �������, �������� ��� ���������������� ��������������� �������
            //� ������ ������ ���������� �� ����� �������������� ��������� �������
            builder.Services.AddControllers();

            //����� ��������� ����� ����� �������

            builder.Services.AddSingleton<WeatherForecastHolder>();//���������������� ������




            /*����������� ����� ��� 
             * ����� controllers - ����������� ������� ��� �������� ���� ������� ������������
             */


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();//�������� ����� build ���������� ������ web application 

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run(); //�� ����� ��������� ����� run, ����������� web server �� ����������
        }
    }
}