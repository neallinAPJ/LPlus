using Model.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Server.User
{
    public interface IUserServer
    {
        Task<bool> CheckPasswordAsync(UserModel user);
        Task<IEnumerable<UserModel>> GetContactList(int userID);
    }
}
