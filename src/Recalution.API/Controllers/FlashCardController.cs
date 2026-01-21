using Microsoft.AspNetCore.Mvc;
using Recalution.Application.Interfaces.Services;

namespace Recalution.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlashCardController(IFlashCardService flashCardService) : Controller
{
    [HttpGet("by-deck/{deckId:guid}")]
    public async Task<IActionResult> GetFlashCardsByDeckId(Guid deckId)
    {
        var flashCards = await flashCardService.GetByDeckIdAsync(deckId);
        return Ok(flashCards);
    }
}