using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PizzaShop.BLL.Interfaces;
using PizzaShop.DAL.Data;
using PizzaShop.DAL.Models;

namespace PizzaShop.BLL.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly PizzaShopDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthRepository(PizzaShopDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<User> LoginAsync(string email, string password, bool rememberMe)
    {
        if (email != null)
        {

            var users = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (users != null)
            {
                var encryptHash = Encrypt(password);
                users = await _dbContext.Users.FirstOrDefaultAsync(u => u.Password == encryptHash);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                };
                if (rememberMe)
                {
                    var httpContext = _httpContextAccessor.HttpContext;
                    httpContext!.Response.Cookies.Append("UserEmail", users!.Email, cookieOptions);
                    cookieOptions.Expires = DateTime.UtcNow.AddDays(30);
                }
            }
            return users!;
        }
        return null!;
    }

    public async Task<User> ForgotPasswordAsync(string email)
    {
        var user = await _dbContext.Users.FindAsync(email);
        return user!;
    }

    public async Task<string> GetRoleAsync(int roleID)
    {
        if (roleID != 0)
        {
            var role = await _dbContext.Roles.FindAsync(roleID);
            return role!.RoleName;
        }
        return null!;
    }

    #region Encrypt & Decrypt Password

    public string Encrypt(string password)
    {
        try
        {
            byte[] encData_byte = new byte[password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }

    public string Decrypt(string encodedData)
    {
        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        System.Text.Decoder utf8Decode = encoder.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(encodedData);
        int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        char[] decoded_char = new char[charCount];
        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        string result = new String(decoded_char);
        return result;
    }

    #endregion
}
