using AutoMapper;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Solicits.Commands.DeleteSolicits
{
    public class DeleteSolicitsCommand:IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteSolicitsCommandHandler : IRequestHandler<DeleteSolicitsCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ISolicitRepository _solicitRepository;

        public DeleteSolicitsCommandHandler(IMapper mapper, ISolicitRepository solicitRepository)
        {
            _mapper = mapper;
            _solicitRepository = solicitRepository;
        }
        public async Task<int> Handle(DeleteSolicitsCommand request, CancellationToken cancellationToken)
        {
            var solicit = await _solicitRepository.GetAsync(request.Id);
            if (solicit == null) throw new Exception("Solicit not found");
            await _solicitRepository.DeleteAsync(solicit);
            return solicit.Id;
        }
    }
}
