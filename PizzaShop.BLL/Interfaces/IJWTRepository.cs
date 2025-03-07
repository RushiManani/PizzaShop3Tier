using Microsoft.AspNetCore.Http;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Interfaces;

public interface IJWTRepository
{
    string GenerateJwtToken(string username, string email, string role, DateTime? expireTime);
    List<string> ReadJWTToken();
    string GenerateExpiryToken(string email,string role,HttpResponse httpResponse=null);
    Task<bool> ValidateToken(string ResetToken, ResetPassword model);
}
