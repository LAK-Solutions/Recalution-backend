using Recalution.Application.DTO.Deck;
using Recalution.Domain.Entities;

namespace Recalution.Application.Interfaces.Services;

public interface IDeckService
{
    Task<IReadOnlyCollection<Deck>> GetByUserIdAsync(Guid userId);
    Task<DeckDetailsDto?> CreateDeckAsync(CreateDeckDto dto, Guid userId);
    Task<Deck?> UpdateDeckAsync(Guid deckId, string name, Guid userId);
    Task DeleteDeckAsync(Guid deckId, Guid userId);
}