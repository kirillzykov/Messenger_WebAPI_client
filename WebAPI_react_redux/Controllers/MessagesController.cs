using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using WebAPI_messenger.Models;
using WebAPI_messenger.Services;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

namespace WebAPI_messenger.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageServices dbMessages;

        public MessagesController(IMessageServices context)
        {
            dbMessages = context;
        }

        [HttpGet("GetMessages/{id}")]
        public async Task<JsonResult> GetMessages(string id)
        {
            List<Message> messages = await dbMessages.GetMessages(id);
            if (messages.Count == 0) { return new JsonResult("not found"); }
            return new JsonResult(messages);
        }

        [HttpPost]
        [Route("AddMessage")]
        public async Task<IActionResult> AddMessage([FromBody] PostMessage model)
        {
            await dbMessages.InsertMessage(new Message
            {
                FromId = new ObjectId(model.FromId),
                ConversationId = new ObjectId(model.ConversationId),
                DateSent = DateTime.UtcNow,
                Content = model.Content
            });
            return NoContent();
        }
    }
}