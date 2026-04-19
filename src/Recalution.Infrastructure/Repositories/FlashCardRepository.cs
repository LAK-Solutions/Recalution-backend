using Microsoft.EntityFrameworkCore;
using Recalution.Application.Interfaces.Repositories;
using Recalution.Domain.Entities;
using Recalution.Infrastructure.Data;

namespace Recalution.Infrastructure.Repositories;

public class FlashCardRepository(AppDbContext context) : Repository<FlashCard>(context), IFlashCardRepository
{
    public async Task<IReadOnlyCollection<FlashCard>> GetFlashCardsByDeckIdAsync(Guid deckId)
    {
        return await _dbSet.Where(d => d.DeckId == deckId).ToListAsync();
    }

    public async Task<FlashCard?> GetByIdAndDeckIdAsync(Guid flashCardId, Guid deckId)
    {
        return await _dbSet.FirstOrDefaultAsync(f => f.Id == flashCardId && f.DeckId == deckId);
    }

    public async Task<int> CountByDeckIdAsync(Guid deckId)
    {
        return await _dbSet.CountAsync(f => f.DeckId == deckId);
    }
}
