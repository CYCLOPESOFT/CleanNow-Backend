using AutoMapper;
using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Features.Hirings.Commands.DeleteHirings
{
    public class DeleteHiringsCommand:IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteHiringsCommandHandler : IRequestHandler<DeleteHiringsCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IHiringRepository _hiringRepository;

        public DeleteHiringsCommandHandler(IMapper mapper, IHiringRepository hiringRepository)
        {
            _mapper = mapper;
            _hiringRepository = hiringRepository;
        }

        public async Task<int> Handle(DeleteHiringsCommand request, CancellationToken cancellationToken)
        {
            var hiring = await _hiringRepository.GetAsync(request.Id);
            if (hiring == null) throw new Exception("Hiring not found");
            await _hiringRepository.DeleteAsync(hiring);
            return hiring.Id;
        }
    }
}
