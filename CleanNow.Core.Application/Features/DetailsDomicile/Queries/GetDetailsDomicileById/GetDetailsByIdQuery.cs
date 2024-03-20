using AutoMapper;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Application.ViewModels.DetailsDomicile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.DetailsDomiciles.Queries.GetDetailsDomicileById
{
    public class GetDetailsByIdQuery:IRequest<DetailsDomicileViewModel>
    {
        public int Id { get; set; }
    }
    public class GetDetailsByIdQueryHandler:IRequestHandler<GetDetailsByIdQuery, DetailsDomicileViewModel>
    {
        private readonly IDetailsDomicileRepository _detailsRepository;
        private readonly IMapper _mapper;
        public GetDetailsByIdQueryHandler(IDetailsDomicileRepository detailsRepository, IMapper mapper)
        {
            _detailsRepository = detailsRepository;
            _mapper = mapper;
        }

        public async Task<DetailsDomicileViewModel> Handle(GetDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var detail = await _detailsRepository.GetAsync(request.Id);
            if (detail == null) throw new Exception("Detail not found");

            return _mapper.Map<DetailsDomicileViewModel>(detail);
        }

    }

}
