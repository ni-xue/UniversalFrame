using System;

namespace Share.Entity
{
    /// <summary>
    /// 用户对象
    /// </summary>
    public class ShareUser
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string LoginName { set; get; }

        /// <summary>
        /// 密码
        /// </summary>
        public string LoginPwd { set; get; }
    }
}
