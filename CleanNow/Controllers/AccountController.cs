using CleanNow.Core.Application.Dto.Account;
using CleanNow.Core.Application.Dto.Account.Forgot;
using CleanNow.Core.Application.Dto.Account.Register;
using CleanNow.Core.Application.Dto.Account.ResetPassword;
using CleanNow.Core.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CleanNow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticationAsync(request));
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterBasicUserAsync(request,origin));
        }
        [HttpGet("generate-code")]
        public async Task<IActionResult> GenerateCode(string email)
        {
            return Ok(await _accountService.GenerateCodeAsync(email));
        }

        [HttpGet("confirme-code")]
        public async Task<IActionResult> RegisterAsync(string email, string code)
        {
            return Ok(await _accountService.ConfirmAccountAsync(email, code));
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ForgotPasswordAsync(request, origin));
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetAsync(ResetPasswordRequest request)
        {
            return Ok(await _accountService.ResetPasswordAsync(request));
        }
    }
}
