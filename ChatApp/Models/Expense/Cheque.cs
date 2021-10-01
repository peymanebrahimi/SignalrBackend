using System;
using ChatApp.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatApp.Models.Expense
{
    public class Cheque : IHaveId
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Shomareh { get; set; }
        public DateTime ChequeDate { get; set; }
        public string Bank { get; set; }
    }
}