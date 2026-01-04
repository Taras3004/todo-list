using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.TodoDb;
using WebApi.Model.Entities.UsersDb;
using WebApi.Model.UsersDb;

var builder = WebApplication.CreateBuilder(args);

var clientUrl = builder.Configuration.GetConnectionString("ClientConnection")!;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactClient", policy =>
    {
        _ = policy
            .WithOrigins(clientUrl)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddJwtTokenServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TodoListDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoDbConnection")));

builder.Services.AddDbContext<UsersDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UsersDbConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 4;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<UsersDbContext>();


builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.MapOpenApi();
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactClient");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
