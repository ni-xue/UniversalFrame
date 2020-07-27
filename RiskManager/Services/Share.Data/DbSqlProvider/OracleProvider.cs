using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using UniversalFrame.Core;
using UniversalFrame.Core.SqlKernel;

namespace Share.Data.DbSqlProvider
{
	/// <summary>
	/// 获取相关Oracle对应的数据类型
	/// </summary>
	public class OracleProvider : IDbProvider
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
					return OracleDbType.Boolean;
				case "System.DateTime":
					return OracleDbType.Date;
				case "System.Decimal":
					return OracleDbType.Decimal;
				case "System.Single":
					return OracleDbType.BinaryFloat;
				case "System.Double":
					return OracleDbType.Double;
				case "System.Byte[]":
					return OracleDbType.BFile;
				case "System.Int64":
					return OracleDbType.Long;
				case "System.Int32":
					return OracleDbType.Int32;
				case "System.String":
					return OracleDbType.NVarchar2;
				case "System.Int16":
					return OracleDbType.Int16;
				case "System.Byte":
					return OracleDbType.Byte;
				case "System.Guid":
					return OracleDbType.NVarchar2;
				case "System.TimeSpan":
					return OracleDbType.TimeStamp;
				case "System.Object":
					return OracleDbType.NVarchar2;
			}
			return OracleDbType.NVarchar2;
		}

		///// <summary>
		///// 根据<see cref="Type"/>类型获取对应的类型字符串
		///// </summary>
		///// <param name="netType"><see cref="Type"/>类型</param>
		///// <returns>类型字符串</returns>
		//public string ConvertToLocalDbTypeString(Type netType)
		//{
		//	string key = netType.ToString();
		//	switch (key)
		//	{
		//		case "System.Boolean":
		//			return "bit";
		//		case "System.DateTime":
		//			return "datetime";
		//		case "System.Decimal":
		//			return "decimal";
		//		case "System.Single":
		//			return "float";
		//		case "System.Double":
		//			return "float";
		//		case "System.Int64":
		//			return "bigint";
		//		case "System.Int32":
		//			return "int";
		//		case "System.String":
		//			return "nvarchar";
		//		case "System.Int16":
		//			return "smallint";
		//		case "System.Byte":
		//			return "tinyint";
		//		case "System.Guid":
		//			return "uniqueidentifier";
		//		case "System.TimeSpan":
		//			return "time";
		//		case "System.Byte[]":
		//			return "image";
		//		case "System.Object":
		//			return "sql_variant";
		//	}
		//	return null;
		//}

		/// <summary>
		/// 验证对象信息，并填充进<see cref="SqlCommand"/>集合中
		/// </summary>
		/// <param name="cmd">参数</param>
		public void DeriveParameters(IDbCommand cmd)
		{
			if (cmd is OracleCommand)
			{
				OracleCommandBuilder.DeriveParameters(cmd as OracleCommand);
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
			OracleParameter sqlParameter = paraName as OracleParameter;
			if (paraType != null)
			{
				sqlParameter.OracleDbType = this.ConvertToLocalDbType(paraType).ToVar<OracleDbType>();
			}
		}

		public PagerSet GetPagerSet(DbHelper dbHelper, PagerParameters pramsPager)
		{
			if (pramsPager.PageIndex < 1 || pramsPager.PageSize < 1) return null;

			throw new Exception("请自行实现分页逻辑。");
		}

		/// <summary>
		/// 就是这个符号'@'
		/// </summary>
		public string ParameterPrefix
		{
			get
			{
				return ":";
			}
		}
	}
}
