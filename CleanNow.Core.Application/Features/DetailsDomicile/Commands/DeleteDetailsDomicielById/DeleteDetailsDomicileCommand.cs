using CleanNow.Core.Application.Interfaces.Repositories;
using MediatR;

namespace CleanNow.Core.Application.Features.DetailsDomiciles.Commands.DeleteDetailsDomicielById
{
    public class DeleteDetailsDomicileCommand:IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteDetailsDomicileCommandHandler : IRequestHandler<DeleteDetailsDomicileCommand, int>
    {
        private readonly IDetailsDomicileRepository _detailsRepository;
        public DeleteDetailsDomicileCommandHandler(IDetailsDomicileRepository detailsRepository)
        {
            _detailsRepository = detailsRepository;
        }
        public async Task<int> Handle(DeleteDetailsDomicileCommand command, CancellationToken cancellationToken)
        {
            var detail = await _detailsRepository.GetAsync(command.Id);
            if (detail == null) throw new Exception("Detail not found");
            await _detailsRepository.DeleteAsync(detail);
            return detail.Id;
        }
    }
}
