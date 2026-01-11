namespace Recalution.Domain.Entities;

public class Deck
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    
    public ICollection<FlashCard> FlashCards { get; set; } = new List<FlashCard>();
    
    public Guid UserId { get; set; }
    public User? User { get; set; }
}