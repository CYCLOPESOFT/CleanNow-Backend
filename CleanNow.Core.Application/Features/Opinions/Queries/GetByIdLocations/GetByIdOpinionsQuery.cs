using AutoMapper;
using CleanNow.Core.Application.Dto.Opinions;
using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Opinions.Queries.GetByIdLocations
{
    public class GetByIdOpinionsQuery:IRequest<GetOpinionsResponse>
    {
        public int Id { get; set; }
    }
    public class GetByIdOpinionsQueryHandler : IRequestHandler<GetByIdOpinionsQuery, GetOpinionsResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOpinionsRepository _opinionRepository;

        public GetByIdOpinionsQueryHandler(IMapper mapper, IOpinionsRepository opinionRepository)
        {
            _mapper = mapper;
            _opinionRepository = opinionRepository;
        }
        public async Task<GetOpinionsResponse> Handle(GetByIdOpinionsQuery request, CancellationToken cancellationToken)
        {
            var opinion = _mapper.Map<GetByIdOpinionsParameter>(request);
            var found =  await GetOpinionsById(opinion);
            return found;
        }
        private async Task<GetOpinionsResponse> GetOpinionsById (GetByIdOpinionsParameter parameter)
        {
            var opinions = await _opinionRepository.GetAllIncludeAsync(new List<string> { "assistant" });
            var opinion = opinions.FirstOrDefault(o => o.Id == parameter.Id);
            if (opinion == null) throw new Exception("Opinion not found");
            var map = _mapper.Map<GetOpinionsResponse>(opinion);
            map.AssistantName = opinion.assistant.Name;
            return map;
        }
    }
}

