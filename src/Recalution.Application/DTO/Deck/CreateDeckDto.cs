using System.ComponentModel.DataAnnotations;

namespace Recalution.Application.DTO.Deck;

public class CreateDeckDto
{
    [Required]
    public string Name { get; set; } = null!;
}