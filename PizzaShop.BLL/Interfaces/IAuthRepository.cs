using PizzaShop.DAL.Models;

namespace PizzaShop.BLL.Interfaces;

public interface IAuthRepository
{
    Task<User> LoginAsync(string email,string password,bool rememberMe);
    Task<string> GetRoleAsync(int roleID);
    Task<User> ForgotPasswordAsync(string email);
    Task<bool> SendEmailAsync(string toEmail, string subject, string body);
    void ResetPasswordAsync(string email,string newPassword);
}
