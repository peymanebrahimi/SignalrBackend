using ChatApp.Models.Expense;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Data.Expense
{
    public interface IParvandehRepository : IRepository<Parvandeh>
    {
        Task<List<Parvandeh>> Query(string query);
    }
}