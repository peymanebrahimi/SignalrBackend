using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Data.Expense;
using ChatApp.Services;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserService _userService;

        public ClientController(IClientRepository clientRepository,
            IUserService userService)
        {
            _clientRepository = clientRepository;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 10,
            string sortColumn = null, string sortOrder = null, string filterColumn = null,
            string filterQuery = null)
        {
            var userId = _userService.GetUserId(User);
            var result = await _clientRepository.GetAllAsync(userId,pageIndex, pageSize, sortColumn,
                sortOrder, filterColumn, filterQuery);

            return Ok(result);
        }


        [HttpGet("{query}")]
        public async Task<IActionResult> Query(string query)
        {
            var userId = _userService.GetUserId(User);
            var result = await _clientRepository.Query(query, userId);

            return Ok(result);
        }
    }
}
