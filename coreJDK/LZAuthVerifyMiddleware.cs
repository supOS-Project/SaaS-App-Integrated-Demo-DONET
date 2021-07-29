using coreJDK.Common;
using coreJDK.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using coreJDK.Services;
using System.Text;

namespace coreJDK
{
    public class LZAuthVerifyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LZAuthVerifyMiddleware> _logger;
        private readonly ITenantService _iTenanteService;

        public LZAuthVerifyMiddleware(RequestDelegate next, ILogger<LZAuthVerifyMiddleware> logger,  ITenantService tenantService)
        {
            _next = next;
            _logger = logger;
            _iTenanteService = tenantService;
        }


        public async Task Invoke(HttpContext context)
        {
            string sessionId = context.Session.Id;
            _logger.LogDebug("Invoke---sessionId=" + sessionId);
            //过滤对外接口不进行auth的认证
            if (context.Request.Path.ToString().Contains("/open-api"))
            {
                await _next(context);
                return;
            }

            //如果是auth重定向的地址，则进行请求获取access_token
            if (context.Request.Path.ToString().Contains("LzAuth/Index"))
            {
                await GetAccessToken(context, sessionId);
                await _next(context);
                return;
            }
            else
            {
                //从缓存中获取是否已经认证，如果未认证则需要过蓝卓的单点登录
                var sessionInfo = context.Session.Get(LzConfigHelper.PreAuthCacheKey + sessionId);
                
                LzAuthInfoModel lzAuthInfoModel = sessionInfo != null ? JsonConvert.DeserializeObject<LzAuthInfoModel>(Encoding.UTF8.GetString(sessionInfo)) : null;
                if (lzAuthInfoModel == null)
                {
                    await RedirectAuth(context);
                }
                else
                {
                    //如果已经认证则进入判断access_token是否已经过期，如果过期则刷新access_token
                    if (TimeUtils.ConvertDateTimeToInt() - lzAuthInfoModel.createTime >= LzConfigHelper.OverdueTime)
                    {
                        await RefeshAccessToken(context, lzAuthInfoModel, sessionId);
                    }
                }
                await _next(context);
            }
        }

        /// <summary>
        /// 重定向
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task RedirectAuth(HttpContext context)
        {
            var request = context.Request;
            request.Query.TryGetValue("tenantId", out var tenantId);
            TenantModel tenantModel = _iTenanteService.GetTenantModel(tenantId);
            if (tenantModel == null)
            {
                await HandleErrorAsync(context, "租户信息不存在");
                return;
            }
            tenantId = tenantModel.tenantId;
            string scope = $"openid,{tenantModel.region},{tenantModel.instanceName}";
            string callBackUrl = $"{request.Scheme}://{request.Host}/LzAuth/Index?tenantId={tenantId}";

            //组合跳转地址
            string authUrl = LzConfigHelper.AuthUrl
                            + "oauth/authorize?responseType=code&appid="
                            + LzConfigHelper.AppId
                            + "&redirectUri=" + callBackUrl
                            + "&state=1&scope="
                            + scope;
            context.Response.Redirect(authUrl);
            await context.Response.WriteAsync("");
        }


        /// <summary>
        /// 获取accessToken
        /// </summary>
        /// <returns></returns>
        private async Task GetAccessToken(HttpContext context, string sessionId)
        {
            try
            {
                context.Request.Query.TryGetValue("code", out var code);
                string getAccessCodeUrl = LzConfigHelper.AuthUrl + "oauth/accessToken?grantType=authorization_code&appid="
                                                              + LzConfigHelper.AppId
                                                              + "&secret=" + LzConfigHelper.AppSecret
                                                              + "&code=" + code;
                string result = RequestHelper.HttpGet(getAccessCodeUrl);
                if (string.IsNullOrEmpty(result))
                {
                    _logger.LogError("获取蓝卓OAuth认证access_token失败", getAccessCodeUrl);
                    await RedirectAuth(context);
                }
                else
                {
                    LzResultModel<LzAuthInfoModel> resultModel = JsonConvert.DeserializeObject<LzResultModel<LzAuthInfoModel>>(result);
                    //如果获取access_code不成功则重新走认证流程
                    if (resultModel.code != 1)
                    {
                        await RedirectAuth(context);
                    }
                    else
                    {
                        LzAuthInfoModel lzAuthInfoModel = new LzAuthInfoModel
                        {
                            accessToken = resultModel.data.accessToken,
                            expiresIn = resultModel.data.expiresIn,
                            refreshToken = resultModel.data.refreshToken,
                            userName = resultModel.data.userName,
                            companyCode = resultModel.data.companyCode,
                            personCode = resultModel.data.personCode,
                            createTime = TimeUtils.ConvertDateTimeToInt() - 5000
                        };
                        _logger.LogDebug("Invoke---sessionId="+ sessionId+"；lzauthinfomodel=" + JsonConvert.SerializeObject(lzAuthInfoModel));
                        context.Session.Set(LzConfigHelper.PreAuthCacheKey + sessionId, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(lzAuthInfoModel)));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError("获取蓝卓OAuth认证access_token失败", e);
                await HandleErrorAsync(context, "获取蓝卓OAuth认证access_token失败");
            }
        }

        /// <summary>
        /// 刷新accessToken
        /// </summary>
        /// <returns></returns>
        private async Task RefeshAccessToken(HttpContext context, LzAuthInfoModel lzAuthInfoModel, string sessionId)
        {
            try
            {
                string refeshAuthUrl = LzConfigHelper.AuthUrl + "oauth/refreshToken?refreshToken=" + lzAuthInfoModel.refreshToken;
                string result = RequestHelper.HttpGet(refeshAuthUrl);
                if (string.IsNullOrEmpty(result))
                {
                    _logger.LogError("刷新蓝卓OAuth认证失败", refeshAuthUrl);
                    await HandleErrorAsync(context, "刷新蓝卓OAuth认证失败");
                }
                else
                {
                    LzResultModel<LzAuthInfoModel> resultModel = JsonConvert.DeserializeObject<LzResultModel<LzAuthInfoModel>>(result);
                    lzAuthInfoModel.accessToken = resultModel.data.accessToken;
                    lzAuthInfoModel.expiresIn = resultModel.data.expiresIn;
                    lzAuthInfoModel.refreshToken = resultModel.data.refreshToken;
                    context.Session.Set(LzConfigHelper.PreAuthCacheKey + sessionId, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(lzAuthInfoModel)));
                }
            }
            catch (Exception e)
            {
                _logger.LogError("刷新蓝卓OAuth认证失败", e);
                await HandleErrorAsync(context, "刷新蓝卓OAuth认证失败");
            }
        }

        /// <summary>
        /// 处理失败
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task HandleErrorAsync(HttpContext context, string message)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await response.WriteAsync(JsonConvert.SerializeObject(Result.Error(message)));
        }
    }
}
