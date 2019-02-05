using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WebAPI_messenger.Db;
using WebAPI_messenger.Models;

namespace WebAPI_messenger
{
    public class Context
    {
        public IConfigurationRoot Configuration { get; }
        private IMongoDatabase _database = null;

        public Context(IOptions<Settings> settings)
        {
            //Configuration = settings.Value.ConfigurationRoot;
            settings.Value.connectionString = "mongodb://127.0.0.1";//Configuration.GetSection("MongoConnection:ConnectionString").Value;
            settings.Value.dbName = "MessengerTest";//Configuration.GetSection("MongoConnection:Database").Value;

            var client = new MongoClient(settings.Value.connectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.dbName);
            }
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("Users");
            }
        }

        public IMongoCollection<Conversation> Conversations
        {
            get
            {
                return _database.GetCollection<Conversation>("Conversation");
            }
        }

        public IMongoCollection<Message> Messages
        {
            get
            {
                return _database.GetCollection<Message>("Message");
            }
        }
    }
}