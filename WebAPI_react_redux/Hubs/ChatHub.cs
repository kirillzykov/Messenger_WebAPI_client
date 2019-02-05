using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebAPI_messenger.Models;

namespace WebAPI_messenger.Hubs
{
    ///TODO: Add logic so that not every user gets a notification when message and conversation are created

    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendConv(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveConv", user, message);
        }
    }
}