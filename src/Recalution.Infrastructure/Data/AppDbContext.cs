using Microsoft.EntityFrameworkCore;
using Recalution.Domain.Entities;

namespace Recalution.Infrastructure.Data;

public class AppDbContext : DbContext
{
public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options) { }

public DbSet<User> Users => Set<User>();
}