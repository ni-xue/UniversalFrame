using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShareManager.Models;
using Share.Facade;

namespace ShareManager.Controllers
{
    public class RiskController : RiskPageBase
    {
        /// <summary>
        /// 构造函数，用于IOC注入
        /// </summary>
        /// <param name="facade"></param>
        public RiskController(FacadeManage facade) 
        {
            //facade.aideSqlFacade.Get();测试
        }

        public IActionResult Index()
        {
            return View();
        }

        #region 登陆模块

        public IActionResult Login()
        {
            return View();
        }

        #endregion

    }
}