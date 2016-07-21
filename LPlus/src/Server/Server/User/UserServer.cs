using Dapper;
using Model.User;
using Server.DataProvide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Server.User
{
    public class UserServer:IUserServer
    {
        private IMySqlContext _context;
        public UserServer(IMySqlContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckPasswordAsync(UserModel user)
        {
            string queryStr = string.Empty;
            queryStr = string.Format("select ID from User where Account='{0}' and Password='{1}'", user.Account, user.Password);
            var userID = await _context.Connection.QueryAsync<int>(queryStr);
            if (userID.FirstOrDefault() > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<UserModel>> GetContactList(int userID)
        {
            string queryStr = string.Empty;
            queryStr = string.Format("select * from User where ID IN(Select FriendID from Friends where UserID={0}) ", userID);
            return await _context.Connection.QueryAsync<UserModel>(queryStr);
        }
        public async Task<IEnumerable<UserModel>> GetCurrentUser(string userName)
        {
            string queryStr = string.Format("select * from User where userName='{0}' ", userName);

            return await _context.Connection.QueryAsync<UserModel>(queryStr); ;
        }
    }
}
