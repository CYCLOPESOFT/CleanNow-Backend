using AutoMapper;
using CleanNow.Core.Application.Dto.Like;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Likes.Commands.UpdateLike
{
    public class UpdateLikeCommand:IRequest<UpdateLikeResponse>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int AssistantId { get; set; }
        public bool isLike { get; set; }
        public string NameAssistant { get; set; }
    }
    public class UpdateLikeCommandHandler : IRequestHandler<UpdateLikeCommand, UpdateLikeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILikesRepository _likesRepository;

        public UpdateLikeCommandHandler(IMapper mapper, ILikesRepository likesRepository)
        {
            _mapper = mapper;
            _likesRepository = likesRepository;
        }
        public async Task<UpdateLikeResponse> Handle(UpdateLikeCommand request, CancellationToken cancellationToken)
        {
            var like = await _likesRepository.GetAsync(request.Id);
            if (like == null) throw new Exception($"Like not found with {request.Id}");
            like  = _mapper.Map<Like>(request);
            await _likesRepository.UpdateAsync(like, like.Id);
            return _mapper.Map<UpdateLikeResponse>(like);

        }
    }
}
