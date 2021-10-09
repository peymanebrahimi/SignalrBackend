using ChatApp.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ChatApp.Models.Expense
{
    public class Cheque : IHaveId, IHaveUserId
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Shomareh { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string ChequeBank { get; set; }
        public string SaderKonandeh { get; set; }
    }
}