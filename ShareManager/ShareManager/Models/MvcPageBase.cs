using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Share.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tool;
using Tool.Web;

namespace ShareManager.Models
{
    /// <summary>
    /// MVC 父类继承
    /// </summary>
    public class MvcPageBase : Controller
    {
        /// <summary>
        /// 获取真正的登录URL
        /// </summary>
        /// <returns></returns>
        public string LoginUrl { get => $"{Request.Scheme}://{Request.Host.Value}/Share/Login"; }

        /// <summary>
        /// 获取真正的登录URL
        /// </summary>
        /// <returns></returns>
        public string IndexUrl { get => $"{Request.Scheme}://{Request.Host.Value}/Share/Index"; }

        /// <summary>
        /// 当前请求地址
        /// </summary>
        public string GetUrl { get => $"{Request.Scheme}://{Request.Host.Value}/{Request.RouteValues["controller"]}/{Request.RouteValues["action"]}"; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            string controller = context.ActionDescriptor.RouteValues["controller"];
            string action = context.ActionDescriptor.RouteValues["action"];

            if (controller == "Risk" && action == "Login") return;
            if (!UserLogon())
            {
                string loginUrl = LoginUrl;
                loginUrl = string.Format("{0}{1}url={2}", loginUrl, loginUrl.Contains("?") ? "&" : "?", GetUrl.StringEncode());

                context.Result = new RedirectResult(loginUrl);
            } 
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        /// <summary>
        /// 用户登录(初始化加载时获取用户对象是否在Session中，如果在则将其写入缓存中)
        /// </summary>
        public virtual bool UserLogon()
        {
            try
            {
                if (HttpContext.Session.TryGetValue("User", out ShareUser user))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
    }
}
