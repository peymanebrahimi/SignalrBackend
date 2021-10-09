using AutoMapper;
using ChatApp.Data.Expense;
using ChatApp.Models.Expense.Receive;
using ChatApp.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReceivedController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<ReceivedController> _logger;

        public ReceivedController(IMediator mediator,
            IUserService userService,
            IMapper mapper, ILogger<ReceivedController> logger)
        {
            _mediator = mediator;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 10,
            string sortColumn = null, string sortOrder = null, string filterColumn = null,
            string filterQuery = null)
        {
            try
            {
                var userId = _userService.GetUserId(User);

                var result = await _mediator.Send(new GetReceivedQuery()
                {
                    UserId = userId,
                    PageSize = pageSize,
                    PageIndex = pageIndex,
                    SortColumn = sortColumn,
                    SortOrder = sortOrder,
                    FilterQuery = filterQuery,
                    FilterColumn = filterColumn
                });

                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                var result = StatusCode(StatusCodes.Status500InternalServerError, exception);
                return result;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Received), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string id)
        {
            var userId = _userService.GetUserId(User);

            var result = await _mediator.Send(new GetReceiveByIdQuery
            {
                UserId = userId,
                Id = id
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Received received)
        {
            try
            {
                received.UserId = _userService.GetUserId(User);
                var command = _mapper.Map<Received, CreateReceivedCommand>(received);
                await _mediator.Send(command);
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
            if (id != received.Id)
            {
                return BadRequest("ids not match!");
            }

            received.UserId = _userService.GetUserId(User);
            var command = _mapper.Map<Received, UpdateReceivedCommand>(received);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
