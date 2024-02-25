using Ecommerce.Shared.Auth;
using Ecommerce.Shared.User;

namespace BlazorEcommerce.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> Register(UserRegister request);
        Task<ApiResponse<AuthResponseDto>> Login(UserLogin request);
        Task<string> RefreshToken();
        Task<bool> IsUserAuthenticated();
    }
}
