using Microsoft.Extensions.Logging;
using Share.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tool.Utils;
using Tool.Utils.Data;
using Tool.Web;
using Tool.Web.Api;
using Tool.Web.Routing;

namespace ShareManager.Api
{
    public class Share : MinApi
    {
        private const string LogFilePath = "Log/API/Share/";

        //存放日志服务
        private ILogger _logger;

        protected override IApiOut Initialize(AshxRouteData ashxRoute)
        {
            if (_logger == null)
            {
                //获取日志输出服务
                ILoggerFactory loggerFactory = ashxRoute.HttpContext.GetService<ILoggerFactory>();//ILogger loggerFactory = context.GetService<ILogger<Share>>();
                _logger = loggerFactory.CreateLogger("api");
            }

            //return ApiOut.Json(new { msg = "取消访问。" });
            return null;
        }

        protected override void OnResult(AshxRouteData ashxRoute)
        {
            //写一些类似公共日志的东西
        }

        protected override IApiOut AshxException(AshxException ex)
        {
            Log.Error("ShareApi", ex, LogFilePath);
            return ApiOut.Json(new { msg = "发生异常。" });
        }

        public IApiOut GetApi() => ApiOut.Json(new { msg = "最小，路由版本api。" });

        public async Task<IApiOut> GetTaskApi() 
        {
            _logger.LogInformation("打印日志", "我是GetTaskApi接口。");

            var Pager = FacadeManage.AideSqlFacade.GetPager();
            if (Pager.CheckedPageSet())
            {
                AjaxJson ajax = new AjaxJson() { code = 0, msg = "查询成功！" };
                ajax.SetPage(Pager);
                ajax.SetDataItem("list", Pager.PageTable.ToDictionary());
                return await ApiOut.JsonAsyn(ajax);
            }
            else
            {
                return await ApiOut.JsonAsyn(new { msg = "暂无数据。", code = 200 });
            }
        }

        /// <summary>
        /// 全新查询写法
        /// </summary>
        /// <returns></returns>
        public async Task<IApiOut> GetSelect() 
        {
            var data = FacadeManage.AideSqlFacade.Select();
            if (data.IsEmpty())
            {
                return await ApiOut.JsonAsyn(new { msg = "暂无数据。", code = 200 });
            }
            AjaxJson ajax = new AjaxJson() { code = 0, msg = "查询成功！" };
            ajax.SetDataItem("list", data.ToDictionary());
            return await ApiOut.JsonAsyn(ajax);
        }

        public IApiOut Get() => ApiOut.View();//默认路径Views\Share\Get.html

        public IApiOut Get1() => ApiOut.View(@"Views\Share\Get.html");//强制指定路径

        public async Task<IApiOut> GetTask() => await ApiOut.ViewAsyn();//默认路径Views\Share\GetTask.html

        public async Task<IApiOut> GetTask1() => await ApiOut.ViewAsyn(@"Views\Share\GetTask.html");//强制指定路径

        /// <summary>
        /// 重定向URL
        /// </summary>
        /// <param name="url">新的位置</param>
        /// <returns></returns>
        public async Task<IApiOut> Redirect([ApiVal(Val.Query)] string url) => await ApiOut.RedirectAsyn(url);
    }
}
