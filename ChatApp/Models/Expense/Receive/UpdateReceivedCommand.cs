using MediatR;
using System;

namespace ChatApp.Models.Expense.Receive
{
    public class UpdateReceivedCommand : IRequest
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public decimal AmountReceived { get; set; }
        public string Babat { get; set; }
        public Client Client { get; set; }
        public Parvandeh Parvandeh { get; set; }
        public DateTime DateReceived { get; set; }
        public string Bank { get; set; }
        public Cheque Cheque { get; set; }
    }
}