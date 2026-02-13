namespace Recalution.Domain.Entities;

public class FlashCard
{
    public Guid Id { get; set; }
    public required string Question { get; set; }
    public required string Answer { get; set; }

    public Guid DeckId { get; set; }
    public Deck Deck { get; set; } = null!;
}