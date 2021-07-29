using coreJDK.Common;
using coreJDK.Model;
using coreJDK.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace coreJDK.Controllers
{
    [Route("open-api/app")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        private readonly ILogger _logger;
        public TenantController(ITenantService tenantService, ILogger<TenantController> logger) {
            this._tenantService = tenantService;
            this._logger = logger;
        }

        /// <summary>
        /// 添加租户
        /// </summary>
        /// <param name="tenantParamsModel"></param>
        /// <returns></returns>
        [HttpPost("tenants")]
        public Result<object> AddTenant([FromBody] TenantParamsModel tenantParamsModel)
        {
            return _tenantService.AddTenant(tenantParamsModel);
        }

        /// <summary>
        /// 获取租户状态
        /// </summary>
        /// <param name="tenantParamsModel"></param>
        /// <returns></returns>
        [HttpPost("tenants/status")]
        public Result<object> GetTenantStatus([FromBody] TenantParamsModel tenantParamsModel)
        {
            return _tenantService.GetTenantStatus(tenantParamsModel);
        }

        /// <summary>
        /// 租户续租
        /// </summary>
        /// <param name="tenantParamsModel"></param>
        /// <returns></returns>
        [HttpPost("renew")]
        public Result UpdateTenantTime([FromBody] TenantParamsModel tenantParamsModel)
        {
            return _tenantService.UpdateTenantTimeByTenantId(tenantParamsModel);
        }

        /// <summary>
        /// 租户停止
        /// </summary>
        /// <param name="tenantParamsModel"></param>
        /// <returns></returns>
        [HttpPost("stop")]
        public Result StopTenant([FromBody] TenantParamsModel tenantParamsModel)
        {
            return _tenantService.StopTenant(tenantParamsModel);
        }
    }
}
