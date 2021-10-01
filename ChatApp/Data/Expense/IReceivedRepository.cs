using ChatApp.Models.Expense;
using System.Threading.Tasks;

namespace ChatApp.Data.Expense
{
    public interface IReceivedRepository : IRepository<Received>
    {
        Task AddNewReceived(Received received);
    }
}