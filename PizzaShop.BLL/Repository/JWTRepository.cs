using System.Security.Claims;
using System.Text;
using PizzaShop.BLL.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Repository;

public class JWTRepository : IJWTRepository
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;

    public JWTRepository(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    public string GenerateJwtToken(string username, string email, string role, DateTime? expireTime)
    {
        var claims = new[]
        {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
                // new Claim("ProfilePicture", profilePicture)
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey@2024#JWTAuth!$%^&*()"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "AuthenticationDemo",
            audience: "AuthenticationDemoUsers",
            claims: claims,
            expires: expireTime == null ? DateTime.Now.AddDays(1) : expireTime,
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateExpiryToken(string email, string role, HttpResponse response)
    {
        var user = _userRepository.GetUserByEmail(email);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var token = GenerateJwtToken(user.UserName, email, role, DateTime.Now.AddMinutes(1));

        response.Cookies.Append("ResetToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.Now.AddMinutes(1)
        });

        // var resetToken = new PasswordResetToken
        // {
        //     UserId = user.UserId,
        //     Token = token,
        //     ExpiryDateTime = DateTime.Now.AddHours(24) // Token valid for 1 hour
        // };

        return token;
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
            // var profilePicture = jwtToken.Claims.FirstOrDefault(c=>c.Type=="ProfilePicture")?.Value;
            list.Add(email!);
            list.Add(username!);
            list.Add(role!);
            // list.Add(profilePicture!);
            return list!;
        }
        return list;
    }

    public async Task<bool> ValidateToken(string ResetToken, ResetPassword model)
    {
        try
        {
            if (ResetToken == null)
            {
                return false;
            }
            var handler = new JwtSecurityTokenHandler();
            var JwtToken = handler.ReadJwtToken(ResetToken);

            if (JwtToken.ValidTo < DateTime.Now)
            {

                model.TokenValid = false;
                return true;
            }
            model.TokenValid = true;
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("error: while validating try again later" + ex);
            return false;
        }
    }
}
