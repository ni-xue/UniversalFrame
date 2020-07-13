using Share.IData;
using System;
using System.Data;
using UniversalFrame.Core.SqlKernel;

namespace Share.Data
{
    public class ShareDB : BaseDataProvider, IShareDB
    {
        public ShareDB(string connectionString, DbProviderType dbProviderType, IDbProvider dbProvider) : base(connectionString, dbProviderType, dbProvider)
        {
        }

        public DataSet Get() 
        {
            return Database.Query("SELECT * FROM system OrDER by id desc", new { a = 1, b = 2, c = 3, d = 4 });
        }

        public PagerSet GetPager()
        {
            return GetPagerSet(new PagerParameters()
            {
                IsSql = true,
                Table = "SELECT * FROM system OrDER by id desc"
            }) ;
        }
    }
}