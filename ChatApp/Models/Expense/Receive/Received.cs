using ChatApp.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ChatApp.Models.Expense.Receive
{
    public class Received : IHaveId, IHaveUserId
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
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

    //public class ReceivedListVm
    //{
    //    public string Id { get; set; }
    //    public string UserId { get; set; }
    //    public decimal AmountReceived { get; set; }
    //    public string Babat { get; set; }
    //    public MiniClient Client { get; set; }
    //    public MiniParvandeh Parvandeh { get; set; }
    //    public DateTime DateReceived { get; set; }
    //    public string Bank { get; set; }
    //}

    //public class MiniClient
    //{
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //    public string NationalCode { get; set; }
    //}

    //public class MiniParvandeh
    //{
    //    public string Id { get; set; }
    //    public string Title { get; set; }
    //}
}
