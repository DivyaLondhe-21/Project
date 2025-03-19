using Microsoft.EntityFrameworkCore;
using ReservationServices.Interface;
using ReservationServices.Data;
using ReservationServices.Repositories;

namespace ReservationServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IReservation, ReservationRepository>();


            var constring = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<HotelManagementSystemContext>
                (options => options.UseSqlServer(constring));


            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            
            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
