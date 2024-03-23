using AutoMapper;
using CleanNow.Core.Application.Dto.Locations;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Locations.Commands.UpdateLocations
{
    public class UpdateLocationsCommand:IRequest<UpdateLocationsResponse>
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string Apt { get; set; }
        public string Doorbell { get; set; }
        public string City { get; set; }
    }

    public class UpdateLocationsCommandHandler : IRequestHandler<UpdateLocationsCommand, UpdateLocationsResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;

        public UpdateLocationsCommandHandler(IMapper mapper, ILocationRepository locationRepository)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
        }
        public async Task<UpdateLocationsResponse> Handle(UpdateLocationsCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetAsync(request.Id);
            if (location == null) throw new Exception("Location not found");
            await _locationRepository.UpdateAsync(location,location.Id);
            return _mapper.Map<UpdateLocationsResponse>(location);
        }
    }
}
