using AutoMapper;
using CleanNow.Core.Application.Dto.Assistans;
using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Assistants.Queries.GetByIdAssitants
{
    public class GetAssistansByIdQuery:IRequest<AssistansGetDto>
    {
        public int? Id { get; set; }
    }

    public class GetAssistansByIdQueryHandler : IRequestHandler<GetAssistansByIdQuery, AssistansGetDto>
    {
        private readonly IMapper _mapper;
        private readonly IAssistantRepository _assistantRepository;
        public GetAssistansByIdQueryHandler(IMapper mapper, IAssistantRepository assistantRepository)
        {
            _mapper = mapper;
            _assistantRepository = assistantRepository;
        }
        public async Task<AssistansGetDto> Handle(GetAssistansByIdQuery request, CancellationToken cancellationToken)
        {
            var param = _mapper.Map<GetAssistansByIdParameter>(request);
            var assistant = await AssistansGetById(param);
            return assistant;
        }

        private async Task<AssistansGetDto> AssistansGetById (GetAssistansByIdParameter parameter)
        {
            var assitants = await _assistantRepository.GetAllIncludeAsync(new List<string> { "likes" });
            var assitant = assitants.FirstOrDefault(w => w.Id == parameter.Id);
            if (assitant == null) throw new Exception($"Assistant Not Found.");
            var assistantDto = _mapper.Map<AssistansGetDto>(assitant);
            assistantDto.CountLikes = assitant.likes.Count;
            return assistantDto;

        }
    }
}
