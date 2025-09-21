using BusinessObjects;
using BusinessObjects.Model;
using Microsoft.EntityFrameworkCore;

namespace FPTShopLaptopMVC_API;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        if (builder.Environment.IsEnvironment("Testing"))
        {
            // Use InMemory database for CI/CD tests
            builder.Services.AddDbContext<FptshopDbContext>(options =>
                options.UseInMemoryDatabase("TestLaptopDb"));
        }
        else
        {
            // Use SQL Server for local dev
            builder.Services.AddDbContext<FptshopDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        }
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Allowfrontend", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("Allowfrontend");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}