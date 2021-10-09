using ChatApp.Models.Expense.Receive;
using System.Threading.Tasks;

namespace ChatApp.Data.Expense
{
    public interface IReceivedRepository : IRepository<Received>
    {
        Task AddNewReceived(Received received);
        Task UpdateReceived(Received received);
    }
}