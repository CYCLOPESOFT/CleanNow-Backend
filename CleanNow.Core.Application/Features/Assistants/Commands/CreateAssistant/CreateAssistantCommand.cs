using AutoMapper;
using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Assistants.Commands.CreateAssistant
{
    public class CreateAssistantCommand:IRequest<int>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string AboutMe { get; set; }
        public int Experience { get; set; }
        public bool IsVerify { get; set; }
        public double Price { get; set; }
        public int Availability { get; set; }
        public string Image { get; set; }
    }
    public class CreateAssistantCommandHandler : IRequestHandler<CreateAssistantCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IAssistantRepository _assistantRepository;

        public CreateAssistantCommandHandler(IMapper mapper, IAssistantRepository assistantRepository)
        {
            _mapper = mapper;
            _assistantRepository = assistantRepository;
        }

        public async Task<int> Handle(CreateAssistantCommand request, CancellationToken cancellationToken)
        {
            var assistant = _mapper.Map<Assistant>(request);
            assistant = await _assistantRepository.AddAsync(assistant);
            return assistant.Id;
        }
    }
}
