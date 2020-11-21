using Share.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Tool.Utils;
using Tool.Utils.Data;
using Tool.Web.Api;
using Tool;
using Tool.Web;
using Share.Entity;

namespace ShareManager.API
{
    public class ShareServers : ApiAshx
    {
        private const string LogFilePath = "Log/API/RiskServers/";

        protected override bool Initialize(Ashx ashx)
        {
            return true;
        }

        protected override void AshxException(AshxException ex)
        {
            ex.ExceptionHandled = true;
            Log.Debug("RiskServers/API接口异常", ex, LogFilePath);
            Json(new
            {
                code = 500,
                msg = "接口异常！"
            });
        }

        /// <summary>
        /// 登陆
        /// </summary>
        [Ashx(ID = "ILogin", State = AshxState.Post)]
        public void Login(string LoginName, string LoginPwd)
        {
            AjaxJson _ajv = new AjaxJson();
            try
            {
                if (string.IsNullOrEmpty(LoginName) || string.IsNullOrEmpty(LoginPwd))
                {
                    _ajv.code = 10;
                    _ajv.msg = "用户ID和密码不能为空！";
                }
                else
                {
                    ShareUser user = new ShareUser { LoginName = LoginName, LoginPwd = LoginPwd };
                    //登录成功保存用户信息
                    Session.Set("User", user);
                    _ajv.code = 0;
                    _ajv.msg = "登录成功！";
                }
            }
            catch (Exception ex)
            {
                _ajv.code = 500;
                _ajv.msg = "系统错误！";
                Log.Error("ILoginError", ex, LogFilePath);
            }
            Json(_ajv);
        }

        [Ashx]
        public void Get()
        {
            //DataSet ds = FacadeManage.AideSqlFacade.Get();

            var Pager = FacadeManage.AideSqlFacade.GetPager();
            if (Pager.CheckedPageSet())
            {
                AjaxJson ajax = new AjaxJson() { code = 0, msg = "查询成功！" };
                ajax.SetPage(Pager);
                ajax.SetDataItem("list", Pager.PageTable.ToDictionary());
                Json(ajax);
            }
            else
            {
                Json(new { msg = "暂无数据。", code = 200 });
            }
        }

        public async Task GetAsync() 
        {
            await JsonAsync(new { msg = "暂无数据。", code = 200 });
        }
    }
}
