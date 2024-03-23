using AutoMapper;
using CleanNow.Core.Application.Dto.Opinions;
using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Opinions.Queries.GetAllLocations
{
    public class GetAllOpinionsQuery:IRequest<IEnumerable<GetOpinionsResponse>>
    {
    }
    public class GetAllOpinionsQueryHandler : IRequestHandler<GetAllOpinionsQuery, IEnumerable<GetOpinionsResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IOpinionsRepository _opinionRepository;

        public GetAllOpinionsQueryHandler(IMapper mapper, IOpinionsRepository opinionRepository)
        {
            _mapper = mapper;
            _opinionRepository = opinionRepository;
        }
        public async Task<IEnumerable<GetOpinionsResponse>> Handle(GetAllOpinionsQuery request, CancellationToken cancellationToken)
        {
            var opinions = await GetAllOpinions();
            if (opinions == null || opinions.Count() == 0) throw new Exception("Opinions not found");
            return opinions;
        }

        private async Task<IEnumerable<GetOpinionsResponse>> GetAllOpinions()
        {
            var opinions = await _opinionRepository.GetAllIncludeAsync(new List<string> { "assistant" });
            return opinions.Select(o => new GetOpinionsResponse
            {
                AssistantId = o.AssistantId,
                AssistantName = o.assistant.Name,
                Description = o.Description,
                Id = o.Id,
                Start = o.Start,
                ValuerName= o.ValuerName,

            }).ToList();
        }
    }
}
