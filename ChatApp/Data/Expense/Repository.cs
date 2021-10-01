using System.Threading.Tasks;
using MongoDB.Driver;

namespace ChatApp.Data.Expense
{
    public class Repository<T> : IRepository<T> where T : IHaveId
    {
        public  IMongoCollection<T> Collection { get; }
        public Repository(IMongoClient client)
        {
            var database = client.GetDatabase("Expense");
            Collection = database.GetCollection<T>(typeof(T).Name);
        }

        public async Task<ApiResult<T>> GetAllAsync(int pageIndex = 0, int pageSize = 10, string sortColumn = null, string sortOrder = null,
            string filterColumn = null, string filterQuery = null)
        {
            var result = await ApiResult<T>.CreateAsync(Collection, pageIndex, pageSize,
                sortColumn, sortOrder, filterColumn, filterQuery);

            return result;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await Collection.Find<T>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<T> CreateAsync(T t)
        {
            await Collection.InsertOneAsync(t);
            return t;
        }

        public async Task UpdateAsync(string id, T t)
        {
            await Collection.ReplaceOneAsync(s => s.Id == id, t);
        }

        public async Task DeleteAsync(string id)
        {
            await Collection.DeleteOneAsync(s => s.Id == id);
        }
    }
}