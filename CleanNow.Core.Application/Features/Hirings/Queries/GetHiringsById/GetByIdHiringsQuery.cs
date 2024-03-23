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

namespace CleanNow.Core.Application.Features.Hirings.Queries.GetHiringsById
{
    public class GetByIdHiringsQuery:IRequest<GetHiringsDto>
    {
        public int Id { get; set; }
    }
    public class GetByIdHiringsQueryHandler : IRequestHandler<GetByIdHiringsQuery, GetHiringsDto>
    {
        private readonly IMapper _mapper;
        private readonly IHiringRepository _hiringRepository;

        public GetByIdHiringsQueryHandler(IMapper mapper, IHiringRepository hiringRepository)
        {
            _mapper = mapper;
            _hiringRepository = hiringRepository;
        }

        public async Task<GetHiringsDto> Handle(GetByIdHiringsQuery request, CancellationToken cancellationToken)
        {
            return await GetById(_mapper.Map<GetByIdHiringsParameter>(request));
        }
        private async Task<GetHiringsDto> GetById(GetByIdHiringsParameter parameter)
        {
            var hirings = await _hiringRepository.GetAllIncludeAsync(new List<string> { "assistant", "location" });
            var hiring = hirings.FirstOrDefault(h=>h.Id == parameter.Id);
            if (hiring == null) throw new Exception("Hiring not found");
            var hiringDto = _mapper.Map<GetHiringsDto>(hiring);
            hiringDto.NameAssistant = hiring.assistant.Name;
            hiringDto.Address = hiring.location.Address;
            return hiringDto;
        }
    }
}
