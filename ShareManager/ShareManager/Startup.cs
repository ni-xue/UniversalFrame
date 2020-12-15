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
    /// Web ���� ��������
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="configuration">������Ϣ�ӿ�</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Ĭ�ϻ�ȡ������Ϣ�ӿ�
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();//ע��Session
            services.AddMvc(o => o.EnableEndpointRouting = false).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest); //ע��MVC
            services.AddAshx(o => o.IsAsync = true)//ע��api��
                .AddHttpContext();//ע�ᾲ̬��ʽ��HttpContext�����ȡ��
            FacadeManage.AddSql(services);//���ø���ȫ�ķ�ʽע�����ݿ⡣
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment()) //������ appsettings.Development.json �ļ���ʱ Ϊ true
            {
                app.UseDeveloperExceptionPage(); // ����ÿ���ģʽ������ҳ������쳣��ӡ��
            }
            else
            {
                app.UseExceptionHandler(AllException);
            }

            FacadeManage.UseSqlLog(loggerFactory);//ע�����SQL��־��

            app.UseSession();

            //app.UseAsSession();//������ʱʹ�ÿ���Դ�Session

            app.UseStaticFiles(); //�����û�����wwwroot�ļ��µ���Դ�ļ���

            //ע��·�ɹ���MVC��
            app.UseAshx(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "Api/{controller=ShareServers}/{action=GetAsync}");
            });
            //ע��·�ɹ���Api��
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Share}/{action=Login}/{id?}");
            });
        }

        /// <summary>
        /// ����һ��������ģʽ�£�����ȫ���쳣��ͳһ�ӿ�
        /// </summary>
        /// <param name="context">���������</param>
        /// <param name="exception">�쳣��Ϣ</param>
        public void AllException(HttpContext context, Exception exception)
        {
            context.Response.Write("An unknown error has occurred!");
            Log.Error("����ȫ���쳣��", exception);
        }

    }
}
