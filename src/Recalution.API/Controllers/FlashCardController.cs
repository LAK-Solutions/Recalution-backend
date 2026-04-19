using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recalution.Application.Common.Exceptions;
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

    [Authorize]
    [HttpDelete("{deckId:guid}/{flashCardId:guid}")]
    public async Task<IActionResult> DeleteFlashCard(Guid deckId, Guid flashCardId)
    {
        var userId = GetUserId();

        await flashCardService.DeleteAsync(deckId, flashCardId, userId);

        return NoContent();
    }

    private Guid GetUserId()
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            throw new AppException("Invalid user ID.", System.Net.HttpStatusCode.BadRequest);

        return userId;
    }
}
