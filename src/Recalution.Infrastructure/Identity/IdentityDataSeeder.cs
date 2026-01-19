using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Recalution.Infrastructure.Identity;

public static class IdentityDataSeeder
{
    public static readonly HashSet<string> AllowedRoles =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "Admin",
            "User",
            "Manager"
        };
    public static async Task SeedRolesAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        foreach (var role in AllowedRoles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }

    public static async Task SeedAdminAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
        }

        var adminUser = await userManager.FindByNameAsync("admin");
        if (adminUser == null)
        {
            adminUser = new AppUser
            {
                UserName = "admin",
                Email = "admin@example.com",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(adminUser, "Password123!");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    public static async Task SeedTestUserAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<AppUser>>();

        var testUser = await userManager.FindByNameAsync("test@test.com");
        if (testUser == null)
        {
            testUser = new AppUser
            {
                UserName = "test@test.com",
                Email = "test@test.com",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(testUser, "Test123!");
        }
    }
}