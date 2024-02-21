using CleanNow.Core.Application.Dto.Account.Forgot;
using CleanNow.Core.Application.Dto.Account.Register;
using CleanNow.Core.Application.Dto.Account.ResetPassword;
using CleanNow.Core.Application.Dto.Account;
using CleanNow.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Interfaces.Service
{
    public interface IUsuarioService
    {
        Task<string> ConfirmAsync(string userId, string origin);
        Task<ForgotResponse> ForgotAsync(ForgotPasswordViewModel vm, string token);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(UserSaveViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ForgotPasswordViewModel vm);
        Task SignOutAsync();
    }
}
