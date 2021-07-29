using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using coreJDK.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching;

namespace coreJDK
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region 系统相关配置
            SysConfigHelper.MysqlConnectionString = _configuration["MysqlOptions:MysqlConnectionString"];
            #endregion

            #region 蓝卓的相关配置
            LzConfigHelper.AuthUrl = _configuration["BlueTron:authUrl"];
            LzConfigHelper.AppId = _configuration["BlueTron:appId"];
            LzConfigHelper.AppSecret = _configuration["BlueTron:appSecret"];
            LzConfigHelper.OverdueTime = int.Parse(_configuration["BlueTron:overdueTime"]);
            #endregion

            services.AddDirectoryBrowser();
            services.AddOptions();
            services.AddHttpContextAccessor();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDistributedMemoryCache();

            services.AddCors(options =>
            {
                options.AddPolicy("any",
                      b =>
                      {
                          b.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                      });
            });
            
            //添加redis
            services.AddDistributedRedisCache(option =>
            {
                //redis 连接字符串
                option.Configuration = _configuration["ConnectionRedis:Connection"];
                //redis 实例名
                option.InstanceName = _configuration["ConnectionRedis:InstanceName"];

            });
            services.AddSession(option =>
            {
                option.Cookie.SameSite = SameSiteMode.None;
                option.Cookie.HttpOnly = true;
                option.Cookie.IsEssential = true;
                option.IdleTimeout = TimeSpan.FromMinutes(25);
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false; 
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.IsEssential = true;
                opt.Cookie.HttpOnly = true;
                opt.Cookie.Name = "core.demo";
                opt.Cookie.SameSite = SameSiteMode.None;
            });
            services.AddAntiforgery(o => {
                o.SuppressXFrameOptionsHeader = true;
                o.Cookie.SameSite = SameSiteMode.None;
            });
            var builder = new ContainerBuilder();
            Type baseType = typeof(IDependency);

            Assembly[] assemblys = new Assembly[]
            {
                Assembly.Load("coreJDK.Repository"),
                Assembly.Load("coreJDK.Services")
            };
            builder.RegisterAssemblyTypes(assemblys).Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Populate(services);
            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            logger.AddLog4Net();
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseCors("any");
            app.UseMiddleware<LZAuthVerifyMiddleware>();
            app.UseMvcWithDefaultRoute();
        }
    }
}
