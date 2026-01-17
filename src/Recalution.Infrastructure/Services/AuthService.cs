using Microsoft.AspNetCore.Identity;
using Recalution.Application.DTO.Auth;
using Recalution.Application.DTO.Jwt;
using Recalution.Application.Interfaces;
using Recalution.Infrastructure.Identity;

namespace Recalution.Infrastructure.Services;

public class AuthService(
    UserManager<AppUser> userManager,
    IJwtService jwtService
) : IAuthService
{
    public async Task RegisterAsync(RegisterDto dto)
    {
        if (await userManager.FindByEmailAsync(dto.Email) != null)
            throw new InvalidOperationException("User already exists");

        var user = new AppUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            throw new InvalidOperationException(
                string.Join(", ", result.Errors.Select(e => e.Description))
            );
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password");

        var passwordValid = await userManager.CheckPasswordAsync(user, dto.Password);
        if (!passwordValid)
            throw new UnauthorizedAccessException("Invalid email or password");

        var roles = await userManager.GetRolesAsync(user);

        var jwtUser = new JwtUserDto(
            user.Id,
            user.Email!,
            roles.ToList()
        );

        return jwtService.GenerateJwt(jwtUser);
    }
}