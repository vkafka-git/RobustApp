using Serilog;
using Microsoft.EntityFrameworkCore;
using Robust.Entities.Models;
using Robust.WebApi.Extensions;
using Robust.WebApi.ExceptionHandling;
using Robust.LoggerService;
using ILogger = Robust.LoggerService.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureCors();
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));
//builder.Services.AddControllers(options =>
//{
//    //options.Filters.Add<UnhandledExceptionFilterAttribute>();
//    options.Filters.Add<MyExceptionAttribute>();
//});

builder.Services.AddTransient<ILogger, Logger>();
//var logger = new LoggerConfiguration()
//        .ReadFrom.Configuration(builder.Configuration)
//        .Enrich.FromLogContext()
//        .CreateLogger();

builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(logger);

var app = builder.Build();

app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
// Configure the HTTP request pipeline.
//app.UseSerilogRequestLogging(); // <-- Add this line
app.UseAuthorization();

app.MapControllers();

app.Run();
