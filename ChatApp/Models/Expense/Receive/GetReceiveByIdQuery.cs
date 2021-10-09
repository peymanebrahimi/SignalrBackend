using MediatR;

namespace ChatApp.Models.Expense.Receive
{
    public class GetReceiveByIdQuery:IRequest<Received>
    {
        public string Id { get; set; }
        public string UserId { get; set; }
    }
}