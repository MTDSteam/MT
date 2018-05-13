﻿using System.Web;

namespace MTDS.Common
{
    public class FileHelper
    {
        public FileHelper()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 获取上传目录
        /// </summary>
        /// <returns></returns>
        public static string GetUploadPath()
        {
            string path = HttpContext.Current.Server.MapPath("~/");
            string dirname = GetDirName();
            string uploadDir = path + "\\" + dirname;
            CreateDir(uploadDir);
            return uploadDir;
        }
        /// <summary>
        /// 获取临时目录
        /// </summary>
        /// <returns></returns>
        public static string GetTempPath()
        {
            string path = HttpContext.Current.Server.MapPath("~/");
            string dirname = GetTempDirName();
            string uploadDir = path + "\\" + dirname;
            CreateDir(uploadDir);
            return uploadDir;
        }
        private static string GetDirName()
        {
            return System.Configuration.ConfigurationManager.AppSettings["uploaddir"];
        }
        private static string GetTempDirName()
        {
            return System.Configuration.ConfigurationManager.AppSettings["tempdir"];
        }
        public static void CreateDir(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }

    }
}
