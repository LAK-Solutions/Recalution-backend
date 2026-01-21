namespace Recalution.Application.DTO.FlashCard;

public class FlashCardDetailsDto
{
    public Guid Id { get; set; }
    public string Question { get; set; } = null!;
    public string Answer { get; set; } = null!;
}