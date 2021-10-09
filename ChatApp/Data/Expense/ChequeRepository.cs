using ChatApp.Models.Expense;
using MongoDB.Driver;

namespace ChatApp.Data.Expense
{
    public class ChequeRepository : Repository<Cheque>, IChequeRepository
    {
        public ChequeRepository(IMongoClient client) : base(client)
        {
        }
    }
}