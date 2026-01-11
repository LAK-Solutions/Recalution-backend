namespace Recalution.Application.DTO.Deck;

public class CreateDeckDto
{
    public string Name { get; set; } = null!;
    public Guid UserId { get; set; } //TODO: take userId from claims, remove this
}