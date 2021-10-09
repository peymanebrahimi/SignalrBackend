using AutoMapper;
using ChatApp.Data;
using ChatApp.Data.Expense;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Models.Expense.Receive
{
    public class GetReceivedQueryHandler : IRequestHandler<GetReceivedQuery, ApiResult<Received>>
    {
        private readonly IReceivedRepository _receivedRepository;
        private readonly IMapper _mapper;

        public GetReceivedQueryHandler(IReceivedRepository receivedRepository,
            IMapper mapper)
        {
            _receivedRepository = receivedRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<Received>> Handle(GetReceivedQuery request, CancellationToken cancellationToken)
        {
            var result = await _receivedRepository.GetAllAsync(request.UserId, request.PageIndex, request.PageSize, request.SortColumn,
                request.SortOrder, request.FilterColumn, request.FilterQuery);

            return result;
        }
    }
}