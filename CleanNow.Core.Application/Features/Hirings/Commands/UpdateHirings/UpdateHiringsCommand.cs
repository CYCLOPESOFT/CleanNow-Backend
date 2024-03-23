using AutoMapper;
using CleanNow.Core.Application.Dto.Assistans;
using CleanNow.Core.Application.Dto.Hirings;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Hirings.Commands.UpdateHirings
{
    public class UpdateHiringsCommand:IRequest<HiringsUpdateResponse>
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public Location location { get; set; }
        public int AssistentId { get; set; }
        public Assistant assistant { get; set; }
        public string UserId { get; set; }
        public string PayType { get; set; }
        public string Total { get; set; }
        public string Meter { get; set; }
    }
    public class UpdateHiringsCommandHandler:IRequestHandler<UpdateHiringsCommand, HiringsUpdateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHiringRepository _hiringRepository;

        public UpdateHiringsCommandHandler(IMapper mapper, IHiringRepository hiringRepository)
        {
            _mapper = mapper;
            _hiringRepository = hiringRepository;
        }

        public async Task<HiringsUpdateResponse> Handle(UpdateHiringsCommand request, CancellationToken cancellationToken)
        {
            var hiring = await _hiringRepository.GetAsync(request.Id);
            if (hiring == null) throw new Exception("Hiring not found for update");
            await _hiringRepository.UpdateAsync(hiring, request.Id);
            return _mapper.Map<HiringsUpdateResponse>(hiring);
        }
    }

}
