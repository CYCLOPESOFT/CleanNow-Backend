using AutoMapper;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Solicits.Commands.CreateSolicits
{
    public class CreateSolicitsCommand:IRequest<int>
    {
        public bool Status { get; set; }
        public DateTime SelectedDate { get; set; }
        public int HiringId { get; set; }
    }
    public class CreateSolicitsCommandHandler : IRequestHandler<CreateSolicitsCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ISolicitRepository _solicitRepository;

        public CreateSolicitsCommandHandler(IMapper mapper, ISolicitRepository solicitRepository)
        {
            _mapper = mapper;
            _solicitRepository = solicitRepository;
        }
        public async Task<int> Handle(CreateSolicitsCommand request, CancellationToken cancellationToken)
        {
            var solicit = _mapper.Map<Solicit>(request);
            var added = await _solicitRepository.AddAsync(solicit);
            return added.Id;
        }
    }
}
