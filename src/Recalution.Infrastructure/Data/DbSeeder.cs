using Recalution.Domain.Entities;

namespace Recalution.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.Users.Any())
            return;

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test User",
            Email = "test@test.com"
        };

        var decks = new List<Deck>
        {
            new Deck
            {
                Id = Guid.NewGuid(),
                Name = "German A1",
                User = user
            },
            new Deck
            {
                Id = Guid.NewGuid(),
                Name = "English Words",
                User = user
            }
        };

        context.Users.Add(user);
        context.Decks.AddRange(decks);

        await context.SaveChangesAsync();
    }
}