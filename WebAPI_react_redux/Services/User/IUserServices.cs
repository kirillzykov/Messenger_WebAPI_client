using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_react_redux.Models;
using MongoDB.Bson;

namespace WebAPI_react_redux.Services
{
    public interface IUserServices
    {
        Task<User> FindUserId(string id);

        Task<User> FindUserEmail(string email);

        Task<User> FindUserEmailPassword(string email, string password);

        Task InsertUser(User user);
    }
}