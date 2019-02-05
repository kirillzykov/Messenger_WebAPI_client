using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI_react_redux.Models
{
    public class Message
    {
        [BsonId]
        public ObjectId MessageId { get; set; }

        [BsonRequired]
        public ObjectId FromId { get; set; }

        [BsonRequired]
        public ObjectId ConversationId { get; set; }

        [BsonRequired]
        public DateTime DateSent { get; set; }

        [BsonRequired]
        public string Content { get; set; }
    }
}