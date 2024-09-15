
using InventoryManagementSystem.API.Extensions;
using InventoryManagementSystem.API.Middlewares;
using InventoryManagementSystem.Core.Mappers;
using InventoryManagementSystem.Infrastructure.Configuration;
using InventoryManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddCustomRepository();
builder.Services.AddCustomServices();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.AddCustomMiddleware();

app.MapControllers();

app.Run();
