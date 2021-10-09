using AutoMapper;
using ChatApp.Data.Expense;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Models.Expense.Receive
{
    public class CreateReceivedCommandHandler : IRequestHandler<CreateReceivedCommand>
    {
        private readonly IReceivedRepository _receivedRepository;
        private readonly IMapper _mapper;

        public CreateReceivedCommandHandler(IReceivedRepository receivedRepository,
            IMapper mapper)
        {
            _receivedRepository = receivedRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateReceivedCommand request, CancellationToken cancellationToken)
        {
            request.Client.UserId = request.UserId;
            request.Parvandeh.UserId = request.UserId;
            if (!string.IsNullOrEmpty(request.Cheque.Shomareh))
            {
                request.Cheque.UserId = request.UserId;
            }
            var model = _mapper.Map<CreateReceivedCommand, Received>(request);
            await _receivedRepository.AddNewReceived(model);

            return Unit.Value;
        }
    }
}