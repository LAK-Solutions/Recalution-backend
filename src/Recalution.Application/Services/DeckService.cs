using Recalution.Application.DTO.Deck;
using Recalution.Application.Interfaces;
using Recalution.Application.Interfaces.Services;
using Recalution.Domain.Entities;

namespace Recalution.Application.Services;

public class DeckService(IDeckRepository deckRepository) : IDeckService
{
    public async Task<IEnumerable<Deck>> GetByUserIdAsync(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("UserId cannot be empty", nameof(userId));
        }

        return await deckRepository.GetDeckByUserId(userId);
    }

    public async Task<Deck> CreateDeckAsync(string name, Guid userId)
    {
        var deck = new Deck
        {
            Id = Guid.NewGuid(),
            Name = name,
            UserId = userId
        };

        await deckRepository.AddAsync(deck);
        return deck;
    }

    public async Task<Deck> UpdateDeckAsync(Guid deckId, string name, Guid userId)
    {
        if (deckId == Guid.Empty)
            throw new ArgumentException("DeckId cannot be empty", nameof(deckId));

        var deck = await deckRepository.GetByIdAsync(deckId);
        if (deck == null) throw new KeyNotFoundException("Deck not found");

        if (deck.UserId != userId)
            throw new UnauthorizedAccessException("You cannot update the deck");

        deck.Name = name;
        await deckRepository.UpdateAsync(deck);
        return deck;
    }

    public async Task DeleteDeckAsync(Guid deckId, Guid userId)
    {
        var deck = await deckRepository.GetByIdAsync(deckId);
        if (deck == null) throw new KeyNotFoundException("Deck not found");

        if (deck.UserId != userId)
            throw new UnauthorizedAccessException("You cannot delete the deck");

        await deckRepository.DeleteAsync(deck);
    }
}