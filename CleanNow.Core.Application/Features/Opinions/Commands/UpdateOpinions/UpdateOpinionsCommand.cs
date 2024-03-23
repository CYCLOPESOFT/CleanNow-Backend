using AutoMapper;
using CleanNow.Core.Application.Dto.Opinions;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Opinions.Commands.UpdateOpinions
{
    public class UpdateOpinionsCommand:IRequest<UpdateOpinionsResponse>
    {
        public int Id { get; set; }
        public int AssistantId { get; set; }
        public string? Description { get; set; }
        public int Start { get; set; }
        public string ValuerName { get; set; }
    }
    public class UpdateOpinionsCommandHandler : IRequestHandler<UpdateOpinionsCommand, UpdateOpinionsResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOpinionsRepository _opinionRepository;

        public UpdateOpinionsCommandHandler(IMapper mapper, IOpinionsRepository opinionRepository)
        {
            _mapper = mapper;
            _opinionRepository = opinionRepository;
        }
        public async Task<UpdateOpinionsResponse> Handle(UpdateOpinionsCommand request, CancellationToken cancellationToken)
        {
            var opinion = await _opinionRepository.GetAsync(request.Id);
            if (opinion == null) throw new Exception("Opinions not found");
            var mapOption = _mapper.Map<UpdateOpinionsResponse>(opinion);
            await _opinionRepository.UpdateAsync(opinion, opinion.Id);
            return mapOption;
        }
    }
}
