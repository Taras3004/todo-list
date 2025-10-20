using Microsoft.AspNetCore.Authentication.Cookies;
using WebApp.Models.ApiClients.TodoListApiClient;
using WebApp.Models.ApiClients.UsersApiClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<JwtAuthHandler>();

builder.Services.AddHttpClient<TodoApiClientContext>(client =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("TodoApiLocalConnection")!);
    })
    .AddHttpMessageHandler<JwtAuthHandler>();


builder.Services.AddHttpClient<AuthApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("TodoApiLocalConnection")!);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}")
    .WithStaticAssets();


await app.RunAsync();

