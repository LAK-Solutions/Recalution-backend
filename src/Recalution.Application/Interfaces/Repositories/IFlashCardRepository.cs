using Recalution.Application.DTO.Deck;
using Recalution.Domain.Entities;

namespace Recalution.Application.Interfaces.Repositories;

public interface IFlashCardRepository : IRepository<FlashCard>
{
    Task<IReadOnlyCollection<FlashCard>> GetFlashCardsByDeckIdAsync(Guid deckId);
    Task<FlashCard?> GetByIdAndDeckIdAsync(Guid flashCardId, Guid deckId);
    Task<int> CountByDeckIdAsync(Guid deckId);
}
