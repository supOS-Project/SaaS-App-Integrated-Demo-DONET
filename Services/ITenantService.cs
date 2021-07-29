using coreJDK.Common;
using coreJDK.Model;

namespace coreJDK.Services
{
    public interface ITenantService : IBaseService
    {
        /// <summary>
        /// 通过租户ID 获取详情
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        TenantModel GetTenantModel(string tenantId);

        /// <summary>
        /// 添加租户
        /// </summary>
        /// <param name="tenantModel"></param>
        /// <returns></returns>
        Result<object> AddTenant(TenantParamsModel tenantParamsModel);

        /// <summary>
        /// 修改租户时间
        /// </summary>
        /// <param name="tenantModel"></param>
        /// <returns></returns>
        Result UpdateTenantTimeByTenantId(TenantParamsModel tenantParamsModel);

        /// <summary>
        /// 修改租户停止
        /// </summary>
        /// <param name="tenantModel"></param>
        /// <returns></returns>
        Result StopTenant(TenantParamsModel tenantParamsModel);

        /// <summary>
        /// 查询租户状态
        /// </summary>
        /// <param name="tenantParamsModel"></param>
        /// <returns></returns>
        Result<object> GetTenantStatus(TenantParamsModel tenantParamsModel);
    }
}
