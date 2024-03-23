using AutoMapper;
using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Likes.Commands.DeleteLike
{
    public class DeleteLikeCommand:IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILikesRepository _likesRepository;

        public DeleteLikeCommandHandler(IMapper mapper, ILikesRepository likesRepository)
        {
            _mapper = mapper;
            _likesRepository = likesRepository;
        }
        public async Task<int> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
        {
            var like = await _likesRepository.GetAsync(request.Id);
            if (like == null) throw new Exception($"Like not found with {request.Id}");
            await _likesRepository.UpdateAsync(like, like.Id);
            return like.Id;
        }
    }
}
