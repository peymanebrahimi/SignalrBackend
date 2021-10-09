using System.Threading.Tasks;

namespace ChatApp.Data.Expense
{
    public interface IRepository<T> where T : IHaveId, IHaveUserId
    {
        Task<ApiResult<T>> GetAllAsync(string userId, int pageIndex = 0, int pageSize = 10,
            string sortColumn = null, string sortOrder = null, string filterColumn = null,
            string filterQuery = null);

        Task<T> GetByIdAsync(string id, string userId);
        Task<T> CreateAsync(T t);
        Task UpdateAsync(string id, T t);
        Task DeleteAsync(string id);
    }

}