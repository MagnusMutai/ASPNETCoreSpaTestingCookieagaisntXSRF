using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//Add the reverse proxy capability to the server
builder.Services.AddReverseProxy();
    //Initialize the reverse proxy from the "ReverseProxy" section of configuration
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapReverseProxy();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => Guid.NewGuid().ToString());

app.MapGet("/protected", (HttpContext ctx) => ctx.User.FindFirst(ClaimTypes.Name)?.Value).RequireAuthorization();

app.MapGet("/data", () => "Welp we edited something...").RequireAuthorization();

app.MapPost("/login", (LoginForm form, HttpContext ctx) =>
{
    ctx.SignInAsync(new ClaimsPrincipal(new[]
    {
        new ClaimsIdentity(new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, form.Username),
        },
        CookieAuthenticationDefaults.AuthenticationScheme)
    }));

    return "ok";
});

app.MapDefaultControllerRoute();

app.MapControllers();

app.Run();

public class LoginForm
{
    public string? Username { get; set; }
    public string? Name { get; set; }

}