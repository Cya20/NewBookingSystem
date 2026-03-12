using BookingWeb.API.BLL.Interfaces;
using BookingWeb.API.BLL.Services;
using BookingWeb.API.DAL.Data;
using BookingWeb.API.DAL.IRepository;
using BookingWeb.API.DAL.Repository;
using BookingWeb.API.Interface;
using BookingWeb.API.Middleware;
using BookingWeb.API.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("MockDB");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

// Dependency Injection
builder.Services.AddTransient<IBookingRepository, BookingRepository>();
builder.Services.AddTransient<IBookingBusinessLogic, BookingBusinessLogicService>();
builder.Services.AddSingleton<ILoggerService, LoggerService>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ApiRequestFlowMiddleware>();

app.MapControllers();

app.Run();
