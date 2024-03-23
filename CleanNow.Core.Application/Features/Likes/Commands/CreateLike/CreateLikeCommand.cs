using AutoMapper;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Likes.Commands.CreateLike
{
    public class CreateLikeCommand:IRequest<int>
    {
        public string UserId { get; set; }
        public int AssistantId { get; set; }
        public bool isLike { get; set; }
    }
    public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILikesRepository _likesRepository;

        public CreateLikeCommandHandler(IMapper mapper, ILikesRepository likesRepository)
        {
            _mapper = mapper;
            _likesRepository = likesRepository;
        }

        public async Task<int> Handle(CreateLikeCommand command, CancellationToken cancellationToken)
        {
            var like = _mapper.Map<Like>(command);
            like = await _likesRepository.AddAsync(like);
            return like.Id;
        }

    }
}
