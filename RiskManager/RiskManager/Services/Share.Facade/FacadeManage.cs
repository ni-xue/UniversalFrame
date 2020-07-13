using Share.Facade.Facade;
using System;

namespace Share.Facade
{
    public class FacadeManage
    {
        private static readonly object lockObj = new object();

        ///// <summary>
        ///// 配置库逻辑
        ///// </summary>
        private static volatile SqlFacade _aideSqlFacade;

        /// <summary>
        /// 数据对象
        /// </summary>
        public static SqlFacade AideSqlFacade
        {
            get
            {
                if (_aideSqlFacade == null)
                {
                    lock (lockObj)
                    {
                        if (_aideSqlFacade == null) _aideSqlFacade = new SqlFacade();
                    }
                }
                return _aideSqlFacade;
            }
        }
    }
}
