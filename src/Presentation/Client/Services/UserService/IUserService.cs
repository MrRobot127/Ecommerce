using Ecommerce.Shared.User;

namespace BlazorEcommerce.Client.Services.UserService
{
    public interface IUserService
    {
        Task<ApiResponse<string>> ChangePassword(UserChangePassword request);
    }
}
