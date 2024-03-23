using AutoMapper;
using CleanNow.Core.Application.Dto.Solicits;
using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Solicits.Queries.GetAllSolicits
{
    public class GetAllSolicitsQuery:IRequest<IEnumerable<GetSolicitsResponse>>
    {
    }
    public class GetAllSolicitsQueryHandler : IRequestHandler<GetAllSolicitsQuery, IEnumerable<GetSolicitsResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ISolicitRepository _solicitRepository;

        public GetAllSolicitsQueryHandler(IMapper mapper, ISolicitRepository solicitRepository)
        {
            _mapper = mapper;
            _solicitRepository = solicitRepository;
        }
        public async Task<IEnumerable<GetSolicitsResponse>> Handle(GetAllSolicitsQuery request, CancellationToken cancellationToken)
        {
            var solicits = await _solicitRepository.GetAllAsync();
            if (solicits == null || solicits.Count == 0) throw new Exception("Solicits not found");
            return _mapper.Map<IEnumerable<GetSolicitsResponse>>(solicits);
        }
    }
}
