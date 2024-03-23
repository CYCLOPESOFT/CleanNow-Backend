using AutoMapper;
using CleanNow.Core.Application.Dto.Assistans;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Assistants.Queries.GetAllAssistants
{
    public class GetAllAssistantQuery:IRequest<IEnumerable<AssistansGetDto>>
    {
    }

    public class GetAllAssistantQueryHandle : IRequestHandler<GetAllAssistantQuery, IEnumerable<AssistansGetDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAssistantRepository _assistantRepository;
        public GetAllAssistantQueryHandle(IMapper mapper, IAssistantRepository assistantRepository)
        {
            _mapper = mapper;
            _assistantRepository = assistantRepository;
        }
        public async Task<IEnumerable<AssistansGetDto>> Handle(GetAllAssistantQuery request, CancellationToken cancellationToken)
        {
            var assistansList = await GetAllAssistans();
            if (assistansList == null || assistansList.Count() == 0) throw new Exception("Assistants not found");

            return assistansList;
        }
        private async Task<IEnumerable<AssistansGetDto>> GetAllAssistans()
        {
            var assistants = await _assistantRepository.GetAllIncludeAsync(new List<string> { "likes" });


            return assistants.Select(a => new AssistansGetDto
            {
                AboutMe = a.AboutMe,
                Age = a.Age,
                Availability = a.Availability,
                CountLikes = a.likes.Count,
                Experience = a.Experience,
                Id = a.Id,
                IsVerify = a.IsVerify,
                LastName = a.LastName,
                Location = a.Location,
                Name = a.Name,
                Price = a.Price
            }).ToList();
        }

    }
}
