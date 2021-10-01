using ChatApp.Data.Expense;
using ChatApp.Models.Expense;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IRepository<Received> _receiveRepository;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Parvandeh> _parvandehRepository;

        public IConfiguration Configuration { get; }

        public ValuesController(IRepository<Received> receiveRepository, 
            IRepository<Client> clientRepository, 
            IRepository<Parvandeh> parvandehRepository,
            IConfiguration configuration)
        {
            _receiveRepository = receiveRepository;
            _clientRepository = clientRepository;
            _parvandehRepository = parvandehRepository;
            Configuration = configuration;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _receiveRepository.GetAllAsync();
            return Ok(result);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var mongodbCn = Configuration.GetValue<string>("MONGODB_CN");
            return mongodbCn;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string val)
        {
            var clientId = ObjectId.GenerateNewId();
            var client = new Client()
            {
                Id = clientId.ToString(),
                Name = "Abas",
                NationalCode = "007"
            };

            var parvandehId = ObjectId.GenerateNewId();
            var parvandeh = new Parvandeh()
            {
                Id = parvandehId.ToString(),
                Baygani = "bb1",
                Shomareh = "16*16"
            };

            var received = new Received()
            {

                AmountReceived = 12,
                Babat = "dadkhast",
                Bank = "sepah",
                Client = client,
                Parvandeh = parvandeh
            };
            var result = await _receiveRepository.CreateAsync(received);
            var result2 = await _clientRepository.CreateAsync(client);
            var result3 = await _parvandehRepository.CreateAsync(parvandeh);
            return Ok(result);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}