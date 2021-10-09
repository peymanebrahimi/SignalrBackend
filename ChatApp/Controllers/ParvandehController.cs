using ChatApp.Data.Expense;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChatApp.Services;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParvandehController : ControllerBase
    {
        private readonly IParvandehRepository _repository;
        private readonly IUserService _userService;

        public ParvandehController(IParvandehRepository repository,
            IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 10,
            string sortColumn = null, string sortOrder = null, string filterColumn = null,
            string filterQuery = null)
        {
            var userId = _userService.GetUserId(User);
            var result = await _repository.GetAllAsync(userId, pageIndex, pageSize, sortColumn,
                sortOrder, filterColumn, filterQuery);

            return Ok(result);
        }


        [HttpGet("{query}")]
        public async Task<IActionResult> Query(string query)
        {
            var userId = _userService.GetUserId(User);
            var result = await _repository.Query(query, userId);

            return Ok(result);
        }
    }
}