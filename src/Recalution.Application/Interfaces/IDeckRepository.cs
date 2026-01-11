using Recalution.Domain.Entities;
using Recalution.Application.Interfaces;

namespace Recalution.Application.Interfaces;

public interface IDeckRepository : IRepository<Deck>
{
    Task<IEnumerable<Deck>> GetDeckByUserId(Guid userId);
}   