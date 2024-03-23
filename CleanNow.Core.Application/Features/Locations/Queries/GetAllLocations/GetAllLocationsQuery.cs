using AutoMapper;
using CleanNow.Core.Application.Dto.Locations;
using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Locations.Queries.GetAllLocations
{
    public class GetAllLocationsQuery:IRequest<IEnumerable<GetLocationsResponse>>
    {
    }

    public class GetAllLocationsQueryHandler : IRequestHandler<GetAllLocationsQuery, IEnumerable<GetLocationsResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;

        public GetAllLocationsQueryHandler(IMapper mapper, ILocationRepository locationRepository)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
        }
        public async Task<IEnumerable<GetLocationsResponse>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _locationRepository.GetAllAsync();
            if (locations == null || locations.Count == 0) throw new Exception("Locations not found");
            return _mapper.Map<IEnumerable<GetLocationsResponse>>(locations);
        }
    }
}
