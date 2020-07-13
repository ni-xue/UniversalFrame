using Share.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversalFrame.Core.Utils.Data;
using UniversalFrame.Core.Web.Api;

namespace RiskManager.API
{
    public class Share : MinApi
    {
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
    }
}
