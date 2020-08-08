using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RiskManager.Models;
using Share.Facade;

namespace RiskManager.Controllers
{
    public class RiskController : RiskPageBase
    {

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