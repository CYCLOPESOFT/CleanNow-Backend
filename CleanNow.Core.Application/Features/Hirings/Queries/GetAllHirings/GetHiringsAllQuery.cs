using AutoMapper;
using CleanNow.Core.Application.Dto.Hirings;
using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Hirings.Queries.GetAllHirings
{
    public class GetHiringsAllQuery:IRequest<IEnumerable<GetHiringsDto>>
    {
    }
    public class GetHiringsAllQueryHandler : IRequestHandler<GetHiringsAllQuery, IEnumerable<GetHiringsDto>>
    {
        private readonly IMapper _mapper;
        private readonly IHiringRepository _hiringRepository;

        public GetHiringsAllQueryHandler(IMapper mapper, IHiringRepository hiringRepository)
        {
            _mapper = mapper;
            _hiringRepository = hiringRepository;
        }

        public async Task<IEnumerable<GetHiringsDto>> Handle(GetHiringsAllQuery request, CancellationToken cancellationToken)
        {
            var hirings = await GetAllAsync();
            if (hirings == null || hirings.Count() == 0) throw new Exception("Hirings not found");
            return hirings;
        }
        private async Task<IEnumerable<GetHiringsDto>> GetAllAsync()
        {
            var hirings = await _hiringRepository.GetAllIncludeAsync(new List<string> { "assistant", "location" });
            return hirings.Select(h=>new GetHiringsDto
            {
                Address = h.location.Address,
                AssistentId = h.AssistentId,
                CreateDate = h.CreatedDate,
                Id = h.Id,
                LocationId = h.LocationId,
                Meter = h.Meter,
                NameAssistant = h.assistant.Name,
                PayType = h.PayType,
                Total = h.Total,
                UserId = h.UserId
            }).ToList();
        }
    }
}
