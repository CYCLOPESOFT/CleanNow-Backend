using AutoMapper;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Locations.Commands.CreateLocations
{
    public class CreateLocationsCommand:IRequest<int>
    {
        public string Address { get; set; }
        public string Street { get; set; }
        public string Apt { get; set; }
        public string Doorbell { get; set; }
        public string City { get; set; }
    }

    public class CreateLocationsCommandHandler : IRequestHandler<CreateLocationsCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;

        public CreateLocationsCommandHandler(IMapper mapper, ILocationRepository locationRepository)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
        }
        public async Task<int> Handle(CreateLocationsCommand request, CancellationToken cancellationToken)
        {
            var location = _mapper.Map<Location>(request);
            location = await _locationRepository.AddAsync(location);
            return location.Id;
        }
    }
}
