using Recalution.Application.DTO.FlashCard;

namespace Recalution.Application.DTO.Deck;

public class UpdateDeckDto
{
    public string Name { get; set; } = null!;

    public List<FlashCardDetailsDto> Cards { get; set; } = new();
}