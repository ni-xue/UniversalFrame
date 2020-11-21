using Microsoft.Extensions.Logging;
using System;
using System.Data;
using Tool.SqlCore;

namespace Share.IData
{
    /// <summary>
    /// 数据库连接接口
    /// </summary>
    public interface IShareDB
    {
        void SetLogger(ILogger logger);//日志模块
        DataSet Get();

        DataTable Select();

        PagerSet GetPager();
    }
}
