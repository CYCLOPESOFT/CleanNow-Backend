using CleanNow.Core.Application.Dto.Account;
using CleanNow.Core.Application.Dto.Account.Forgot;
using CleanNow.Core.Application.Dto.Account.Register;
using CleanNow.Core.Application.Dto.Account.ResetPassword;
using CleanNow.Core.Application.Dto.Email;
using CleanNow.Core.Application.Enum;
using CleanNow.Core.Application.Interfaces.Identity;
using CleanNow.Core.Application.Interfaces.Shared;
using CleanNow.Infrastructured.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Infrastructured.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _emailService = emailService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //Login
        public async Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"Not account registered with {request.Email}";
                return response;
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, true, false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.Email}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.Email}";
                return response;
            }
            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.Name = user.Name;
            return response;
        }

        //Logout
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        //Process Register
        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new();
            response.HasError = false;
            var userWithEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already register";
                return response;
            }
            var userName = await _userManager.FindByNameAsync(request.Username);
            if (userName != null)
            {
                response.HasError = true;
                response.Error = $"Username '{request.Username}' is already taken";
                return response;
            }
            var defaultUser = new ApplicationUser
            {
                Email = request.Email,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                UserName = request.Username,
                CreatedAt = DateTime.Now,
            };
            var result = await _userManager.CreateAsync(defaultUser, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                var verificationUri = await SendVerificationEmailUrl(defaultUser, origin);
                await _emailService.SendEmailAsync(new EmailRequest()
                {
                    To = defaultUser.Email,
                    Body = $"Please confirm your account visiting this URL {verificationUri}",
                    Subject = "Confirma registration"
                });
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }
            return response;
        }
        //Confirmar correo.
        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "No accounts registered with this user";
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.Email}. You can now use the app";
            }
            else
            {
                return $"An error occurred with confirming {user.Email}";
            }
        }

        //Forgot Password
        public async Task<ForgotResponse> ForgotPasswordAsync(ForgotRequest request, string origin)
        {
            ForgotResponse response = new() {
            HasError = false,
            };
            var account = await _userManager.FindByEmailAsync(request.Email);
            if (account == null)
            {
                response.HasError = true;
                response.Error = $"No account register with {request.Email}";
                return response;
            }
            var verificationUri = await SendForgotPasswordUri(account, origin);
            await _emailService.SendEmailAsync(new EmailRequest()
            {
                To = account.Email,
                Body = $"Please reset your account visiting this URL {verificationUri}",
                Subject = "Reset password"
            });
            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new() { 
            HasError= false
            };
            var account = await _userManager.FindByEmailAsync(request.Email);
            if (account == null)
            {
                response.HasError = true;
                response.Error = $"No account register with {request.Email}";
                return response;
            }
            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(account, request.Token, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while resseting";
                return response;
            }

            return response;
        }
         
        //Send verificacion de email with token.
        private async Task<string> SendVerificationEmailUrl(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);
            return verificationUri;
        }

        //ForgotPasswordUri
        private async Task<string> SendForgotPasswordUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPasword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);
            return verificationUri;
        }
    }
}
