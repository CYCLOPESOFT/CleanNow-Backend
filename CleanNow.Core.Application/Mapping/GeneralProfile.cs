using AutoMapper;
using CleanNow.Core.Application.Dto.Account;
using CleanNow.Core.Application.Dto.Account.Forgot;
using CleanNow.Core.Application.Dto.Account.Register;
using CleanNow.Core.Application.Dto.Account.ResetPassword;
using CleanNow.Core.Application.Dto.Assistans;
using CleanNow.Core.Application.Dto.DetailsDomicile;
using CleanNow.Core.Application.Dto.Hirings;
using CleanNow.Core.Application.Dto.Like;
using CleanNow.Core.Application.Dto.Locations;
using CleanNow.Core.Application.Dto.Opinions;
using CleanNow.Core.Application.Dto.Solicits;
using CleanNow.Core.Application.Features.Assistants.Commands.CreateAssistant;
using CleanNow.Core.Application.Features.Assistants.Commands.UpdateAssistant;
using CleanNow.Core.Application.Features.Assistants.Queries.GetByIdAssitants;
using CleanNow.Core.Application.Features.DetailsDomiciles.Commands.CreateDetailsDomicile;
using CleanNow.Core.Application.Features.DetailsDomiciles.Commands.UpdateDetailsDomicile;
using CleanNow.Core.Application.Features.Hirings.Commands.CreateHirings;
using CleanNow.Core.Application.Features.Hirings.Commands.UpdateHirings;
using CleanNow.Core.Application.Features.Hirings.Queries.GetHiringsById;
using CleanNow.Core.Application.Features.Likes.Commands.CreateLike;
using CleanNow.Core.Application.Features.Likes.Commands.UpdateLike;
using CleanNow.Core.Application.Features.Likes.Queries.GetLikesById;
using CleanNow.Core.Application.Features.Locations.Commands.CreateLocations;
using CleanNow.Core.Application.Features.Locations.Commands.UpdateLocations;
using CleanNow.Core.Application.Features.Opinions.Commands.CreateOpinions;
using CleanNow.Core.Application.Features.Opinions.Commands.UpdateOpinions;
using CleanNow.Core.Application.Features.Opinions.Queries.GetByIdLocations;
using CleanNow.Core.Application.Features.Solicits.Commands.CreateSolicits;
using CleanNow.Core.Application.Features.Solicits.Commands.UpdateSolicits;
using CleanNow.Core.Application.ViewModels.User;
using CleanNow.Core.Domain.Entities;

namespace CleanNow.Core.Application.Mapping
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            #region Solicits

            CreateMap<Solicit, GetSolicitsResponse>()
             .ReverseMap()
             .ForMember(x => x.UpdatedDate, opt => opt.Ignore());

            CreateMap<CreateSolicitsCommand, Solicit>()
                .ForMember(x => x.hiring, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateSolicitsCommand, Solicit>()
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<UpdateSolicitsResponse, Solicit>()
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ReverseMap();
            #endregion
            #region Opinions
            CreateMap<GetByIdOpinionsParameter, GetByIdOpinionsQuery>()
                .ReverseMap();

            CreateMap<Opinion, GetOpinionsResponse>()
                .ForMember(x => x.AssistantName, opt => opt.Ignore())
             .ReverseMap()
             .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
             .ForMember(x => x.assistant, opt => opt.Ignore());

            CreateMap<CreateOpinionsCommand, Opinion>()
                .ForMember(x => x.assistant, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateOpinionsCommand, Opinion>()
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<UpdateOpinionsResponse, Opinion>()
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ReverseMap();
            #endregion
            #region Likes
            CreateMap<GetLikeByIdParameter, GetLikeByIdQuery>()
                .ReverseMap();

            CreateMap<Like, GetLikeResponse>()
                .ForMember(x=>x.NameAssistant, opt=>opt.Ignore())
             .ReverseMap()
             .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
             .ForMember(x => x.assistant, opt => opt.Ignore());

            CreateMap<CreateLikeCommand, Like>()
                .ForMember(x => x.assistant, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateLikeCommand, Like>()
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<UpdateLikeResponse, Like>()
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ReverseMap();
            #endregion
            #region Locations
            CreateMap<GetByIdHiringsParameter, GetByIdHiringsQuery>()
                .ReverseMap();

            CreateMap<Location, GetLocationsResponse>()
             .ReverseMap()
             .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
             .ForMember(x => x.hirings, opt => opt.Ignore());

            CreateMap<CreateLocationsCommand, Location>()
                .ForMember(x => x.hirings, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateLocationsCommand, Location>()
                .ForMember(x => x.hirings, opt => opt.Ignore())
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<UpdateLocationsResponse, Location>()
                .ForMember(x => x.hirings, opt => opt.Ignore())
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ReverseMap();
            #endregion

            #region Hiring
            CreateMap<GetByIdHiringsParameter, GetByIdHiringsQuery>()
                .ReverseMap();

            CreateMap<Hiring, GetHiringsDto>()
                .ForMember(x => x.NameAssistant, opt => opt.Ignore())
                .ForMember(x => x.Address, opt => opt.Ignore())
             .ReverseMap()
             .ForMember(x => x.UpdatedDate, opt => opt.Ignore());

            CreateMap<CreateHiringsCommand, Hiring>()
                .ForMember(x=>x.assistant, opt=>opt.Ignore())
                .ForMember(x=>x.location, opt=>opt.Ignore())
                .ForMember(x => x.solicit, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateHiringsCommand, Hiring>()
                 .ForMember(x => x.assistant, opt => opt.Ignore())
                .ForMember(x => x.location, opt => opt.Ignore())
                .ForMember(x => x.solicit, opt => opt.Ignore())
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<HiringsUpdateResponse, Hiring>()
                .ForMember(x => x.assistant, opt => opt.Ignore())
                .ForMember(x => x.location, opt => opt.Ignore())
                .ForMember(x => x.solicit, opt => opt.Ignore())
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ReverseMap();
            #endregion
            #region Assistant
            CreateMap<GetAssistansByIdParameter, GetAssistansByIdQuery>()
                .ReverseMap();

            CreateMap<Assistant, AssistansGetDto>()
                .ForMember(x=>x.CountLikes,opt=>opt.Ignore())
             .ReverseMap()
             .ForMember(x => x.CreatedDate, opt => opt.Ignore())
             .ForMember(x => x.UpdatedDate, opt => opt.Ignore());

            CreateMap<Assistant, AssistansGetDto>()
                 .ForMember(x => x.CountLikes, opt => opt.Ignore())
             .ReverseMap()
                 .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                 .ForMember(x => x.UpdatedDate, opt => opt.Ignore());

            CreateMap<CreateAssistantCommand, Assistant>()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateAssistantCommand, Assistant>()
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<UpdateAssistantResponse, Assistant>()
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ReverseMap();
            #endregion

            #region DetailsDomicile
            CreateMap<DetailsDomicile, GetDetailsDomicileDto>()
             .ReverseMap()
             .ForMember(x => x.CreatedDate, opt => opt.Ignore())
             .ForMember(x => x.UpdatedDate, opt => opt.Ignore());


            CreateMap<CreateDetailsDomicileCommand,DetailsDomicile>()
                .ForMember(x=>x.CreatedDate, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateDetailsDomicileCommand, DetailsDomicile>()
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<DetailsDomicileUpdateResponse, DetailsDomicile>()
            .ForMember(x => x.CreatedDate, opt => opt.Ignore())
            .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
            .ReverseMap();
            #endregion

            #region Authentication
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
            #endregion
        }
    }
}
