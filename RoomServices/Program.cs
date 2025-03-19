using RoomServices.Data;
using RoomServices.Interface;
using RoomServices.Models;
using Microsoft.EntityFrameworkCore;
using RoomServices.Repositories;

namespace RoomServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
             var constring = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<HotelManagementSystemContext>
                (options => options.UseSqlServer(constring));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IRoomService, RoomServicesRepository>();

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
