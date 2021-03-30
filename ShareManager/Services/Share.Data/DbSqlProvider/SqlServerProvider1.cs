using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Text;
using Tool;
using Tool.SqlCore;

namespace Share.Data.DbSqlProvider
{
	/// <summary>
	/// 获取相关SqlServer对应的数据类型(Microsoft.Data.SqlClient)
	/// </summary>
	public class SqlServerProvider1 : IDbProvider
	{
		/// <summary>
		/// 根据<see cref="Type"/>类型获取对应的类型
		/// </summary>
		/// <param name="t"><see cref="Type"/>类型</param>
		/// <returns>类型</returns>
		public Enum ConvertToLocalDbType(Type t)
		{
			string key = t.ToString();
			switch (key)
			{
				case "System.Boolean":
					return SqlDbType.Bit;
				case "System.DateTime":
					return SqlDbType.DateTime;
				case "System.Decimal":
					return SqlDbType.Decimal;
				case "System.Single":
					return SqlDbType.Float;
				case "System.Double":
					return SqlDbType.Float;
				case "System.Byte[]":
					return SqlDbType.Image;
				case "System.Int64":
					return SqlDbType.BigInt;
				case "System.Int32":
					return SqlDbType.Int;
				case "System.String":
					return SqlDbType.NVarChar;
				case "System.Int16":
					return SqlDbType.SmallInt;
				case "System.Byte":
					return SqlDbType.TinyInt;
				case "System.Guid":
					return SqlDbType.UniqueIdentifier;
				case "System.TimeSpan":
					return SqlDbType.Time;
				case "System.Object":
					return SqlDbType.Variant;
			}
			return SqlDbType.Int;
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
			if (cmd is SqlCommand)
			{
				SqlCommandBuilder.DeriveParameters(cmd as SqlCommand);
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
		public void GetParam(ref DbParameter paraName, object paraValue, ParameterDirection direction, Type paraType, string sourceColumn, int size)
		{
			//SqlParameter sqlParameter = new SqlParameter
			SqlParameter sqlParameter = paraName as SqlParameter;
			if (paraType != null)
			{
				sqlParameter.SqlDbType = this.ConvertToLocalDbType(paraType).ToVar<SqlDbType>();
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
