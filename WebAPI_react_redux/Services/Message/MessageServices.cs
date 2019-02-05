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
    public class MessageServices : IMessageServices
    {
        private readonly Context _context = null;

        public MessageServices(IOptions<Settings> settings)
        {
            _context = new Context(settings);
        }

        public async Task<List<Message>> GetMessages(string convId)
        {
            var messages = new List<Message>();

            var filter = new BsonDocument("ConversationId", new ObjectId(convId));

            var document = await _context.Messages.FindAsync(filter);

            await document.ForEachAsync(doc => { messages.Add(doc); });

            return messages;
        }

        public async Task InsertMessage(Message message) => await _context.Messages.InsertOneAsync(message);
    }
}