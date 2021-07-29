using coreJDK.Common;
using coreJDK.Model;
using coreJDK.Repository;
using Newtonsoft.Json;
using System;

namespace coreJDK.Services.Implement
{
    public class TenantService : BaseService, ITenantService
    {
        private ITenantRepository _iTenantRepository { get; set; }

        public TenantService(ITenantRepository iTenantRepository)
        {
            _iTenantRepository = iTenantRepository;
        }


        #region 通过租户ID 获取详情
        /// <summary>
        /// 通过租户ID 获取详情
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public TenantModel GetTenantModel(string tenantId)
        {
            return _iTenantRepository.GetTenantModel(tenantId);
        }
        #endregion

        #region 添加租户
        /// <summary>
        /// 添加租户
        /// </summary>
        /// <param name="tenantModel"></param>
        /// <returns></returns>
        public Result<object> AddTenant(TenantParamsModel tenantParamsModel)
        {
            if (RASHelper.RSAEncrypt(JsonConvert.SerializeObject(tenantParamsModel), tenantParamsModel.sign))
            {
                TenantModel tenantModel = new TenantModel
                {
                    tenantId = Guid.NewGuid().ToString().Replace("-", ""),
                    instanceId = tenantParamsModel.instanceId,
                    instanceName = tenantParamsModel.instanceName,
                    startTime = Convert.ToDateTime(tenantParamsModel.startDate),
                    endTime = Convert.ToDateTime(tenantParamsModel.endDate),
                    region = tenantParamsModel.region,
                    appId = tenantParamsModel.appId,
                    status = 1
                };
                if (_iTenantRepository.AddTenant(tenantModel))
                {
                    return Result<object>.Success("添加租户成功", new { tenantModel.tenantId });
                }
                else
                {
                    return Result<object>.Error("添加租户失败", null);
                }
            }
            return Result<object>.Error("签名认证失败", null);
        }
        #endregion

        #region 获取租户状态
        /// <summary>
        /// 获取租户状态
        /// </summary>
        /// <param name="tenantParamsModel"></param>
        /// <returns></returns>
        public Result<object> GetTenantStatus(TenantParamsModel tenantParamsModel)
        {
            //if (RASHelper.RSAEncrypt(JsonConvert.SerializeObject(tenantParamsModel), tenantParamsModel.sign))
            //{
                TenantModel tenantModel = GetTenantModel(tenantParamsModel.tenantId);
                return tenantModel != null ? Result<object>.Success("获取租户状态成功", new { tenantModel.tenantId }) : Result<object>.Error("获取租户状态失败", null);

            //}
            //return Result<object>.Error("签名认证失败", null);
        }
        #endregion

        #region 修改租户时间
        /// <summary>
        /// 修改租户时间
        /// </summary>
        /// <param name="tenantModel"></param>
        /// <returns></returns>
        public Result UpdateTenantTimeByTenantId(TenantParamsModel tenantParamsModel)
        {
           // if (RASHelper.RSAEncrypt(JsonConvert.SerializeObject(tenantParamsModel), tenantParamsModel.sign))
            //{
                TenantModel tenantModel = new TenantModel
                {
                    tenantId = tenantParamsModel.tenantId,
                    endTime = Convert.ToDateTime(tenantParamsModel.endDate)
                };
                return _iTenantRepository.UpdateTenantTimeByTenantId(tenantModel) ? Result.Success("租户续期成功！") : Result.Error("租户续期失败！");
          //  }
          //  return Result.Error("签名认证失败");
        }
        #endregion

        #region 修改租户停止
        /// <summary>
        /// 修改租户停止
        /// </summary>
        /// <param name="tenantModel"></param>
        /// <returns></returns>
        public Result StopTenant(TenantParamsModel tenantParamsModel)
        {
            //if (RASHelper.RSAEncrypt(JsonConvert.SerializeObject(tenantParamsModel), tenantParamsModel.sign))
            //{
                TenantModel tenantModel = new TenantModel
                {
                    tenantId = tenantParamsModel.tenantId,
                    status = 0
                };
               return _iTenantRepository.UpdateTenantStatusByTenantId(tenantModel) ? Result.Success("租户停用成功！") : Result.Error("租户停用失败！");
           // }
            //return Result.Error("签名认证失败");
        }
        #endregion
    }
}