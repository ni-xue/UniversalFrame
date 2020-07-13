using System;
using System.Collections.Generic;
using System.Text;
using UniversalFrame.Core;

namespace Share.Facade
{
    /// <summary>
    /// Ajax异步请求返回数据类
    /// </summary>
    public class AjaxJson
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code
        {
            get;
            set;
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string msg
        {
            get;
            set;
        }

        /// <summary>
        /// 数据项列表
        /// </summary>
        public Dictionary<string, object> data { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AjaxJson()
        {
            code = 0;
            msg = "";
            data = new Dictionary<string, object>();
        }

        /// <summary>
        /// 为数据项赋值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetPage(UniversalFrame.Core.SqlKernel.PagerSet pagerSet)
        {
            SetDataItem("PageCount", pagerSet.PageCount);

            SetDataItem("RecordCount", pagerSet.RecordCount);

            SetDataItem("PageIndex", pagerSet.PageIndex);

            SetDataItem("PageSize", pagerSet.PageSize);
        }

        /// <summary>
        /// 为数据项赋值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetDataItem(string key, object value)
        {
            if (data.ContainsKey(key))
            {
                data[key] = value;
            }
            else
            {
                data.Add(key, value);
            }
        }

        /// <summary>
        /// 为数据项赋值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="IsJson">是否是Json字符串</param>
        public void SetDataItem(string key, object value, bool IsJson)
        {
            if (IsJson)
            {
                value = value.ToString().JsonDynamic();
            }

            if (data.ContainsKey(key))
            {
                data[key] = value;
            }
            else
            {
                data.Add(key, value);
            }
        }

        /// <summary>
        /// 为数据键值对赋值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetIDictionary(IDictionary<string, object> keyValues)
        {
            foreach (var keyValue in keyValues)
            {
                if (data.ContainsKey(keyValue.Key))
                {
                    data[keyValue.Key] = keyValue.Value;
                }
                else
                {
                    data.Add(keyValue.Key, keyValue.Value);
                }
            }
        }

        /// <summary>
        /// 获取数据项值
        /// </summary>
        /// <param name="key">键</param>
        public object GetDataItemValue(string key)
        {
            return data[key];
        }

        /// <summary>
        /// 序列化AjaxJson
        /// </summary>
        /// <returns>Json字符串</returns>
        public string SerializeToJson()
        {
            return this.ToJson();
        }
    }
}
