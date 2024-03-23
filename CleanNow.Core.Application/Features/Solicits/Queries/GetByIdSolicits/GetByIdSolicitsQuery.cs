using AutoMapper;
using CleanNow.Core.Application.Dto.Solicits;
using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Solicits.Queries.GetByIdSolicits
{
    public class GetByIdSolicitsQuery:IRequest<GetSolicitsResponse>
    {
        public int Id { get; set; }
    }
    public class GetByIdSolicitsQueryHandler : IRequestHandler<GetByIdSolicitsQuery, GetSolicitsResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISolicitRepository _solicitRepository;

        public GetByIdSolicitsQueryHandler(IMapper mapper, ISolicitRepository solicitRepository)
        {
            _mapper = mapper;
            _solicitRepository = solicitRepository;
        }
        public async Task<GetSolicitsResponse> Handle(GetByIdSolicitsQuery request, CancellationToken cancellationToken)
        {
            var solicit = await _solicitRepository.GetAsync(request.Id);
            if (solicit == null) throw new Exception("Solicit not found");
            return _mapper.Map<GetSolicitsResponse>(solicit);
        }
    }
}
