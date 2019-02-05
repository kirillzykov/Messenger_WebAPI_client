using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_messenger.Models
{
    public class PostMessage
    {
        public string FromId { get; set; }
        public string ConversationId { get; set; }
        public string Content { get; set; }
    }
}