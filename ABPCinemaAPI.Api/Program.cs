using ABPCinemaAPI.Api.Middleware;
using ABPCinemaAPI.BLL.Services;
using ABPCinemaAPI.BLL.Services.Contracts;
using ABPCinemaAPI.DAL.Persistence;
using ABPCinemaAPI.DAL.Repositories;
using ABPCinemaAPI.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ABPCinemaAPI.DAL")));

// Persistence
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddSingleton<Serilog.ILogger>(sp => Log.Logger);

// MediatR
var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(currentAssemblies));

//AddingServices
builder.Services.AddScoped<ILoggerService, LoggerService>();


builder.Services.AddEndpointsApiExplorer();

//Adding CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "http://localhost:3000", "http://localhost:8080")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

//Adding Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Seed the database
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDbContext>();

            context.SeedData();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
        }
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("AllowOrigins");

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
