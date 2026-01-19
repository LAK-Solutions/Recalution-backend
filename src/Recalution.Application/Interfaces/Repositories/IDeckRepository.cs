using Recalution.Domain.Entities;

namespace Recalution.Application.Interfaces.Repositories;

public interface IDeckRepository : IRepository<Deck>
{
    Task<IReadOnlyCollection<Deck>> GetDeckByUserId(Guid userId);
    Task<bool> DeckExistsAsync(string name, Guid ownerId);
}