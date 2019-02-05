using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_react_redux.Models
{
    public class CreateConversationWithUser
    {
        public string UserId { get; set; }

        public string Email { get; set; }
    }
}