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

        public DataTable Select()
        {
            return Database.Select("SELECT * FROM system  WHERE ID=@id OrDER by id desc", new { id = 1 });//查询，带条件
        }

        public void SqlTemplate() 
        {
            //查询
            System.Data.DataSet ds = Database.Query("SELECT * FROM system WHERE Id=@a OrDER by id desc", new { a = 1, b = "2", c = true, d = DateTime.Now, e = new byte[] { 60, 254 } });

            System.Data.SqlClient.SqlDataAdapter sqlDataReader = Database.CreateDataAdapter<System.Data.SqlClient.SqlDataAdapter>();

            //获取新创建的事物对象
            MySql.Data.MySqlClient.MySqlTransaction sqlConnection = Database.CreateTransaction<MySql.Data.MySqlClient.MySqlTransaction>();

            //事物提交修改操作
            DbTransResult trans = sqlConnection.ExecuteNonQuery(Database,
                Database.GetTextParameter("INSERT INTO system(id,key_en,key_cn,value)VALUES(@Id,'1','1','1')", new { Id = 4 })
                , Database.GetTextParameter("INSERT INTO system(id,key_en,key_cn,value)VALUES(@Id,'1','1','1')", new { Id = 5 })
                , Database.GetTextParameter("INSERT INTO system(id,key_en,key_cn,value)VALUES(@Id,'1','1','1')", new { Id = 4 }));

            //new SqlTextParameter("INSERT INTO [dbo].[Backpacks]([Name],[Capacity],[PropId])VALUES('1',1,1)")
            //    , new SqlTextParameter("INSERT INTO [dbo].[Backpacks]([Name],[Capacity],[PropId])VALUES('1',1,1)")
            //    , dbHelper.GetTextParameter("INSERT INTO [dbo].[Backpacks]([Id],[Name],[Capacity],[PropId])VALUES(@Id'1',1,1)", new { Id = 4 }));

            if (trans.Success)
            {
                //trans.Rows;//影响函数
            }
            //trans.Exception;//异常情况

            //通过才存储过程名称获取其需要的参数。
            System.Data.Common.DbParameter[] dbParameter = Database.GetSpParameterSet("spName");

            //直接使用事物的操作方式
            //Database.TransExecuteNonQuery();

            //新增
            //var da1 = Database.Insert("system", new { id = 4, key_en = "market", key_cn = "可用积分价值", value = "1" });
            //var da1_1 = Database.Insert<system>(new { id = 4, key_en = "market", key_cn = "可用积分价值", value = "1" });dao

            //修改
            //var da2 = Database.Update("system", "WHERE Id=@a", new { value = 5 }, new { a = 4 });
            //var da2_1 = Database.Update("system", "WHERE Id=1", new { value = 1 });
            //var da2_2 = Database.Update<system>("WHERE Id=1", new { value = 1 }, new { a = 1 });
            //var da2_3 = Database.Update<system>("Id=@Id", new { value = "5" }, new { Id = 4 });

            //查询
            //var da0_1 = Database.Select("SELECT * FROM system OrDER by id desc", new { a = 1 });

            //删除
            //var da3 = Database.Delete("system", "WHERE Id=@a", new { a = 1 });
            //var da3_1 = Database.Delete<system>("WHERE Id=@Id", new { Id = 4 });
            //var da3_2 = Database.Delete<system>("Id=@a", new { a = 1 });

            //存储过程
            //Database.GetMessage();
            //Database.GetMessageForDataSet();

            //查询获得单个值
            //Database.ExecuteScalar();
            //Database.ExecuteScalarToStr();

            //查询获得实体
            //Database.Query<system>();
            //Database.QueryList<system>();
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