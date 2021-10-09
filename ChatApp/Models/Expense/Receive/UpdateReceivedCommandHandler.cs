using AutoMapper;
using ChatApp.Data.Expense;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Models.Expense.Receive
{
    public class UpdateReceivedCommandHandler : IRequestHandler<UpdateReceivedCommand>
    {
        private readonly IReceivedRepository _receivedRepository;
        private readonly IMapper _mapper;

        public UpdateReceivedCommandHandler(IReceivedRepository receivedRepository,
            IMapper mapper)
        {
            _receivedRepository = receivedRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateReceivedCommand request, CancellationToken cancellationToken)
        {
            request.Client.UserId = request.UserId;
            request.Parvandeh.UserId = request.UserId;
            var model = _mapper.Map<UpdateReceivedCommand, Received>(request);
            await _receivedRepository.UpdateReceived(model);

            return Unit.Value;
        }
    }
}