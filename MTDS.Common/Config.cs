using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MTDS.Common
{
    public class Config
    { /// <summary>
        /// 平台连接字符串
        /// </summary>
        public static string PlatformConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["PlatformConnection"].ConnectionString;
            }
        }

        /// <summary>
        /// 系统初始密码
        /// </summary>
        public static string SystemInitPassword
        {
            get
            {
                string pass = ConfigurationManager.AppSettings["InitPassword"];
                return pass.IsNullOrEmpty() ? "123456" : pass.Trim();
            }
        }

        /// <summary>
        /// 得到当前主题
        /// </summary>
        public static string Theme
        {
            get
            {
                var cookie = System.Web.HttpContext.Current.Request.Cookies["theme_platform"];
                return cookie != null && !cookie.Value.IsNullOrEmpty() ? cookie.Value : "Gray";
            }
        }

        public static string Get(string name)
        {
            return ConfigurationManager.AppSettings.Get(name) ?? string.Empty;
        }

        public static void Set(string name, string value)
        {
            Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection appSection = appConfig.GetSection("appSettings") as AppSettingsSection;
            if (appSection == null) return;
            if (appSection.Settings[name] == null)
            {
                appSection.Settings.Add(name, value);
                appConfig.Save();
            }
            else
            {
                appSection.Settings.Remove(name);
                appSection.Settings.Add(name, value);
                appConfig.Save();
            }
        }
    }
}
