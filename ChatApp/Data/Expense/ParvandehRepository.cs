using ChatApp.Models.Expense;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Data.Expense
{
    public class ParvandehRepository : Repository<Parvandeh>, IParvandehRepository
    {
        public ParvandehRepository(IMongoClient client) : base(client)
        {
        }


       public async Task<List<Parvandeh>> Query(string query)
        {
            var result = Collection.AsQueryable<Parvandeh>()
                .Where(x => x.Title.Contains(query) || x.Shomareh == query)
                .ToList();

            return result;
        }
    }
}