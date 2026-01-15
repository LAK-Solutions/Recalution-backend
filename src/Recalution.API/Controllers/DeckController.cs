using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recalution.Application.DTO.Deck;
using Recalution.Application.Interfaces.Services;

namespace Recalution.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DecksController(IDeckService deckService) : Controller
{
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetDecksByUserId(string userId)
    {
        var decks = await deckService.GetByUserIdAsync(userId);

        return Ok(decks);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult>
        CreateDeck([FromBody] CreateDeckDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var deck = await deckService.CreateDeckAsync(dto.Name, userId);

        return Ok(deck);
    }

    [Authorize]
    [HttpPut("{deckId:guid}")]
    public async Task<IActionResult>
        UpdateDeck(Guid deckId, [FromBody] UpdateDeckDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var deck = await deckService.UpdateDeckAsync(deckId, dto.Name, userId);

        return Ok(deck);
    }

    [Authorize]
    [HttpDelete("{deckId:guid}")]
    public async Task<IActionResult>
        DeleteDeck(Guid deckId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        await deckService.DeleteDeckAsync(deckId, userId);

        return NoContent();
    }
}