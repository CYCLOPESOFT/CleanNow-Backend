using AutoMapper;
using CleanNow.Core.Application.Dto.Account;
using CleanNow.Core.Application.Dto.Account.Register;
using CleanNow.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Mapping
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(a => a.Error, x => x.Ignore())
                .ForMember(a => a.HasError, x => x.Ignore())
                .ReverseMap();
            CreateMap<RegisterRequest, UserSaveViewModel>()
                .ForMember(r=>r.Error, x=>x.Ignore())
                .ForMember(r=>r.HasError, x=>x.Ignore())
                .ReverseMap();
        }
    }
}
