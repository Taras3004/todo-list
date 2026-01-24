using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Model.Entities.UsersDb;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        if (registerRequest.Password != registerRequest.ConfirmPassword)
        {
            return this.BadRequest("Password confirmation is not correct.");
        }

        var userExists = await userManager.FindByEmailAsync(registerRequest.Email);
        if (userExists != null)
        {
            return this.BadRequest("User with this email already exists.");
        }

        var user = new ApplicationUser { Email = registerRequest.Email, UserName = registerRequest.Email };

        var result = await userManager.CreateAsync(user, registerRequest.Password);

        if (!result.Succeeded)
        {
            return this.BadRequest(result.Errors);
        }

        return this.Ok("User created successfully!");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await userManager.FindByEmailAsync(loginRequest.Email);

        if (user != null && await userManager.CheckPasswordAsync(user, loginRequest.Password))
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
