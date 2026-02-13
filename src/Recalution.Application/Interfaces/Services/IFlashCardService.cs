using Recalution.Application.DTO.FlashCard;

namespace Recalution.Application.Interfaces.Services;

public interface IFlashCardService
{
    Task<IReadOnlyCollection<FlashCardDetailsDto>> GetByDeckIdAsync(Guid deckId);
}