namespace MTDS.Common
{
    public class Keys
    {
        /// <summary>
        /// SESSION键
        /// </summary>
        public enum SessionKeys
        {
            /// <summary>
            /// 登录用户ID
            /// </summary>
            UserId,
            /// <summary>
            /// 用户登录唯一ID
            /// </summary>
            UserUniqueId,
            /// <summary>
            /// 登录用户
            /// </summary>
            User,
            /// <summary>
            /// 用户角色编号
            /// </summary>
            RoleId,
            /// <summary>
            /// 用户角色
            /// </summary>
            Role,
            /// <summary>
            /// 验证码
            /// </summary>
            ValidateCode,
            /// <summary>
            /// 是否要检查验证码
            /// </summary>
            IsValidateCode,
            /// <summary>
            /// 主题
            /// </summary>
            Theme
        }

        /// <summary>
        /// COOKIE键
        /// </summary>
        public enum CookieKeys
        {
            /// <summary>
            /// 用户账户
            /// </summary>
            Account,
            /// <summary>
            /// 用户密码
            /// </summary>
            Password,
            /// <summary>
            /// 是否记住密码
            /// </summary>
            IsRePassword,

        }

        /// <summary>
        /// 缓存键
        /// </summary>
        public enum CacheKeys
        {
            /// <summary>
            /// 角色所有应用
            /// </summary>
            RoleApp,
            /// <summary>
            /// 个人所有应用
            /// </summary>
            UsersApp,
            /// <summary>
            /// 应用程序库
            /// </summary>
            AppLibrary,
            /// <summary>
            /// 数据字典
            /// </summary>
            Dictionary,
            /// <summary>
            /// 数据库连接
            /// </summary>
            DbConnnections,
            /// <summary>
            /// 在线用户
            /// </summary>
            OnlineUsers
        }
    }
}
