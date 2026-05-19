using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using VehicleManagementSystem.Domain.Models;

namespace VehicleManagementSystem.Infrastructure.Seed
{
    public static class IdentitySeeder
    {
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminRole = "Admin";
            string staffRole = "Staff";
            string customerRole = "Customer";

            if (!await roleManager.RoleExistsAsync(adminRole))
                await roleManager.CreateAsync(new IdentityRole(adminRole));

            if (!await roleManager.RoleExistsAsync(staffRole))
                await roleManager.CreateAsync(new IdentityRole(staffRole));

            if (!await roleManager.RoleExistsAsync(customerRole))
                await roleManager.CreateAsync(new IdentityRole(customerRole));

            string adminEmail = "admin@vehicle.com";
            string adminPassword = "Admin@123";

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin == null)
            {
                var adminUser = new ApplicationUser
                {
                    FullName = "System Admin",
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }
        }
    }
}