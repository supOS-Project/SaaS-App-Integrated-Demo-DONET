using coreJDK.Model;
using System.Collections.Generic;

namespace coreJDK.Services
{
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// 通过租户ID获取用户列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        List<UserModel> GetUserListByTenantId(string tenantId);
    }
}
