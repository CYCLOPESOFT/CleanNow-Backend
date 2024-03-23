using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Assistants.Commands.DeleteAssistant
{
    public class DeleteAssistantCommand:IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteAssistantCommandHandler : IRequestHandler<DeleteAssistantCommand, int>
    {
        private readonly IAssistantRepository _assistantRepository;

        public DeleteAssistantCommandHandler(IAssistantRepository assistantRepository)
        {
            _assistantRepository = assistantRepository;
        }

        public async Task<int> Handle(DeleteAssistantCommand request, CancellationToken cancellationToken)
        {
            var assistant = await _assistantRepository.GetAsync(request.Id);
            if (assistant == null) throw new Exception("Not Found Assistant");
            await _assistantRepository.DeleteAsync(assistant);
            return assistant.Id;
        }
    }
}
