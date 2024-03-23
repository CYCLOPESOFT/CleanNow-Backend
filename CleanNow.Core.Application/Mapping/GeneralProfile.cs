using AutoMapper;
using CleanNow.Core.Application.Dto.Account;
using CleanNow.Core.Application.Dto.Account.Forgot;
using CleanNow.Core.Application.Dto.Account.Register;
using CleanNow.Core.Application.Dto.Account.ResetPassword;
using CleanNow.Core.Application.Dto.DetailsDomicile;
using CleanNow.Core.Application.Features.DetailsDomiciles.Commands.CreateDetailsDomicile;
using CleanNow.Core.Application.Features.DetailsDomiciles.Commands.UpdateDetailsDomicile;
using CleanNow.Core.Application.ViewModels.User;
using CleanNow.Core.Domain.Entities;

namespace CleanNow.Core.Application.Mapping
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            CreateMap<DetailsDomicile, GetDetailsDomicileDto>()
             .ReverseMap()
             .ForMember(x => x.CreatedDate, opt => opt.Ignore());


            CreateMap<CreateDetailsDomicileCommand,DetailsDomicile>()
                .ForMember(x=>x.CreatedDate, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateDetailsDomicileCommand, DetailsDomicile>()
    .ForMember(x => x.CreatedDate, opt => opt.Ignore())
    .ReverseMap();

            CreateMap<DetailsDomicileUpdateResponse, DetailsDomicile>()
    .ForMember(x => x.CreatedDate, opt => opt.Ignore())
    .ReverseMap();



            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(a => a.Error, x => x.Ignore())
                .ForMember(a => a.HasError, x => x.Ignore())
                .ReverseMap();
            CreateMap<RegisterRequest, UserSaveViewModel>()
                .ForMember(r=>r.Error, x=>x.Ignore())
                .ForMember(r=>r.HasError, x=>x.Ignore())
                .ReverseMap();
            CreateMap<ForgotRequest, ForgotPasswordViewModel>()
                .ForMember(r => r.Error, x => x.Ignore())
                .ForMember(r => r.HasError, x => x.Ignore())
                .ReverseMap();
            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(r => r.Error, x => x.Ignore())
                .ForMember(r => r.HasError, x => x.Ignore())
                .ReverseMap();
        }
    }
}
