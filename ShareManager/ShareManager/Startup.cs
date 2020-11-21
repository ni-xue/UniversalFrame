using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Share.Facade;
using Tool.Utils;
using Tool.Web;
using Tool.Web.Api.Builder;
using Tool.Web.Session;

namespace ShareManager
{
    /// <summary>
    /// Web 服务 启动配置
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置信息接口</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 默认获取配置信息接口
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();//注册Session
            services.AddMvc(o => o.EnableEndpointRouting = false).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest); //注册MVC
            services.AddAshx(o => o.IsAsync = true)//注册api。
                .AddHttpContext();//注册静态方式的HttpContext对象获取。
            FacadeManage.AddSql(services);//采用更安全的方式注册数据库。
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment()) //当存在 appsettings.Development.json 文件的时 为 true
            {
                app.UseDeveloperExceptionPage(); // 会采用开发模式，进行页面错误异常打印。
            }
            else
            {
                app.UseExceptionHandler(AllException);
            }

            FacadeManage.UseSqlLog(loggerFactory);//注册相关SQL日志。

            app.UseSession();

            //app.UseAsSession();//可以暂时使用框架自带Session

            app.UseStaticFiles(); //运行用户请求wwwroot文件下的资源文件。

            //注册路由规则（MVC）
            app.UseAshx(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "Api/{controller=ShareServers}/{action=GetAsync}");
            });
            //注册路由规则（Api）
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Share}/{action=Login}/{id?}");
            });
        }

        /// <summary>
        /// 创建一个在线上模式下，捕获全局异常的统一接口
        /// </summary>
        /// <param name="context">请求发起对象</param>
        /// <param name="exception">异常信息</param>
        public void AllException(HttpContext context, Exception exception)
        {
            context.Response.Write("An unknown error has occurred!");
            Log.Error("捕获全局异常：", exception);
        }

    }
}
