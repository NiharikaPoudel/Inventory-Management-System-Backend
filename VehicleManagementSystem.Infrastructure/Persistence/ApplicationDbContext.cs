using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.Domain.Models;

namespace VehicleManagementSystem.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Part> Parts { get; set; }

        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
    }
}