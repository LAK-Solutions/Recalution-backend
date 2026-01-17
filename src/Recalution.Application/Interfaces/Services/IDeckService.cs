using Recalution.Application.DTO.Deck;
using Recalution.Domain.Entities;

namespace Recalution.Application.Interfaces.Services;

public interface IDeckService
{
    Task<IReadOnlyCollection<Deck>> GetByUserIdAsync(string userId);
    Task<Deck?> CreateDeckAsync(string name, string userId);
    Task<Deck?> UpdateDeckAsync(Guid deckId, string name, string userId);
    Task DeleteDeckAsync(Guid deckId, string userId);
}