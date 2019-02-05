using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI_messenger.Models
{
    public class LoginModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}