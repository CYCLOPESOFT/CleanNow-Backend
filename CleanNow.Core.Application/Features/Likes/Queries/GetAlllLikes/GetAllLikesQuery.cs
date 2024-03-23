using AutoMapper;
using CleanNow.Core.Application.Dto.Like;
using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Likes.Queries.GetAlllLikes
{
    public class GetAllLikesQuery:IRequest<IEnumerable<GetLikeResponse>>
    {
    }
    public class GetAllLikesQueryHandler : IRequestHandler<GetAllLikesQuery, IEnumerable<GetLikeResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ILikesRepository _likesRepository;

        public GetAllLikesQueryHandler(IMapper mapper, ILikesRepository likesRepository)
        {
            _mapper = mapper;
            _likesRepository = likesRepository;
        }

        public async Task<IEnumerable<GetLikeResponse>> Handle(GetAllLikesQuery request, CancellationToken cancellationToken)
        {
            var likes = await GetAllLikesAsync();
            if (likes == null || likes.Count() == 0) throw new Exception($"Likes not found");

            return likes;
        }

        private async Task<IEnumerable<GetLikeResponse>> GetAllLikesAsync()
        {
            var likes = await _likesRepository.GetAllIncludeAsync(new List<string> { "assistant"});
            return likes.Select(l => new GetLikeResponse
            {
                AssistantId = l.Id,
                Id = l.Id,
                isLike = l.isLike,
                NameAssistant = l.assistant.Name,
                UserId = l.UserId,
                CreatedDate = l.CreatedDate
            }).ToList();
        }
    }
}
