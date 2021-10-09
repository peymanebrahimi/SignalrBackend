using ChatApp.Models.Expense.Receive;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace ChatApp.Data.Expense
{
    public class ReceivedRepository : Repository<Received>, IReceivedRepository
    {
        private readonly IClientRepository _clientRepository;
        private readonly IChequeRepository _chequeRepository;
        private readonly IParvandehRepository _parvandehRepository;

        public ReceivedRepository(IMongoClient client,
            IClientRepository clientRepository,
            IChequeRepository chequeRepository,
            IParvandehRepository parvandehRepository) : base(client)
        {
            _clientRepository = clientRepository;
            _chequeRepository = chequeRepository;
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
            if (string.IsNullOrEmpty(received.Cheque.Shomareh))
            {
                //ignore cheque
                received.Cheque = null;
            }
            else
            {
                if (string.IsNullOrEmpty(received.Cheque.Id))
                {

                    received.Cheque.Id = ObjectId.GenerateNewId().ToString();
                    received.Cheque.UserId = received.UserId;
                    await _chequeRepository.CreateAsync(received.Cheque);
                }
                else
                {
                    received.Cheque.UserId = received.UserId;
                    await _chequeRepository.UpdateAsync(received.Cheque.Id, received.Cheque);
                }
            }
            var result = await this.CreateAsync(received);

        }

        public async Task UpdateReceived(Received received)
        {
            if (string.IsNullOrEmpty(received.Client.Id))
            {
                // add new client in its collection
                received.Client.Id = ObjectId.GenerateNewId().ToString();
                await _clientRepository.CreateAsync(received.Client);
            }
            else
            {
                await _clientRepository.UpdateAsync(received.Client.Id, received.Client);
            }
            if (string.IsNullOrEmpty(received.Parvandeh.Id))
            {

                received.Parvandeh.Id = ObjectId.GenerateNewId().ToString();
                await _parvandehRepository.CreateAsync(received.Parvandeh);
            }
            else
            {
                await _parvandehRepository.UpdateAsync(received.Parvandeh.Id, received.Parvandeh);
            }

            if (string.IsNullOrEmpty(received.Cheque.Shomareh))
            {
                //ignore cheque
                received.Cheque = null;
            }
            else
            {
                if (string.IsNullOrEmpty(received.Cheque.Id))
                {

                    received.Cheque.Id = ObjectId.GenerateNewId().ToString();
                    received.Cheque.UserId = received.UserId;
                    await _chequeRepository.CreateAsync(received.Cheque);
                }
                else
                {
                    received.Cheque.UserId = received.UserId;
                    await _chequeRepository.UpdateAsync(received.Cheque.Id, received.Cheque);
                }
            }
           
            await this.UpdateAsync(received.Id, received);

        }
    }
}