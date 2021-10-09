using AutoMapper;
using ChatApp.Data.Expense;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Models.Expense.Receive
{
    public class GetReceiveByIdQueryHandler : IRequestHandler<GetReceiveByIdQuery, Received>
    {
        private readonly IReceivedRepository _receivedRepository;
        private readonly IMapper _mapper;

        public GetReceiveByIdQueryHandler(IReceivedRepository receivedRepository,
            IMapper mapper)
        {
            _receivedRepository = receivedRepository;
            _mapper = mapper;
        }

        public async Task<Received> Handle(GetReceiveByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _receivedRepository.GetByIdAsync(request.Id, request.UserId);

            return result;
        }
    }
}