using Recalution.Application.DTO.FlashCard;
using Recalution.Application.Interfaces.Repositories;
using Recalution.Application.Interfaces.Services;

namespace Recalution.Application.Services;

public class FlashCardService(IFlashCardRepository flashCardRepository) : IFlashCardService
{
    public async Task<IReadOnlyCollection<FlashCardDetailsDto>> GetByDeckIdAsync(Guid deckId)
    {
        if (deckId == Guid.Empty)
            throw new ArgumentException("DeckId cannot be empty", nameof(deckId));

        var flashCards = await flashCardRepository.GetFlashCardByDeckId(deckId);

        return flashCards.Select(f => new FlashCardDetailsDto
        {
            Id = f.Id,
            Question = f.Question,
            Answer = f.Answer
        }).ToList();
    }
}