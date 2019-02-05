using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_react_redux.Models;
using MongoDB.Bson;

namespace WebAPI_react_redux.Services
{
    public interface IMessageServices
    {
        Task<List<Message>> GetMessages(string convId);

        Task InsertMessage(Message message);
    }
}