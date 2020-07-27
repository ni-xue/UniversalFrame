using Share.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversalFrame.Core.Utils;
using UniversalFrame.Core.Utils.Data;
using UniversalFrame.Core.Web.Api;
using UniversalFrame.Core.Web.Api.Routing;

namespace RiskManager.API
{
    public class Share : MinApi
    {
        private const string LogFilePath = "Log/API/Share/";

        protected override bool Initialize(AshxRouteData ashxRoute, out IAipOut aipOut)
        {
            aipOut = null;
            //aipOut = ApiOut.Json(new { msg = "取消访问。" });
            return true;
        }

        protected override IAipOut AshxException(AshxException ex)
        {
            Log.Error("ShareApi", ex, LogFilePath);
            return ApiOut.Json(new { msg = "发生异常。" });
        }

        public IAipOut GetApi() => ApiOut.Json(new { msg = "最小，路由版本api。" });

        public async Task<IAipOut> GetTaskApi() 
        {
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
        public async Task<IAipOut> GetSelect() 
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

        public IAipOut Get() => ApiOut.View();

        public async Task<IAipOut> GetTask() => await ApiOut.ViewAsyn();
    }
}
