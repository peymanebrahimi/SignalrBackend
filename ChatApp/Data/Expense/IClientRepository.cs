using ChatApp.Models.Expense;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Data.Expense
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<List<Client>> Query(string query);
    }
}