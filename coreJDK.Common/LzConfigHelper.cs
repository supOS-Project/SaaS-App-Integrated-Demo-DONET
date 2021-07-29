namespace coreJDK.Common
{
    public class LzConfigHelper
    {

        /// <summary>
        /// 认证的缓存前缀
        /// </summary>
        public static string PreAuthCacheKey = "LzAuthInfo_";

        /// <summary>
        /// 公钥
        /// </summary>
        public static string PublicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCsNswFbCci+u6FYNPaKdrkIMZ5ymaVcRjav4GP9zpDsPVBnUMEC7i8DfpzJq6QHr06/xitlSPbDDoUPcpPpf//bE9Q30iRvDxGmD2SDSAJGl4sOSzR7ol36CEZwIQOXr63i3PP2iDQ+Pnev5tJi240d0oRJqzsTnrjxBthCwylmQIDAQAB";

        /// <summary>
        /// 认证地址
        /// </summary>
        public static string AuthUrl { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public static string AppId { get; set; }

        /// <summary>
        /// 应用秘钥
        /// </summary>
        public static string AppSecret { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public static int OverdueTime { get; set; }
        
    }
}
