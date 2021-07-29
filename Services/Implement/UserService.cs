using coreJDK.Model;
using coreJDK.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace coreJDK.Services.Implement
{
    public class UserService : BaseService, IUserService
    {
        private IUserRepository _iUserRepository { get; set; }

        public UserService(IUserRepository iUserRepository)
        {
            _iUserRepository = iUserRepository;
        }

        /// <summary>
        /// 获取租户下的所有用户列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public List<UserModel> GetUserListByTenantId(string tenantId) {
            return _iUserRepository.GetUserListByTenantId(tenantId);
        }
    }
}
