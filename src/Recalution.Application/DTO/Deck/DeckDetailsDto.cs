using Recalution.Application.DTO.FlashCard;

namespace Recalution.Application.DTO.Deck;

public class DeckDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public List<FlashCardDetailsDto> Cards { get; set; } = new();
}