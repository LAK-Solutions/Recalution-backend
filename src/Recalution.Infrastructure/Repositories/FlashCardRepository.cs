using Microsoft.EntityFrameworkCore;
using Recalution.Application.Interfaces.Repositories;
using Recalution.Domain.Entities;
using Recalution.Infrastructure.Data;

namespace Recalution.Infrastructure.Repositories;

public class FlashCardRepository(AppDbContext context) : Repository<FlashCard>(context), IFlashCardRepository
{
    public async Task<IReadOnlyCollection<FlashCard>> GetFlashCardByDeckId(Guid deckId)
    {
        return await _dbSet.Where(d => d.DeckId == deckId).ToListAsync();
    }
}