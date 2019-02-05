using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI_messenger.Models
{
    public class Conversation
    {
        [BsonId]
        public ObjectId ConversationrId { get; set; }

        [BsonRequired]
        public ObjectId[] Members { get; set; }
    }
}