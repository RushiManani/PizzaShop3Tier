using Microsoft.AspNetCore.Http;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Interfaces;

public interface IUserRepository
{
    List<UserViewModel> GetUserAsync(int page, int pageSize);
    Task DeleteUserAsync(int userId);
    Task<bool> AddUserAsync(NewUserModel model);
    Task<EditUserModel?> GetUserByIDAsync(int userID);
    Task UpdateUserAsync(EditUserModel model);
    Task<string?> UploadPhotoAsync(IFormFile photo);
}
