using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Autolaya.Domain.Models;

namespace Autolaya.Infrastructure.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<ServiceHistory> ServiceHistories => Set<ServiceHistory>();
    public DbSet<PartRequest> PartRequests => Set<PartRequest>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Customer>()
            .HasIndex(c => c.Email).IsUnique();
        builder.Entity<Review>()
            .Property(r => r.StarRating).HasPrecision(3, 1);
        builder.Entity<ServiceHistory>()
            .Property(s => s.TotalCost).HasPrecision(10, 2);
    }
}