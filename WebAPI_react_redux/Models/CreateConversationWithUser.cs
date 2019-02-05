using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_messenger.Models
{
    public class CreateConversationWithUser
    {
        public string UserId { get; set; }

        public string Email { get; set; }
    }
}