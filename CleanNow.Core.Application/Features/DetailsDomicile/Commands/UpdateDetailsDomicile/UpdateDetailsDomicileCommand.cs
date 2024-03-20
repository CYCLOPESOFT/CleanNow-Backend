using AutoMapper;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.DetailsDomiciles.Commands.UpdateDetailsDomicile
{
    public class UpdateDetailsDomicileCommand:IRequest<DetailsDomicileUpdateResponse>
    {
        public int Id { get; set; }
        public string Addresses { get; set; }
        public string? Apt { get; set; }
        public string? TypeClean { get; set; }
        public string Time { get; set; }
        public string ImageDomicile { get; set; }
    }
    public class UpdateDetailsDomicileCommandHandler:IRequestHandler<UpdateDetailsDomicileCommand, DetailsDomicileUpdateResponse> 
    {
        private readonly IDetailsDomicileRepository _detailsRepository;
        private readonly IMapper _mapper;
        public UpdateDetailsDomicileCommandHandler(IDetailsDomicileRepository detailsRepository, IMapper mapper)
        {
            _detailsRepository = detailsRepository;
            _mapper = mapper;
        }

        public async Task<DetailsDomicileUpdateResponse> Handle(UpdateDetailsDomicileCommand command, CancellationToken cancellationToken)
        {
            var details = await _detailsRepository.GetAsync(command.Id);
            if (details == null) throw new Exception("Details not found");

            details = _mapper.Map<DetailsDomicile>(command);
            await _detailsRepository.UpdateAsync(details, details.Id);
            var detailsResponse = _mapper.Map<DetailsDomicileUpdateResponse>(details);
            return detailsResponse;
        }
    }
}
