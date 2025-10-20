using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities.UsersDb;

namespace WebApi.Controllers;

public record RegisterDto(string Email, string Password);

public record LoginDto(string Email, string Password);

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IConfiguration configuration;

    public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var userExists = await this.userManager.FindByEmailAsync(registerDto.Email);
        if (userExists != null)
        {
            return this.BadRequest("User with this email already exists.");
        }

        var user = new ApplicationUser { Email = registerDto.Email, UserName = registerDto.Email };

        var result = await this.userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return this.BadRequest(result.Errors);
        }

        return this.Ok("User created successfully!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await this.userManager.FindByEmailAsync(loginDto.Email);

        if (user != null && await this.userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            var token = this.GenerateJwtToken(user);
            return this.Ok(new { token });
        }

        return this.Unauthorized("Invalid credentials.");
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var jwtKey = this.configuration["JsonWebTokenKeys:IssuerSigningKey"];
        var jwtIssuer = this.configuration["JsonWebTokenKeys:ValidIssuer"];
        var jwtAudience = this.configuration["JsonWebTokenKeys:ValidAudience"];


        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!), new Claim(ClaimTypes.NameIdentifier, user.Id),
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
