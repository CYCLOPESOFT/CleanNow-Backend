using AutoMapper;
using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Opinions.Commands.DeleteOpinions
{
    public class DeleteOpinionsCommand:IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteOpinionsCommandHandler : IRequestHandler<DeleteOpinionsCommand, int>
    {

        private readonly IMapper _mapper;
        private readonly IOpinionsRepository _opinionRepository;

        public DeleteOpinionsCommandHandler(IMapper mapper, IOpinionsRepository opinionRepository)
        {
            _mapper = mapper;
            _opinionRepository = opinionRepository;
        }
        public async Task<int> Handle(DeleteOpinionsCommand request, CancellationToken cancellationToken)
        {
            var opinion = await _opinionRepository.GetAsync(request.Id);
            if(opinion == null) throw new Exception("Opinions not found");
            await _opinionRepository.DeleteAsync(opinion);
            return opinion.Id;
        }
    }
}
