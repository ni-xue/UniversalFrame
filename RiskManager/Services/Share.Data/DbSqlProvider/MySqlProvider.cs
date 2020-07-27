using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using UniversalFrame.Core.SqlKernel;
using System.Data.Common;
using UniversalFrame.Core;

namespace Share.Data.DbSqlProvider
{
    /// <summary>
    /// 获取相关MySql对应的数据类型
    /// </summary>
    public class MySqlProvider : IDbProvider
    {
        /// <summary>
        /// 根据<see cref="Type"/>类型获取对应的类型
        /// </summary>
        /// <param name="t"><see cref="Type"/>类型</param>
        /// <returns>类型</returns>
        public object ConvertToLocalDbType(Type t)
        {
            string key = t.ToString();
            switch (key)
            {
                case "System.Boolean":
                    return MySqlDbType.Bit;
                case "System.DateTime":
                    return MySqlDbType.DateTime;
                case "System.Decimal":
                    return MySqlDbType.Decimal;
                case "System.Single":
                    return MySqlDbType.Float;
                case "System.Double":
                    return MySqlDbType.Double;
                case "System.Byte[]":
                    return MySqlDbType.Binary;
                case "System.Int64":
                    return MySqlDbType.Int64;
                case "System.Int32":
                    return MySqlDbType.Int32;
                case "System.String":
                    return MySqlDbType.String;
                case "System.Int16":
                    return MySqlDbType.Int16;
                case "System.Byte":
                    return MySqlDbType.Byte;
                case "System.Guid":
                    return SqlDbType.UniqueIdentifier;
                case "System.TimeSpan":
                    return MySqlDbType.Time;
                case "System.Object":
                    return MySqlDbType.Binary;
            }
            return MySqlDbType.Int32;
        }

        ///// <summary>
        ///// 根据<see cref="Type"/>类型获取对应的类型字符串
        ///// </summary>
        ///// <param name="netType"><see cref="Type"/>类型</param>
        ///// <returns>类型字符串</returns>
        //public string ConvertToLocalDbTypeString(Type netType)
        //{
        //    string key = netType.ToString();
        //    switch (key)
        //    {
        //        case "System.Boolean":
        //            return "bit";
        //        case "System.DateTime":
        //            return "datetime";
        //        case "System.Decimal":
        //            return "decimal";
        //        case "System.Single":
        //            return "float";
        //        case "System.Double":
        //            return "float";
        //        case "System.Int64":
        //            return "bigint";
        //        case "System.Int32":
        //            return "int";
        //        case "System.String":
        //            return "nvarchar";
        //        case "System.Int16":
        //            return "smallint";
        //        case "System.Byte":
        //            return "tinyint";
        //        case "System.Guid":
        //            return "uniqueidentifier";
        //        case "System.TimeSpan":
        //            return "time";
        //        case "System.Byte[]":
        //            return "image";
        //        case "System.Object":
        //            return "sql_variant";
        //    }
        //    return null;
        //}

        /// <summary>
        /// 验证对象信息，并填充进<see cref="SqlCommand"/>集合中
        /// </summary>
        /// <param name="cmd">参数</param>
        public void DeriveParameters(IDbCommand cmd)
        {
            //MySql.Data.MySqlClient.MySqlCommand
            if (cmd is MySqlCommand)//SqlCommand
            {
                //SqlCommandBuilder.DeriveParameters(cmd as SqlCommand);
                MySqlCommandBuilder.DeriveParameters(cmd as MySqlCommand);
            }
        }

        /// <summary>
        /// 获取插入数据的主键ID（SQL）
        /// </summary>
        /// <returns></returns>
        public string GetLastIdSql()
        {
            return "SELECT SCOPE_IDENTITY()";
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="paraName">键</param>
        /// <param name="paraValue">值</param>
        /// <param name="direction">指定查询内的有关 <see cref="DataSet"/> 的参数的类型。</param>
        /// <param name="paraType">类型</param>
        /// <param name="sourceColumn">源列</param>
        /// <param name="size">大小</param>
        /// <returns></returns>
        public void GetParam(DbParameter paraName, object paraValue, ParameterDirection direction, Type paraType, string sourceColumn, int size)
        {
            //SqlParameter sqlParameter = new SqlParameter
            MySqlParameter sqlParameter = paraName as MySqlParameter;
            if (paraType != null)
            {
                sqlParameter.MySqlDbType = this.ConvertToLocalDbType(paraType).ToVar<MySqlDbType>();
            }
        }

        public PagerSet GetPagerSet(DbHelper dbHelper, PagerParameters pramsPager)
        {
            if (pramsPager.PageIndex < 1 || pramsPager.PageSize < 1) return null;
            if (pramsPager.IsSql)
            {
                string RecordCountStr = "SELECT COUNT(*) FROM (" + pramsPager.Table + ") AS A";

                int recordCount = dbHelper.ExecuteScalar(CommandType.Text, RecordCountStr).ToVar<int>();
                int pageCount = recordCount / pramsPager.PageSize;
                if (pageCount * pramsPager.PageSize < recordCount) pageCount++;
                pramsPager.PageIndex = pramsPager.PageIndex <= 0 ? 1 : pramsPager.PageIndex;
                string pageSetStr = "SELECT * FROM (" + pramsPager.Table + ") AS A limit " + (pramsPager.PageIndex - 1) * pramsPager.PageSize + "," + pramsPager.PageSize;

                DataSet pageSet = dbHelper.Query(pageSetStr);

                return new PagerSet(pramsPager.PageIndex, pramsPager.PageSize, pageCount, recordCount, pageSet);
            }
            else
            {
                string RecordCountStr = "SELECT COUNT(*) FROM " + pramsPager.Table + " WHERE " + pramsPager.WhereStr + " " + pramsPager.PKey;

                int recordCount = dbHelper.ExecuteScalar(CommandType.Text, RecordCountStr).ToVar<int>();
                int pageCount = recordCount / pramsPager.PageSize;
                if (pageCount * pramsPager.PageSize < recordCount) pageCount++;
                pramsPager.PageIndex = pramsPager.PageIndex <= 0 ? 1 : pramsPager.PageIndex;
                string pageSetStr = "SELECT * FROM " + pramsPager.Table + " WHERE " + pramsPager.WhereStr + " " + pramsPager.PKey + " limit " + (pramsPager.PageIndex - 1) * pramsPager.PageSize + "," + pramsPager.PageSize;

                DataSet pageSet = dbHelper.Query(pageSetStr);

                return new PagerSet(pramsPager.PageIndex, pramsPager.PageSize, pageCount, recordCount, pageSet);
            }
        }

        /// <summary>
        /// 就是这个符号'@'
        /// </summary>
        public string ParameterPrefix
        {
            get
            {
                return "@";
            }
        }
    }
}
