using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ApiClients.UsersApiClient;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers;

public class AccountController : Controller
{
    private readonly AuthApiClient authApiClient;

    public AccountController(AuthApiClient authApiClient)
    {
        this.authApiClient = authApiClient;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return this.View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(model);
        }

        var success = await this.authApiClient.RegisterAsync(model);

        if (success)
        {
            this.TempData["SuccessMessage"] = "Реєстрація успішна! Тепер ви можете увійти.";
            return this.RedirectToAction("Login");
        }

        this.ModelState.AddModelError(string.Empty, "Помилка реєстрації. Можливо, користувач з таким email вже існує.");
        return this.View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return this.View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(model);
        }

        var tokenData = await this.authApiClient.LoginAsync(model);

        if (tokenData != null && !string.IsNullOrEmpty(tokenData.Token))
        {
            this.Response.Cookies.Append("jwt", tokenData.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, model.Email),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return this.RedirectToAction("Index", "Home");
        }

        this.ModelState.AddModelError(string.Empty, "Неправильний логін або пароль.");
        return this.View(model);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        this.Response.Cookies.Delete("jwt");

        return this.RedirectToAction("Login");
    }
}
