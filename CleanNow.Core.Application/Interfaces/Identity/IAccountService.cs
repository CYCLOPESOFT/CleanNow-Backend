using CleanNow.Core.Application.Dto.Account.Register;
using CleanNow.Core.Application.Dto.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanNow.Core.Application.Dto.Account.Forgot;
using CleanNow.Core.Application.Dto.Account.ResetPassword;

namespace CleanNow.Core.Application.Interfaces.Identity
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest request);
        Task<ForgotResponse> ForgotPasswordAsync(ForgotRequest request, string origin);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task SignOutAsync();
        Task<GenerateResponse> GenerateCodeAsync(GenerateRequest generate);
    }
}
