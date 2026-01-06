using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recalution.Domain.Entities;
using Recalution.Infrastructure.Data;

namespace Recalution.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public UsersController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _dbContext.Users.ToListAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return Ok(user);
    }
}