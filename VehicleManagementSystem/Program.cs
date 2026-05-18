using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.Application.Interfaces.IRepositories;
using VehicleManagementSystem.Application.Interfaces.IServices;
using VehicleManagementSystem.Infrastructure.Persistence;
using VehicleManagementSystem.Infrastructure.Repositories;
using VehicleManagementSystem.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISaleService, SaleService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

app.Run();