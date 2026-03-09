using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recalution.Application.Common.Exceptions;
using Recalution.Application.DTO.Deck;
using Recalution.Application.Interfaces.Services;

namespace Recalution.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DecksController(IDeckService deckService) : Controller
{
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetDecksByUserId(Guid userId)
    {
        var decks = await deckService.GetByUserIdAsync(userId);

        return Ok(decks);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult>
        CreateDeck([FromBody] CreateDeckDto dto)
    {
        var userId = GetUserId();

        var deck = await deckService.CreateDeckAsync(dto, userId);

        return Ok(new
        {
            Deck = deck,
            Message = "Deck was created successfully."
        });
    }

    [Authorize]
    [HttpPut("{deckId:guid}")]
    public async Task<IActionResult>
        UpdateDeck(Guid deckId, [FromBody] UpdateDeckDto dto)
    {
        var userId = GetUserId();

        var deck = await deckService.UpdateDeckAsync(deckId, dto, userId);

        return Ok(new
        {
            Deck = deck,
            Message = "Deck was updated successfully."
        });
    }

    [Authorize]
    [HttpDelete("{deckId:guid}")]
    public async Task<IActionResult>
        DeleteDeck(Guid deckId)
    {
        var userId = GetUserId();

        await deckService.DeleteDeckAsync(deckId, userId);

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