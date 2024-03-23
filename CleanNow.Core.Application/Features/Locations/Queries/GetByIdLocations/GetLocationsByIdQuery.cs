using AutoMapper;
using CleanNow.Core.Application.Dto.Locations;
using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Locations.Queries.GetByIdLocations
{
    public class GetLocationsByIdQuery:IRequest<GetLocationsResponse>
    {
        public int Id { get; set; }
    }

    public class GetLocationsByIdQueryHandler : IRequestHandler<GetLocationsByIdQuery, GetLocationsResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;

        public GetLocationsByIdQueryHandler(IMapper mapper, ILocationRepository locationRepository)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
        }
        public async Task<GetLocationsResponse> Handle(GetLocationsByIdQuery request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetAsync(request.Id);
            if (location == null) throw new Exception("Location not found");
            return _mapper.Map<GetLocationsResponse>(location);
        }
    }
}
