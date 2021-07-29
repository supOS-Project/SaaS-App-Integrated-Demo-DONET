using coreJDK.Common;
using MySql.Data.MySqlClient;

namespace coreJDK.Repository
{
    public class DbDapper
    {
        public static MySqlConnection GetMySqlConnection()
        {
            MySqlConnection connection = new MySqlConnection(SysConfigHelper.MysqlConnectionString);
            return connection;
        }
    }
}
