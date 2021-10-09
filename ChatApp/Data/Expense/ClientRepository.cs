using ChatApp.Models.Expense;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Data.Expense
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(IMongoClient client) : base(client)
        {
        }

        public async Task<List<Client>> Query(string query, string userId)
        {
            var result = Collection.AsQueryable<Client>()
                .Where(x => x.UserId == userId)
                .Where(x => x.Name.Contains(query) || x.NationalCode == query)
                .ToList();

            return result;
        }
    }
}