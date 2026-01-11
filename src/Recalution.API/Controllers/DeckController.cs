using Microsoft.AspNetCore.Mvc;
using Recalution.Application.DTO;
using Recalution.Application.DTO.Deck;
using Recalution.Application.Interfaces.Services;

namespace Recalution.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DecksController : Controller
{
    private readonly IDeckService _deckService;

    public DecksController(IDeckService deckService)
    {
        this._deckService = deckService;
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetDecksByUserId(Guid userId)
    {
        var decks = await _deckService.GetByUserIdAsync(userId);

        return Ok(decks);
    }

    [HttpPost]
    public async Task<IActionResult>
        CreateDeck([FromBody] CreateDeckDto dto) //TODO: take userId from claims, not from DTO
    {
        var deck = await _deckService.CreateDeckAsync(dto.Name, dto.UserId);

        return Ok(deck);
    }

    [HttpPut("{deckId:guid}")]
    public async Task<IActionResult> UpdateDeck(Guid deckId, [FromBody] UpdateDeckDto dto) //TODO: take userId from claims, not from DTO
    {
        var deck = await _deckService.UpdateDeckAsync(deckId, dto.Name, dto.UserId);
        return Ok(deck);
    }

    [HttpDelete("{deckId:guid}")]
    public async Task<IActionResult>
        DeleteDeck(Guid deckId, [FromBody] DeleteDeckDto dto) //TODO: take userId from claims, not from DTO
    {
        await _deckService.DeleteDeckAsync(deckId, dto.UserId);
        return NoContent();
    }
}