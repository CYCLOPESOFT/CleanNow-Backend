using AutoMapper;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.DetailsDomiciles.Commands.CreateDetailsDomicile
{
    public class CreateDetailsDomicileCommand:IRequest<int>
    {
        public string Addresses { get; set; }
        public string? Apt { get; set; }
        public string? TypeClean { get; set; }
        public string Time { get; set; }
        public string ImageDomicile { get; set; }
    }
    public class CreateDetailsDomicileCommandHandler : IRequestHandler<CreateDetailsDomicileCommand, int>
    {
        private readonly IDetailsDomicileRepository _detailsRepository;
        private readonly IMapper _mapper;
        public CreateDetailsDomicileCommandHandler(IDetailsDomicileRepository detailsRepository, IMapper mapper)
        {
            _detailsRepository = detailsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateDetailsDomicileCommand request, CancellationToken cancellationToken)
        {
            var details = _mapper.Map<DetailsDomicile>(request);
            details = await _detailsRepository.AddAsync(details);
            return details.Id;
        }
    }
}
