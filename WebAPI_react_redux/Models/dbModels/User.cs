using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI_react_redux.Models
{
    public class User
    {
        [BsonId]
        public ObjectId UserId { get; set; }

        [BsonRequired]
        public string Email { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public string Password { get; set; }
    }
}