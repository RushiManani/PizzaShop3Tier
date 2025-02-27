using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PizzaShop.BLL.Helpers;

public class JWTExpirationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<JWTExpirationMiddleware> _logger;

    public JWTExpirationMiddleware(RequestDelegate next, ILogger<JWTExpirationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Cookies.TryGetValue("JWT", out string token))
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var expiration = jwtToken.ValidTo;

            if (expiration < DateTime.UtcNow)
            {
                _logger.LogInformation("JWT token expired. Redirecting to login page.");
                context.Response.Cookies.Delete("JWT");
                context.Response.Redirect("/Auth/User_Login");
                return;
            }
        }

        await _next(context);
    }
}
