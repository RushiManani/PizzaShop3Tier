using Microsoft.AspNetCore.Http;

namespace PizzaShop.BLL.Interfaces;

public interface IJWTRepository
{
    string GenerateJwtToken(string username, string email, string role,string profilePicture);
    List<string> ReadJWTToken();
}
