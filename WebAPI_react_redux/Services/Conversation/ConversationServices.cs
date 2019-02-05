using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_messenger.Db;
using WebAPI_messenger.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace WebAPI_messenger.Services
{
    public class ConversationServices : IConversationServices
    {
        private readonly Context _context = null;

        public ConversationServices(IOptions<Settings> settings)
        {
            _context = new Context(settings);
        }

        public async Task<List<Conversation>> GetConversations(string userId)
        {
            var conversations = new List<Conversation>();

            BsonDocument filter = new BsonDocument();
            filter.Add("Members", new BsonDocument()
                    .Add("$elemMatch", new BsonDocument()
                            .Add("$eq", new BsonObjectId(new ObjectId(userId)))
                    )
            );

            var document = await _context.Conversations.FindAsync(filter);

            await document.ForEachAsync(doc => { conversations.Add(doc); });

            return conversations;
        }

        public async Task InsertConversation(Conversation conversation) => await _context.Conversations.InsertOneAsync(conversation);
    }
}