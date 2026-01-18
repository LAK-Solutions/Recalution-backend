using Microsoft.EntityFrameworkCore;
using Recalution.Domain.Entities;
using Recalution.Application.Interfaces;
using Recalution.Infrastructure.Data;

namespace Recalution.Infrastructure.Repositories;

public class DeckRepository(AppDbContext context) : Repository<Deck>(context), IDeckRepository
{
    public async Task<IReadOnlyCollection<Deck>> GetDeckByUserId(Guid userId)
    {
        return await _dbSet.Where(d => d.OwnerId == userId).ToListAsync();
    }
}