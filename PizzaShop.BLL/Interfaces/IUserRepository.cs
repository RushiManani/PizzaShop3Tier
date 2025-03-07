using Microsoft.AspNetCore.Http;
using PizzaShop.DAL.Models;
using PizzaShop.DAL.ViewModel;

namespace PizzaShop.BLL.Interfaces;

public interface IUserRepository
{
    Task<(List<UserViewModel> UserList, int Count, int PageSize, int PageNumber, string SortBy, string SortOrder, string SearchKey)> GetUserAsync(int PageSize, int PageNumber, string sortBy, string sortOrder, string SearchKey);
    Task DeleteUserAsync(int userId);
    Task<bool> AddUserAsync(NewUserModel model);
    Task<EditUserModel?> GetUserByIDAsync(int userID);
    Task<bool> UpdateUserAsync(EditUserModel model);
    Task<string?> UploadPhotoAsync(IFormFile photo);
    List<UserViewModel> GetUserByNameAsync(string searchString);
    User GetUserByEmail(string email);
}
