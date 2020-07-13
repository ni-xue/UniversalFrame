using Share.IData;
using System;
using System.Collections.Generic;
using System.Text;
using UniversalFrame.Core.SqlKernel;
using Share.Data.DbSqlProvider;
using UniversalFrame.Core.Utils;

namespace Share.Data.Factory
{
    /// <summary>
    /// 数据访问创建工厂
    /// </summary>
    public class DbFactory
    {

        /// <summary>
        /// 创建访问库对象实例
        /// </summary>
        /// <returns></returns>
        public static IShareDB GetShareDBProvider()
        {
            return ProxyFactory.CreateInstance<ShareDB>(AppSettings.Get("ShareDB"), DbProviderType.MySql, new MySqlProvider());
            //return new ShareDB(AppSettings.Get("ShareDB"), DbProviderType.MySql, new MySqlProvider()); //这样也行,第一种可能容易架构吧。
        }

    }
}
