using ChatApp.Data.Expense;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParvandehController : ControllerBase
    {
        private readonly IParvandehRepository _repository;

        public ParvandehController(IParvandehRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 10,
            string sortColumn = null, string sortOrder = null, string filterColumn = null,
            string filterQuery = null)
        {
            var result = await _repository.GetAllAsync(pageIndex, pageSize, sortColumn,
                sortOrder, filterColumn, filterQuery);

            return Ok(result);
        }


        [HttpGet("{query}")]
        public async Task<IActionResult> Query(string query)
        {
            var result = await _repository.Query(query);

            return Ok(result);
        }
    }
}