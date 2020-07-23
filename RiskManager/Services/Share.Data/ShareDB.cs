using Share.IData;
using System;
using System.Data;
using UniversalFrame.Core.SqlKernel;

namespace Share.Data
{
    /// <summary>
    /// BaseDataProvider 为DbHelper 的功能增强类，可以帮助高效实现SQL。
    /// </summary>
    public class ShareDB : BaseDataProvider, IShareDB
    {
        private readonly ITableProvider aideIsystem;//开始创建的表操作对象

        public ShareDB(string connectionString, DbProviderType dbProviderType, IDbProvider dbProvider) : base(connectionString, dbProviderType, dbProvider)
        {
            aideIsystem = GetTableProvider("system");//注册生成对指定表的操作对象
        }

        public DataSet Get() 
        {
            return Database.Query("SELECT * FROM system  WHERE ID=@id OrDER by id desc", new { id = 1 });//查询，带条件
        }

        public PagerSet GetPager()
        {
            return GetPagerSet(new PagerParameters()//分页内置对象，简单好管理
            {
                IsSql = true,
                Table = "SELECT * FROM system OrDER by id desc"
            }) ;
        }

        public int Insert()
        {
            return aideIsystem.Insert(new { /* 里面放赋值的参数 */ });
        }

        public int Update()
        {
            return aideIsystem.Update(new { /* 里面放赋值的参数 */ }, "条件");
        }

        public int Delete()
        {
            return aideIsystem.Delete("条件");
        }
    }
}