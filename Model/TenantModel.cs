using System;

namespace coreJDK.Model
{
   public class TenantModel
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public string tenantId { get; set; }

        /// <summary>
        /// 开通app的supos实例id
        /// </summary>
        public string instanceId { get; set; }

        /// <summary>
        /// 开通app的supos实例名称，调用open-api时需要此字段
        /// </summary>
        public string instanceName { get; set; }

        /// <summary>
        /// app租户开始时间
        /// </summary>
        public DateTime startTime { get; set; }

        /// <summary>
        /// app租户结束时间
        /// </summary>
        public DateTime endTime { get; set; }

        /// <summary>
        /// 开放平台创建app时生成的id
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// 开通app的supos实例所在区域，调用open-api时需要此字段
        /// </summary>
        public string region { get; set; }

        /// <summary>
        /// 租户开通状态
        /// </summary>
        public int status { get; set; }
    }
}
