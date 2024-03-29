using Microsoft.EntityFrameworkCore;
using ROVA_24.Data;
using ROVA_24.IRepository;
using ROVA_24.IService;
using ROVA_24.Repository;
using ROVA_24.Service;

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
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddDbContext<ROVADBContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            options.EnableSensitiveDataLogging();
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
