using System.Security.Claims;
using System.Text;
using PizzaShop.BLL.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace PizzaShop.BLL.Repository;

public class JWTRepository : IJWTRepository
{

    private readonly IHttpContextAccessor _httpContextAccessor;

    public JWTRepository(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GenerateJwtToken(string username, string email, string role,string profilePicture)
    {
        var claims = new[]
        {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim("ProfilePicture", profilePicture)
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey@2024#JWTAuth!$%^&*()"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "AuthenticationDemo",
            audience: "AuthenticationDemoUsers",
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public List<string> ReadJWTToken()
    {
        List<string> list = new List<string>();
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext.Request.Cookies.TryGetValue("JWT", out string? token))
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var username = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var profilePicture = jwtToken.Claims.FirstOrDefault(c=>c.Type=="ProfilePicture")?.Value;
            list.Add(email!);
            list.Add(username!);
            list.Add(role!);
            list.Add(profilePicture!);
            return list!;
        }
        return list;
    }
}
