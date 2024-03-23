using AutoMapper;
using CleanNow.Core.Application.Dto.Solicits;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Solicits.Commands.UpdateSolicits
{
    public class UpdateSolicitsCommand:IRequest<UpdateSolicitsResponse>
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime SelectedDate { get; set; }
        public int HiringId { get; set; }
    }
    public class UpdateSolicitsCommandHandler : IRequestHandler<UpdateSolicitsCommand, UpdateSolicitsResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISolicitRepository _solicitRepository;

        public UpdateSolicitsCommandHandler(IMapper mapper, ISolicitRepository solicitRepository)
        {
            _mapper = mapper;
            _solicitRepository = solicitRepository;
        }
        public async Task<UpdateSolicitsResponse> Handle(UpdateSolicitsCommand request, CancellationToken cancellationToken)
        {
            var solicit = await _solicitRepository.GetAsync(request.Id);
            if (solicit == null) throw new Exception("Solicit not found");
            await _solicitRepository.UpdateAsync(_mapper.Map<Solicit>(request), solicit.Id);
            return _mapper.Map<UpdateSolicitsResponse>(solicit);
        }
    }
}
