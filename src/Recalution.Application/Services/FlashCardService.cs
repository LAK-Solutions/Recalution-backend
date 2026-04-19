using System.Net;
using Recalution.Application.Common.Exceptions;
using Recalution.Application.DTO.FlashCard;
using Recalution.Application.Interfaces.Repositories;
using Recalution.Application.Interfaces.Services;

namespace Recalution.Application.Services;

public class FlashCardService(
    IFlashCardRepository flashCardRepository,
    IDeckRepository deckRepository) : IFlashCardService
{
    public async Task<IReadOnlyCollection<FlashCardDetailsDto>> GetByDeckIdAsync(Guid deckId)
    {
        if (deckId == Guid.Empty)
            throw new ArgumentException("DeckId cannot be empty", nameof(deckId));

        var flashCards = await flashCardRepository.GetFlashCardsByDeckIdAsync(deckId);

        return flashCards.Select(f => new FlashCardDetailsDto
        {
            Id = f.Id,
            Question = f.Question,
            Answer = f.Answer
        }).ToList();
    }

    public async Task DeleteAsync(Guid deckId, Guid flashCardId, Guid userId)
    {
        if (deckId == Guid.Empty)
            throw new ArgumentException("DeckId cannot be empty", nameof(deckId));

        if (flashCardId == Guid.Empty)
            throw new ArgumentException("FlashCardId cannot be empty", nameof(flashCardId));

        if (userId == Guid.Empty)
            throw new ArgumentException("UserId cannot be empty", nameof(userId));

        var deck = await deckRepository.GetDeckByIdAsync(deckId);
        if (deck == null)
            throw new AppException("Deck not found", HttpStatusCode.NotFound);

        if (deck.OwnerId != userId)
            throw new AppException("You cannot delete flashcards from this deck", HttpStatusCode.Forbidden);

        var flashCard = await flashCardRepository.GetByIdAndDeckIdAsync(flashCardId, deckId);
        if (flashCard == null)
            throw new AppException("Flashcard not found", HttpStatusCode.NotFound);

        var flashCardCount = await flashCardRepository.CountByDeckIdAsync(deckId);
        if (flashCardCount <= 2)
            throw new AppException("Deck must contain at least 2 flashcards.", HttpStatusCode.BadRequest);

        await flashCardRepository.DeleteAsync(flashCard);
    }
}
