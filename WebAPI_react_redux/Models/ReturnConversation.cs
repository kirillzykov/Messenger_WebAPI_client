using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_react_redux.Models
{
    public class ReturnConversation
    {
        public string ConversationrId { get; set; }

        public List<string> MembersId { get; set; }

        public List<string> MembersName { get; set; }

        public List<string> MembersEmail { get; set; }
    }
}