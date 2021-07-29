using coreJDK.Model;

namespace coreJDK.Repository
{
    public interface ITenantRepository : IBaseRepository<TenantModel>
    {
        /// <summary>
        /// 获取租户详情
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        TenantModel GetTenantModel(string tenantId);

        /// <summary>
        /// 添加租户
        /// </summary>
        /// <param name="tenantModel"></param>
        /// <returns></returns>
        bool AddTenant(TenantModel tenantModel);


        /// <summary>
        /// 租户续期
        /// </summary>
        /// <param name="tenantModel"></param>
        /// <returns></returns>
        bool UpdateTenantTimeByTenantId(TenantModel tenantModel);

        /// <summary>
        /// 修改租户的状态
        /// </summary>
        /// <param name="tenantModel"></param>
        /// <returns></returns>
        bool UpdateTenantStatusByTenantId(TenantModel tenantModel);
    }
}
