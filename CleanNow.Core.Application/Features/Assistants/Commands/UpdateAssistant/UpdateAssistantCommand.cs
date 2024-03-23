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

namespace CleanNow.Core.Application.Features.Assistants.Commands.UpdateAssistant
{
    public class UpdateAssistantCommand : IRequest<UpdateAssistantResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string AboutMe { get; set; }
        public int Experience { get; set; }
        public bool IsVerify { get; set; }
        public double Price { get; set; }
        public int Availability { get; set; }
    }
    public class CUpdateAssistantCommandHandler : IRequestHandler<UpdateAssistantCommand, UpdateAssistantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAssistantRepository _assistantRepository;
        public CUpdateAssistantCommandHandler(IMapper mapper, IAssistantRepository assistantRepository)
        {
            _assistantRepository = assistantRepository;
            _mapper = mapper;
        }
        public async Task<UpdateAssistantResponse> Handle(UpdateAssistantCommand command, CancellationToken cancellationToken)
        {
            var assistant = await _assistantRepository.GetAsync(command.Id);
            if (assistant == null) throw new Exception($"Assistant not fount with id {command.Id}");
            assistant = _mapper.Map<Assistant>(command);
            await _assistantRepository.UpdateAsync(assistant, assistant.Id);

            return _mapper.Map<UpdateAssistantResponse>(command);
        }
    }
}
