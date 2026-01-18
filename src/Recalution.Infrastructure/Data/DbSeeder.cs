using Microsoft.AspNetCore.Identity;
using Recalution.Domain.Entities;
using Recalution.Infrastructure.Identity;

namespace Recalution.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(
        AppDbContext context,
        UserManager<AppUser> userManager)
    {
        var user = await userManager.FindByNameAsync("test@test.com");
        if (user == null)
            throw new Exception("Test user must be created first!");

        if (context.Decks.Any())
            return;

        var decks = new List<Deck>
        {
            new Deck
            {
                Id = Guid.NewGuid(),
                Name = "German A1",
                OwnerId = user.Id
            },
            new Deck
            {
                Id = Guid.NewGuid(),
                Name = "English Words",
                OwnerId = user.Id
            }
        };

        context.Decks.AddRange(decks);
        await context.SaveChangesAsync();
    }
}