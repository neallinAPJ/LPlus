

using MySql.Data.MySqlClient;

namespace Server.DataProvide
{
    public interface IMySqlContext
    {
        MySqlConnection Connection { get; }
        void Dispose();
    }
}
