using AutoMapper;
using CleanNow.Core.Application.Dto.DetailsDomicile;
using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.DetailsDomiciles.Queries.GetAllDetailsDomicile
{
    public class GetAllDetailsDomicileQuery:IRequest<IEnumerable<GetDetailsDomicileDto>>
    {
    }
    public class GetAllDetailsDomicileQueryHandler : IRequestHandler<GetAllDetailsDomicileQuery, IEnumerable<GetDetailsDomicileDto>>
    {
        private readonly IDetailsDomicileRepository _detailsRepository;
        private readonly IMapper _mapper;
        public GetAllDetailsDomicileQueryHandler(IDetailsDomicileRepository detailsRepository, IMapper mapper)
        {
            _detailsRepository = detailsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetDetailsDomicileDto>> Handle(GetAllDetailsDomicileQuery request, CancellationToken cancellationToken)
        {
            var detailsList = await _detailsRepository.GetAllAsync();
            if (detailsList == null || detailsList.Count == 0) throw new Exception("Details not found");

            return _mapper.Map<IEnumerable<GetDetailsDomicileDto>>(detailsList);
        }
    }
}
