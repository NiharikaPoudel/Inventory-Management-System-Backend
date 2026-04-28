using Microsoft.EntityFrameworkCore;
using Autolaya.Application.Interfaces.IRepositories;
using Autolaya.Application.Interfaces.IServices;
using Autolaya.Infrastructure.Data;
using Autolaya.Infrastructure.Repositories;
using Autolaya.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// TEMPORARY in-memory database — replace with PostgreSQL tomorrow
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("AutolayaTemp"));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBuyerService, BuyerService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp", policy =>
        policy.WithOrigins(
            "https://localhost:54660",
            "https://localhost:7129",
            "http://localhost:5173"
        )
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("ReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run();