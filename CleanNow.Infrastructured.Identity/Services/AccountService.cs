using Azure.Core;
using Azure;
using CleanNow.Core.Application.Dto.Account;
using CleanNow.Core.Application.Dto.Account.Forgot;
using CleanNow.Core.Application.Dto.Account.Register;
using CleanNow.Core.Application.Dto.Account.ResetPassword;
using CleanNow.Core.Application.Dto.Email;
using CleanNow.Core.Application.Enum;
using CleanNow.Core.Application.Interfaces.Identity;
using CleanNow.Core.Application.Interfaces.Shared;
using CleanNow.Core.Domain.Settings;
using CleanNow.Infrastructured.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CleanNow.Infrastructured.Identity.Services
{
    public class AccountService :IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;


        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, IOptions<JWTSettings> JWTSettings)
        {
            _emailService = emailService;
            _userManager = userManager;
            _jwtSettings = JWTSettings.Value;
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
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.Email}";
                return response;
            }
            await _signInManager.SignInAsync(user, true);

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.Name = user.Name;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;
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
            if (userWithEmail == null)
            {
                response.StatusCode = 400;
                response.HasError = true;
                response.Error = $"Email '{request.Email}' isn't register";
                return response;
            }

                userWithEmail.Name = request.Name;
                userWithEmail.Apellido = request.Apellido;
                 userWithEmail.PhoneNumber = request.PhoneNumber;
                 userWithEmail.CreatedAt = DateTime.Now;
                 userWithEmail.EmailConfirmed = true;
            userWithEmail.Image = request.Image;
            var result = await _userManager.UpdateAsync(userWithEmail);
            if (result.Succeeded)
            {
                response.StatusCode = 200;
                return response;
            }
            else
            {
                response.HasError = true;
                response.StatusCode = 500;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }
        }


        public async Task<GenerateResponse> GenerateCodeAsync(GenerateRequest generate)
        {
            var code = GenerateRandomCode();
            GenerateResponse response = new();
            response.HasError = false;
            var userWithEmail = await _userManager.FindByEmailAsync(generate.Email);
            if (userWithEmail != null)
            {
                response.StatusCode = 200;
                response.Message = "Send code for signIn";
                response.Code = code;
                await _emailService.SendEmailAsync(new EmailRequest()
                {
                    To = generate.Email,
                    Body = $"Your code for clean now it's {code}",
                    Subject = "Login User"
                });
                return response;
            }
            var defaultUser = new ApplicationUser
            {
                Email = generate.Email,
                Code = code,
                UserName = generate.Email.Split('@')[0].ToLower(),
            };
            var result = await _userManager.CreateAsync(defaultUser);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                await _emailService.SendEmailAsync(new EmailRequest()
                {
                    To = generate.Email,
                    Body = $"Your code for clean now it's {code}",
                    Subject = "Confirm Email"
                });
                response.StatusCode = 200;
                response.Code = code;
                response.Message = "Your code for clean now";
                return response;

            }
            else
            {
                response.StatusCode = 500;
                response.HasError = true;
                response.Error = $"Internal Error";
                return response;
            }

   
        }


        //Forgot Password
        public async Task<ForgotResponse> ForgotPasswordAsync(ForgotRequest request, string origin)
        {
            ForgotResponse response = new()
            {
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
            ResetPasswordResponse response = new()
            {
                HasError = false
            };
            var account = await _userManager.FindByEmailAsync(request.Email);
            if (account == null)
            {
                response.HasError = true;
                response.Error = $"No account register with {request.Email}";
                return response;
            }
            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(account, request.Token, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while resseting";
                return response;
            }

            return response;
        }

        private string GenerateRandomCode()
        {
            Random random = new Random();
            HashSet<string> usedCodes = new HashSet<string>();

            while (true)
            {
                string code = random.Next(1000, 9999).ToString();

                if (!usedCodes.Contains(code))
                {
                    usedCodes.Add(code);
                    return code;
                }
            }
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
        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredetials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredetials);

            return jwtSecurityToken;
        }
        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var ramdomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(ramdomBytes);

            return BitConverter.ToString(ramdomBytes).Replace("-", "");
        }

    }
}
