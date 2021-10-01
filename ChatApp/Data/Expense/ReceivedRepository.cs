using System.Threading.Tasks;
using ChatApp.Models.Expense;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ChatApp.Data.Expense
{
    public class ReceivedRepository : Repository<Received>, IReceivedRepository
    {
        //private readonly IReceivedRepository _receiveRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IParvandehRepository _parvandehRepository;

        public ReceivedRepository(IMongoClient client,
            //IReceivedRepository receiveRepository,
            IClientRepository clientRepository,
            IParvandehRepository parvandehRepository) : base(client)
        {
            //_receiveRepository = receiveRepository;
            _clientRepository = clientRepository;
            _parvandehRepository = parvandehRepository;
        }


        public async Task AddNewReceived(Received received)
        {
            if (string.IsNullOrEmpty(received.Client.Id))
            {
                // add new client in its collection
                received.Client.Id = ObjectId.GenerateNewId().ToString();
                await _clientRepository.CreateAsync(received.Client);
            }
            if (string.IsNullOrEmpty(received.Parvandeh.Id))
            {
                received.Parvandeh.Id = ObjectId.GenerateNewId().ToString();
                await _parvandehRepository.CreateAsync(received.Parvandeh);
            }
            var result = await this.CreateAsync(received);

        }

    }
}