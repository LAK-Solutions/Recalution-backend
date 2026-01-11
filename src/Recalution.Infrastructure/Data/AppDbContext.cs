using Microsoft.EntityFrameworkCore;
using Recalution.Domain.Entities;

namespace Recalution.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Deck> Decks => Set<Deck>();
}