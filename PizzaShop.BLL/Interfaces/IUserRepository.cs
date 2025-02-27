using Microsoft.AspNetCore.Http;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Interfaces;

public interface IUserRepository
{
    List<UserViewModel> GetUserAsync(int page, int pageSize);
    Task DeleteUserAsync(int userId);
    Task AddUserAsync(NewUserModel model);
    Task<NewUserModel?> GetUserByIDAsync(int userID);
    Task UpdateUserAsync(NewUserModel model);
    Task<string> UploadPhotoAsync(IFormFile photo);
}
