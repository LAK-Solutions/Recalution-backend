using Recalution.Application.DTO.Deck;
using Recalution.Application.DTO.FlashCard;
using Recalution.Application.Interfaces;
using Recalution.Application.Interfaces.Repositories;
using Recalution.Application.Interfaces.Services;
using Recalution.Domain.Entities;

namespace Recalution.Application.Services;

public class DeckService(IDeckRepository deckRepository) : IDeckService
{
    public async Task<IReadOnlyCollection<Deck>> GetByUserIdAsync(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("UserId cannot be empty", nameof(userId));
        }

        return await deckRepository.GetDeckByUserId(userId);
    }

    public async Task<DeckDetailsDto?> CreateDeckAsync(CreateDeckDto dto, Guid userId)
    {
        if (dto.Cards.Count < 2)
            throw new ArgumentException("Deck must contain at least 2 flashcards.", nameof(dto.Cards));

        if (await deckRepository.DeckExistsAsync(dto.Name, userId))
            throw new ArgumentException("Deck with this name already exists.", nameof(dto.Name));

        var deck = new Deck
        {
            Name = dto.Name,
            OwnerId = userId
        };

        foreach (var card in dto.Cards)
        {
            deck.FlashCards.Add(new FlashCard
            {
                Question = card.Question,
                Answer = card.Answer,
            });
        }

        await deckRepository.AddAsync(deck);

        return new DeckDetailsDto
        {
            Id = deck.Id,
            Name = deck.Name,
            Cards = deck.FlashCards.Select(c => new FlashCardDetailsDto
            {
                Id = c.Id,
                Question = c.Question,
                Answer = c.Answer
            }).ToList()
        };
    }

    public async Task<Deck?> UpdateDeckAsync(Guid deckId, string name, Guid userId)
    {
        if (deckId == Guid.Empty)
            throw new ArgumentException("DeckId cannot be empty", nameof(deckId));

        var deck = await deckRepository.GetByIdAsync(deckId);
        if (deck == null) throw new KeyNotFoundException("Deck not found");

        if (deck.OwnerId != userId)
            throw new UnauthorizedAccessException("You cannot update the deck");

        deck.Name = name;
        await deckRepository.UpdateAsync(deck);
        return deck;
    }

    public async Task DeleteDeckAsync(Guid deckId, Guid userId)
    {
        var deck = await deckRepository.GetByIdAsync(deckId);
        if (deck == null) throw new KeyNotFoundException("Deck not found");

        if (deck.OwnerId != userId)
            throw new UnauthorizedAccessException("You cannot delete the deck");

        await deckRepository.DeleteAsync(deck);
    }
}