using Microsoft.EntityFrameworkCore;
using Recalution.Domain.Entities;
using Recalution.Application.Interfaces.Repositories;
using Recalution.Infrastructure.Data;

namespace Recalution.Infrastructure.Repositories;

public class DeckRepository(AppDbContext context) : Repository<Deck>(context), IDeckRepository
{
    public async Task<IReadOnlyCollection<Deck>> GetDeckByUserId(Guid userId)
    {
        return await _dbSet.Where(d => d.OwnerId == userId).ToListAsync();
    }

    public async Task<bool> DeckExistsAsync(string name, Guid ownerId)
    {
        return await _dbSet.AnyAsync(d => d.OwnerId == ownerId && d.Name == name);
    }
}