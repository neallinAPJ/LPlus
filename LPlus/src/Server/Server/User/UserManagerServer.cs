using Model.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using System.Threading;
using Server.DataProvide;
using Microsoft.Extensions.Logging;
using Dapper;
using System.Linq;

namespace Server.Server.User
{
    public class UserManagerServer : UserManager<UserModel>
    {
        private readonly IMySqlContext _context;
        private readonly IUserStore<UserModel> _store;
        public UserManagerServer(IUserStore<UserModel> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<UserModel> passwordHasher, IEnumerable<IUserValidator<UserModel>> userValidators, IEnumerable<IPasswordValidator<UserModel>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<UserModel>> logger, IMySqlContext context) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _store = store;
            _context = context;
        }
        public override async Task<IdentityResult> CreateAsync(UserModel user)
        {
            if (user == null)
            {
                throw new Exception("user is not empty!");
            }
            string query = "INSERT INTO User(ID,Name,Account,Email,Sex,Password,Adress)values(@ID,@Name,@Account,@Email,@Sex,@Password,@Adress);";
            var row = await _context.Connection.ExecuteAsync(query, user);
            IdentityResult result;
            if (row > 0)
            {
                result = IdentityResult.Success;
            }
            else
            {
                result = IdentityResult.Failed();
            }
            return result;
        }
        public override async Task<UserModel> FindByNameAsync(string userName)
        {
            string queryStr = string.Empty;
            queryStr = string.Format("select * from User where Account='{0}'", userName);
            var user = await _context.Connection.QueryAsync<UserModel>(queryStr);
            if (user != null && user.Count() > 0)
            {
                return user.FirstOrDefault();
            }
            return new UserModel();
        }

    }
}
