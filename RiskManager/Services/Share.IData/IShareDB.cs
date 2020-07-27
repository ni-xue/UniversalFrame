using System;
using System.Data;
using UniversalFrame.Core.SqlKernel;

namespace Share.IData
{
    /// <summary>
    /// 数据库连接接口
    /// </summary>
    public interface IShareDB
    {
        DataSet Get();

        DataTable Select();

        PagerSet GetPager();
    }
}
