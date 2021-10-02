using ChatApp.Data.Expense;
using ChatApp.Models.Expense;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ChatApp.Data;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceivedController : ControllerBase
    {
        private readonly IReceivedRepository _receivedRepository;
        private readonly IMapper _mapper;

        public ReceivedController(IReceivedRepository receivedRepository,
            IMapper mapper)
        {
            _receivedRepository = receivedRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 10,
            string sortColumn = null, string sortOrder = null, string filterColumn = null,
            string filterQuery = null)
        {
            var result = await _receivedRepository.GetAllAsync(pageIndex, pageSize, sortColumn,
                   sortOrder, filterColumn, filterQuery);

            var response = _mapper.Map<ApiResult<Received>, ApiResult<ReceivedListVm>>(result);
            
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _receivedRepository.GetByIdAsync(id);
            var response = _mapper.Map<Received, ReceivedListVm>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReceivedListVm received)
        {
            try
            {
                var model = _mapper.Map<ReceivedListVm, Received>(received);
                await _receivedRepository.AddNewReceived(model);
                return Ok();
            }
            catch (Exception exception)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, exception);
                return result;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ReceivedListVm received)
        {
            var model = _mapper.Map<ReceivedListVm, Received>(received);
            await _receivedRepository.UpdateAsync(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
