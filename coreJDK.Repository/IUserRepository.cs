using coreJDK.Model;
using System.Collections.Generic;

namespace coreJDK.Repository
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
      
        /// <summary>
        /// 通过租户ID获取用户列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        List<UserModel> GetUserListByTenantId(string tenantId);
    }
}
