using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_messenger.Db;
using WebAPI_messenger.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebAPI_messenger.Services
{
    public class UserServices : IUserServices
    {
        private readonly Context _context = null;

        public UserServices(IOptions<Settings> settings)
        {
            _context = new Context(settings);
        }

        public async Task<User> FindUserEmailPassword(string email, string password)
        {
            var user = new User();

            var filter = new BsonDocument("$and", new BsonArray{
                new BsonDocument("Email", email),
                new BsonDocument("Password", password)
            });

            var document = await _context.Users.FindAsync(filter);

            await document.ForEachAsync(doc => { user = doc; });

            return user;
        }

        public async Task<User> FindUserEmail(string email)
        {
            var user = new User();

            var filter = new BsonDocument("Email", email);

            var document = await _context.Users.FindAsync(filter);

            await document.ForEachAsync(doc => { user = doc; });

            return user;
        }

        public async Task InsertUser(User user) => await _context.Users.InsertOneAsync(user);

        public async Task<User> FindUserId(string id)
        {
            var user = new User();

            var filter = new BsonDocument("_id", new ObjectId(id));

            var document = await _context.Users.FindAsync(filter);

            await document.ForEachAsync(doc => { user = doc; });

            return user;
        }
    }
}