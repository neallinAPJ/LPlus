using Model.User;
using System.Threading.Tasks;

namespace Server.Server.User
{
    public interface IUserServer
    {
        Task<bool> CheckPasswordAsync(UserModel user);
    }
}
