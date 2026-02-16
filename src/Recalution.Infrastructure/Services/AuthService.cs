using System.Net;
using Microsoft.AspNetCore.Identity;
using Recalution.Application.Common.Exceptions;
using Recalution.Application.DTO.Auth;
using Recalution.Application.DTO.Jwt;
using Recalution.Application.Interfaces.Services;
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
            throw new AppException("User already exists", HttpStatusCode.Conflict);

        var user = new AppUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));

            throw new AppException(errors, HttpStatusCode.BadRequest);
        }
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            throw new AppException("Invalid email or password", HttpStatusCode.Unauthorized);

        var passwordValid = await userManager.CheckPasswordAsync(user, dto.Password);
        if (!passwordValid)
            throw new AppException("Invalid email or password", HttpStatusCode.Unauthorized);

        var roles = await userManager.GetRolesAsync(user);

        var jwtUser = new JwtUserDto(
            user.Id,
            user.Email!,
            roles.ToList()
        );

        return jwtService.GenerateJwt(jwtUser);
    }
}