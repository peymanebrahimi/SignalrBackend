using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Data.Expense;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 10,
            string sortColumn = null, string sortOrder = null, string filterColumn = null,
            string filterQuery = null)
        {
            var result = await _clientRepository.GetAllAsync(pageIndex, pageSize, sortColumn,
                sortOrder, filterColumn, filterQuery);

            return Ok(result);
        }


        [HttpGet("{query}")]
        public async Task<IActionResult> Query(string query)
        {
            var result = await _clientRepository.Query(query);

            return Ok(result);
        }
    }
}
