using ChatApp.Data.Expense;
using ChatApp.Models.Expense;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceivedController : ControllerBase
    {
        private readonly IReceivedRepository _receivedRepository;

        public ReceivedController(IReceivedRepository receivedRepository)
        {
            _receivedRepository = receivedRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 10,
            string sortColumn = null, string sortOrder = null, string filterColumn = null,
            string filterQuery = null)
        {
            var result = await _receivedRepository.GetAllAsync(pageIndex, pageSize, sortColumn,
                   sortOrder, filterColumn, filterQuery);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _receivedRepository.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Received received)
        {
            try
            {
                await _receivedRepository.AddNewReceived(received);
                return Ok();
            }
            catch (Exception exception)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, exception);
                return result;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Received received)
        {
            await _receivedRepository.UpdateAsync(id, received);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
