using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using WebAPI_messenger.Models;
using WebAPI_messenger.Services;

namespace WebAPI_messenger.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationServices dbConversations;
        private readonly IUserServices dbUsers;
        //private MongoDbServices _mongoDbService = new MongoDbServices("MessengerTest", "Users", "mongodb://127.0.0.1");

        public ConversationController(IConversationServices contextConv, IUserServices contextUser)
        {
            dbConversations = contextConv;
            dbUsers = contextUser;
        }

        [HttpPost]
        [Route("GetConversations")]
        public async Task<JsonResult> GetConversations(GetConversation model)
        {
            List<Conversation> conversations = await dbConversations.GetConversations(model.UserId);
            List<ReturnConversation> returnConversations = new List<ReturnConversation>();
            if (conversations.Count == 0) { return new JsonResult("not found"); }
            Parallel.ForEach(conversations, async (conv) =>
             {
                 ReturnConversation returnConversation = new ReturnConversation();
                 returnConversation.MembersName = new List<string>();
                 returnConversation.MembersEmail = new List<string>();
                 returnConversation.MembersId = new List<string>();
                 returnConversation.ConversationrId = conv.ConversationrId.ToString();
                 Parallel.ForEach(conv.Members, async (member) =>
                     {
                         User user = await dbUsers.FindUserId(member.ToString());
                         returnConversation.MembersName.Add(user.Name);
                         returnConversation.MembersEmail.Add(user.Email);
                         returnConversation.MembersId.Add(user.UserId.ToString());
                     });
                 returnConversations.Add(returnConversation);
             });
            return new JsonResult(returnConversations);
        }

        [HttpPost]
        [Route("CreateConversationWithUser")]
        public async Task<IActionResult> CreateConversationWithUser(CreateConversationWithUser model)
        {
            List<ObjectId> members = new List<ObjectId>();
            User user = await dbUsers.FindUserEmail(model.Email);

            members.Add(new ObjectId(model.UserId));
            members.Add(user.UserId);

            await dbConversations.InsertConversation(new Conversation { Members = members.ToArray() });
            return NoContent();
        }

        [HttpPost]
        [Route("CreateConversation")]
        public async Task<IActionResult> CreateConversation(CreateConversation model)
        {
            List<ObjectId> members = new List<ObjectId>();
            foreach (var member in model.Members)
            {
                members.Add(new ObjectId(member));
            }
            await dbConversations.InsertConversation(new Conversation { Members = members.ToArray() });
            return NoContent();
        }
    }
}