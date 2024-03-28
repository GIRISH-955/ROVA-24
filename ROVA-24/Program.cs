using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ROVA_24.Data;
using ROVA_24.IRepository;
using ROVA_24.IServices;
using ROVA_24.Mapper;
using ROVA_24.Repository;
using ROVA_24.Services;

namespace ROVA_24
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IAddressServices, AddressServices>();
            builder.Services.AddScoped<IAddressRepository,AddressRepository>();
            builder.Services.AddScoped<IReviewsRepository,ReviewsRepository>();
            builder.Services.AddScoped<IReviewsServices,ReviewsServices>();
            builder.Services.AddAutoMapper(typeof(ApplicationMapper));
            builder.Services.AddDbContext<Rova_23DBContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy",
                    builder => builder
                        .WithOrigins("http://localhost:5243/swagger/index.html")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetIsOriginAllowed((hosts) => true));

            });
            var app = builder.Build();
            app.UseCors("CORSPolicy");
            app.Run("http://localhost:6000");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }

    }

}

