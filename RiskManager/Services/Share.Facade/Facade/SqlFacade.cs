using Microsoft.Extensions.Logging;
using Share.Data.Factory;
using Share.IData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UniversalFrame.Core.SqlKernel;

namespace Share.Facade.Facade
{
    public class SqlFacade
    {
        #region Fields

        private readonly IShareDB ShareDB;

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlFacade()
        {
            ShareDB = DbFactory.GetShareDBProvider();
        }

        /// <summary>
        /// 系统日志操作
        /// </summary>
        /// <param name="logger"></param>
        public void SetLogger(ILogger logger) 
        {
            ShareDB.SetLogger(logger);
        }

        public DataSet Get()
        {
            return ShareDB.Get();
        }

        public DataTable Select() 
        {
            return ShareDB.Select();
        }

        public PagerSet GetPager()
        {
            return ShareDB.GetPager();
        }
    }
}
