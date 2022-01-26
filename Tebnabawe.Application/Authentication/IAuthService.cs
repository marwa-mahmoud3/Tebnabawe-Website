using System.Threading.Tasks;
using Tebnabawe.Application.Authentication.Dto;

namespace Tebnabawe.Application.Authentication
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model,string Role);
        Task<AuthModel> LoginAsync(LoginModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthModel> ConfirmEmailAsync(string userId, string token);
        Task<AuthModel> ForgetPasswordAsync(ForgotPasswordModel forgotPasswordModel);
        Task<AuthModel> ResetPasswordAsync(ResetPasswordModel model);
    }
}