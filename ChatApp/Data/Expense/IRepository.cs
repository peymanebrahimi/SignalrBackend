using System.Threading.Tasks;

namespace ChatApp.Data.Expense
{
    public interface IRepository<T> where T : IHaveId
    {
        Task<ApiResult<T>> GetAllAsync(int pageIndex = 0, int pageSize = 10,
            string sortColumn = null, string sortOrder = null, string filterColumn = null,
            string filterQuery = null);

        Task<T> GetByIdAsync(string id);
        Task<T> CreateAsync(T t);
        Task UpdateAsync(string id, T t);
        Task DeleteAsync(string id);
    }
   
}