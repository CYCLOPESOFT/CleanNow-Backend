using AutoMapper;
using CleanNow.Core.Application.Dto.Account;
using CleanNow.Core.Application.Dto.Account.Forgot;
using CleanNow.Core.Application.Dto.Account.Register;
using CleanNow.Core.Application.Interfaces.Identity;
using CleanNow.Core.Application.Interfaces.Service;
using CleanNow.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Service
{
    public class UsuarioService:IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        public UsuarioService(IMapper mapper, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<AuthenticationResponse> LoginAsync (LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            return await _accountService.AuthenticationAsync(loginRequest);
        }
        public async Task<RegisterResponse> RegisterAsync(UserSaveViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterBasicUserAsync(registerRequest, origin);
        }
        public async Task<string> ConfirmAsync(string userId, string origin)
        {
            return await _accountService.ConfirmAccountAsync(userId, origin);
        }
        //public async Task<ForgotResponse> ForgotAsync()
        //{
        //    //return await _accountService.ConfirmAccountAsync(userId, origin);
        //}
    }
}
