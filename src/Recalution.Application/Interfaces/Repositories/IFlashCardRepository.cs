using Recalution.Application.DTO.Deck;
using Recalution.Domain.Entities;

namespace Recalution.Application.Interfaces.Repositories;

public interface IFlashCardRepository : IRepository<FlashCard>
{
    Task<IReadOnlyCollection<FlashCard>> GetFlashCardByDeckId(Guid deckId);
}