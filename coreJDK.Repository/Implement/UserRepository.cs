using coreJDK.Model;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace coreJDK.Repository.Implement
{
    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {

        #region 获取用户列表
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public List<UserModel> GetUserListByTenantId(string tenantId)
        {
            using (IDbConnection conn = DbDapper.GetMySqlConnection())
            {
                StringBuilder query = new StringBuilder();

                query.AppendLine("select user_id userId,user_name userName,dep_id depId,tenant_id tenantId from sys_user where tenant_id = @tenantId ");
                return conn.Query<UserModel>(query.ToString(), new { tenantId }).ToList();
            }
        }
        #endregion

    }
}
