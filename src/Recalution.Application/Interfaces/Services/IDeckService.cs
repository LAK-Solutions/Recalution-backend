using Recalution.Application.DTO.Deck;
using Recalution.Domain.Entities;

namespace Recalution.Application.Interfaces.Services;

public interface IDeckService
{
    Task<IReadOnlyCollection<Deck>> GetByUserIdAsync(Guid userId);
    Task<DeckDetailsDto?> CreateDeckAsync(CreateDeckDto dto, Guid userId);
    Task<DeckDetailsDto?> UpdateDeckAsync(Guid deckId, UpdateDeckDto dto, Guid userId);
    Task DeleteDeckAsync(Guid deckId, Guid userId);
}