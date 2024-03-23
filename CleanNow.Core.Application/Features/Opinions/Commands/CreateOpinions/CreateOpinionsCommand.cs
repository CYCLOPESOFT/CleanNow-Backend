using AutoMapper;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Opinions.Commands.CreateOpinions
{
    public class CreateOpinionsCommand:IRequest<int>
    {
        public int AssistantId { get; set; }
        public string? Description { get; set; }
        public int Start { get; set; }
        public string ValuerName { get; set; }
    }
    public class CreateOpinionsCommandHandler : IRequestHandler<CreateOpinionsCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IOpinionsRepository _opinionRepository;

        public CreateOpinionsCommandHandler(IMapper mapper, IOpinionsRepository opinionRepository)
        {
            _mapper = mapper;
            _opinionRepository = opinionRepository;
        }
        public async Task<int> Handle(CreateOpinionsCommand request, CancellationToken cancellationToken)
        {
            var opinion = _mapper.Map<Opinion>(request);
            var created = await _opinionRepository.AddAsync(opinion);
            return created.Id;
        }
    }
}
