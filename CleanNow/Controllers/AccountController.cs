using CleanNow.Core.Application.Dto.Account;
using CleanNow.Core.Application.Dto.Account.Forgot;
using CleanNow.Core.Application.Dto.Account.Register;
using CleanNow.Core.Application.Dto.Account.ResetPassword;
using CleanNow.Core.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticationAsync(request));
        }
        [HttpPost("register")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            var requestRegister = await _accountService.RegisterBasicUserAsync(request, origin);
            if(requestRegister.StatusCode == 400) 
            {
                return BadRequest(400);
            }else if (requestRegister.StatusCode == 500)
            {
                return StatusCode(500, requestRegister);
            }
            return Ok(requestRegister);
        }
        [HttpPost("generate-code")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GenerateCode([FromBody] GenerateRequest email)
        {
            var generate = await _accountService.GenerateCodeAsync(email);
            if(generate.StatusCode == 200)
            {
                return Ok(generate);
            }else if(generate.StatusCode ==400)
            {
                return BadRequest(generate);
            }
            return StatusCode(500,generate);

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
