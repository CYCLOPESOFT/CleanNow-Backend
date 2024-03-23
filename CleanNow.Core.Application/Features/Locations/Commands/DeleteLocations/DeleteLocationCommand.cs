using AutoMapper;
using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Locations.Commands.DeleteLocations
{
    public class DeleteLocationCommand:IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;

        public DeleteLocationCommandHandler(IMapper mapper, ILocationRepository locationRepository)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
        }
        public async Task<int> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetAsync(request.Id);
            if (location == null) throw new Exception("Location not found");
            await _locationRepository.DeleteAsync(location);
            return location.Id;
        }
    }
}
