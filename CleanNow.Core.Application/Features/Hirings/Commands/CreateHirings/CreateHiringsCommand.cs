using AutoMapper;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Hirings.Commands.CreateHirings
{
    public class CreateHiringsCommand:IRequest<int>
    {
        public int LocationId { get; set; }
        public Location location { get; set; }
        public int AssistentId { get; set; }
        public Assistant assistant { get; set; }
        public string UserId { get; set; }
        public string PayType { get; set; }
        public string Total { get; set; }
        public string Meter { get; set; }
    }
    public class CreateHiringsCommandHandler : IRequestHandler<CreateHiringsCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IHiringRepository _hiringRepository;

        public CreateHiringsCommandHandler(IMapper mapper, IHiringRepository hiringRepository)
        {
            _mapper = mapper;
            _hiringRepository = hiringRepository;
        }
        public async Task<int> Handle(CreateHiringsCommand request, CancellationToken cancellationToken)
        {
            var hiring = _mapper.Map<Hiring>(request);
            hiring = await _hiringRepository.AddAsync(hiring);
            return hiring.Id;
        }
    }
}
