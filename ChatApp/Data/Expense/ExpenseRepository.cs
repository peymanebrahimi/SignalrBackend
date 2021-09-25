using ChatApp.Models.Expense;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace ChatApp.Data.Expense
{
    public class ExpenseRepository : IRepository<Received>
    {
        private readonly IMongoCollection<Received> _receivedCollection;
        public ExpenseRepository(IMongoClient client)
        {
            var database = client.GetDatabase("Expense");
            _receivedCollection = database.GetCollection<Received>(nameof(Received));
        }

        public async Task<ApiResult<Received>> GetAllAsync(int pageIndex = 0, int pageSize = 10,
            string sortColumn = null, string sortOrder = null, string filterColumn = null,
            string filterQuery = null)
        {
            var result = await ApiResult<Received>.CreateAsync(_receivedCollection, pageIndex, pageSize,
                sortColumn, sortOrder, filterColumn, filterQuery);

            return result;
        }

        public async Task<Received> GetByIdAsync(string id)
        {
            return await _receivedCollection.Find<Received>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Received> CreateAsync(Received received)
        {
            await _receivedCollection.InsertOneAsync(received);
            return received;
        }

        public async Task UpdateAsync(string id, Received received)
        {
            await _receivedCollection.ReplaceOneAsync(s => s.Id == id, received);
        }

        public async Task DeleteAsync(string id)
        {
            await _receivedCollection.DeleteOneAsync(s => s.Id == id);
        }
    }
}
