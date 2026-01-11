namespace Recalution.Application.DTO.Deck;

public class UpdateDeckDto
{
    public string Name { get; set; }
    public Guid UserId { get; set; } //TODO: take userId from claims, remove this
}