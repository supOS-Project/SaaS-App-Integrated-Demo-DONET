namespace coreJDK.Model
{
   public class TenantParamsModel
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public string tenantId { get; set; }

        /// <summary>
        /// ess实例ID
        /// </summary>
        public string instanceId { get; set; }

        /// <summary>
        /// ess实例名称
        /// </summary>
        public string instanceName { get; set; }

        /// <summary>
        /// 租户开始时间，格式为yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string startDate { get; set; }

        /// <summary>
        /// 租户结束时间，格式为yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string endDate { get; set; }

        /// <summary>
        /// 开通app的supos实例所在区域
        /// </summary>
        public string region { get; set; }

        /// <summary>
        /// 开放平台创建app时生成的id
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// 请求时间戳
        /// </summary>
        public string timeStamp { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public string randomNumber { get; set; }

        /// <summary>
        /// 签名串，校验请求合法性
        /// </summary>
        public string sign { get; set; }
    }
}
