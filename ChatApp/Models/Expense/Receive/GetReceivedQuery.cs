using ChatApp.Data;
using MediatR;

namespace ChatApp.Models.Expense.Receive
{
    public class GetReceivedQuery : IRequest<ApiResult<Received>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string FilterColumn { get; set; }
        public string FilterQuery { get; set; }
        public string UserId { get; set; }
    }
}