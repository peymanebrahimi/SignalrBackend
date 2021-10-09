using ChatApp.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatApp.Models.Expense
{
    public class Parvandeh : IHaveId, IHaveUserId
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Shomareh { get; set; }
        public string Baygani { get; set; }
    }
}