using ChatApp.Models.Expense;
using MongoDB.Driver;

namespace ChatApp.Data.Expense
{
    public class ParvandehRepository : Repository<Parvandeh>, IParvandehRepository
    {
        public ParvandehRepository(IMongoClient client) : base(client)
        {
        }
    }
}