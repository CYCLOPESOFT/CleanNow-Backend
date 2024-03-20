using AutoMapper;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Application.ViewModels.DetailsDomicile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.DetailsDomiciles.Queries.GetAllDetailsDomicile
{
    public class GetAllDetailsDomicileQuery:IRequest<IEnumerable<DetailsDomicileViewModel>>
    {
    }
    public class GetAllDetailsDomicileQueryHandler : IRequestHandler<GetAllDetailsDomicileQuery, IEnumerable<DetailsDomicileViewModel>>
    {
        private readonly IDetailsDomicileRepository _detailsRepository;
        private readonly IMapper _mapper;
        public GetAllDetailsDomicileQueryHandler(IDetailsDomicileRepository detailsRepository, IMapper mapper)
        {
            _detailsRepository = detailsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DetailsDomicileViewModel>> Handle(GetAllDetailsDomicileQuery request, CancellationToken cancellationToken)
        {
            var detailsList = await _detailsRepository.GetAllAsync();
            if (detailsList == null || detailsList.Count == 0) throw new Exception("Details not found");

            return _mapper.Map<IEnumerable<DetailsDomicileViewModel>>(detailsList);
        }
    }
}
