using AutoMapper;
using CleanNow.Core.Application.Dto.Like;
using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Likes.Queries.GetLikesById
{
    public class GetLikeByIdQuery:IRequest<GetLikeResponse>
    {
        public int Id { get; set; }
    }

    public class GetLikeByIdQueryHandler : IRequestHandler<GetLikeByIdQuery, GetLikeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILikesRepository _likesRepository;

        public GetLikeByIdQueryHandler(IMapper mapper, ILikesRepository likesRepository)
        {
            _mapper = mapper;
            _likesRepository = likesRepository;
        }

        public async Task<GetLikeResponse> Handle(GetLikeByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetLikeAsync(_mapper.Map<GetLikeByIdParameter>(request));
        }

        private async Task<GetLikeResponse> GetLikeAsync(GetLikeByIdParameter parameter)
        {
            var likes = await _likesRepository.GetAllIncludeAsync(new List<string> { "assistant" });
            var like= likes.FirstOrDefault(l => l.Id == parameter.Id);
            if (like == null) throw new Exception("Like not found");
            var response = _mapper.Map<GetLikeResponse>(like);
            response.NameAssistant = like.assistant.Name;
            return response;
        }
    }

}
