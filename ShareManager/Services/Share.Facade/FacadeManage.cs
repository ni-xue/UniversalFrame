using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Share.Facade.Facade;
using System;
using Tool.Web;

namespace Share.Facade
{
    public class FacadeManage
    {
        private static readonly object lockObj = new object();

        ///// <summary>
        ///// 配置库逻辑
        ///// </summary>
        public readonly SqlFacade aideSqlFacade;

        public FacadeManage() 
        {
            aideSqlFacade = new SqlFacade();
        }

        /// <summary>
        /// 多个数据库原型管理器
        /// </summary>
        public static FacadeManage Facade { get; private set; }

        public static void AddSql(IServiceCollection services) 
        {
            lock (lockObj) {
                Facade = new FacadeManage();
                services.AddObject(Facade);
            }
        }

        public static void UseSqlLog(ILoggerFactory loggerFactory)
        {
            lock (lockObj)
            {
                Facade.aideSqlFacade.SetLogger(loggerFactory.CreateLogger("ShareDB"));
            }
        }

        /// <summary>
        /// 数据对象
        /// </summary>
        public static SqlFacade AideSqlFacade
        {
            get
            {
                //if (_aideSqlFacade == null)
                //{
                //    lock (lockObj)
                //    {
                //        if (_aideSqlFacade == null) _aideSqlFacade = new SqlFacade();
                //    }
                //}
                return Facade.aideSqlFacade;
            }
        }
    }
}
