using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Model.Entities.UsersDb;

namespace WebApi.Controllers;

public record RegisterDto(string Email, string Password, string ConfirmPassword);

public record LoginDto(string Email, string Password);

[Route("api/[controller]")]
[ApiController]
public class AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (registerDto.Password != registerDto.ConfirmPassword)
        {
            return this.BadRequest("Password confirmation is not correct.");
        }

        var userExists = await userManager.FindByEmailAsync(registerDto.Email);
        if (userExists != null)
        {
            return this.BadRequest("User with this email already exists.");
        }

        var user = new ApplicationUser { Email = registerDto.Email, UserName = registerDto.Email };

        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return this.BadRequest(result.Errors);
        }

        return this.Ok("User created successfully!");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);

        if (user != null && await userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            var token = this.GenerateJwtToken(user);
            return this.Ok(new { token });
        }

        return this.Unauthorized("Invalid credentials.");
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var jwtKey = configuration["JsonWebTokenKeys:IssuerSigningKey"];
        var jwtIssuer = configuration["JsonWebTokenKeys:ValidIssuer"];
        var jwtAudience = configuration["JsonWebTokenKeys:ValidAudience"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
