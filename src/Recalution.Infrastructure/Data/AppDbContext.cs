using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recalution.Domain.Entities;
using Recalution.Infrastructure.Identity;

namespace Recalution.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public DbSet<Deck> Decks => Set<Deck>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Deck>()
            .HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(d => d.OwnerId)
            .IsRequired();
    }
}