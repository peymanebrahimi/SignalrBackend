using ChatApp.Data.Expense;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ChatApp.Models.Expense
{
    public class Received:IHaveId
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

    public class Client:IHaveId
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string NationalCode { get; set; }

    }

    public class Parvandeh:IHaveId
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Shomareh { get; set; }
        public string Baygani { get; set; }
    }

    public class Cheque:IHaveId
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Shomareh { get; set; }
        public DateTime ChequeDate { get; set; }
        public string Bank { get; set; }
    }
}
