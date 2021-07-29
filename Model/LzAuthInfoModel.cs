namespace coreJDK.Model
{
   public class LzAuthInfoModel
    {
        /// <summary>
        /// 认证token
        /// </summary>
        public string accessToken { get; set; }

        /// <summary>
        /// 认证时间
        /// </summary>
        public int expiresIn { get; set; }

        /// <summary>
        /// 刷新token
        /// </summary>
        public string refreshToken { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public string companyCode { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string personCode { get; set; }

        /// <summary>
        /// 创建时间戳
        /// </summary>
        public long createTime { get; set; }
    }
}
