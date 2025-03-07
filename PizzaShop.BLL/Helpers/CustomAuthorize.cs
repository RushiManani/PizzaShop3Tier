using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PizzaShop.BLL.Interfaces;

namespace PizzaShop.BLL.Helpers;

public class CustomAuthorize : Attribute, IAuthorizationFilter
{
    private readonly string[] _roles;
    public CustomAuthorize(string[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Cookies["JWT"];
        if(token==null)
        {
            context.Result = new RedirectToRouteResult(new {controller="Auth",action="User_Login"});
            return;
        }
        var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
        var handler = new JwtSecurityTokenHandler();
        var JWT = configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(JWT["Key"]);
        try
        {
            var ValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = JWT["Issuer"],
                ValidateAudience = true,
                ValidAudience = JWT["Audience"],
                ValidateLifetime = true
            };
            var ClaimsPrincipal = handler.ValidateToken(token, ValidationParameters, out SecurityToken validatedToken);
            var roleClaim = ClaimsPrincipal.Claims.FirstOrDefault(Claim => Claim.Type == ClaimTypes.Role)?.Value;
            if (roleClaim == null)
            {
                context.Result = new RedirectToRouteResult(new { controller = "Auth", action = "User_Login" });
                return;
            }
            if (!_roles.Contains(roleClaim))
            {
                context.Result = new RedirectToRouteResult(new { Controller = "Auth", action = "AccessDenied" });
            }
        }
        catch
        {
            context.Result = new RedirectToRouteResult(new { controller = "Auth", action = "User_Login" });
        }
    }

}
