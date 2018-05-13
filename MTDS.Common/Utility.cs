using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32;

namespace MTDS.Common
{
    /// <summary>
    /// 常用工具类
    /// </summary>
    public sealed class Utility
    {
        /// <summary>
        /// 获得文件夹内所有文件，包括子文件夹
        /// </summary>
        /// <param name="dirpath"></param>
        /// <returns></returns>
        public static List<FileInfo> GetFiles(string dirpath)
        {
            string[] files = Directory.GetFiles(dirpath);

            List<FileInfo> lstFile = files.Select(file => new FileInfo(file)).ToList();

            //获取子文件夹内的文件
            string[] dirs = Directory.GetDirectories(dirpath);
            foreach (string dir in dirs)
            {
                List<FileInfo> dirFiles = GetFiles(dir);
                if (dirFiles.Count > 0)
                    lstFile.AddRange(dirFiles);
            }

            return lstFile;
        }

        /// <summary>
        /// 检查文件夹内是否还有子文件夹
        /// </summary>
        /// <param name="dirpath"></param>
        /// <returns></returns>
        public static bool HasFolders(string dirpath)
        {
            string[] dirs = Directory.GetDirectories(dirpath);
            return dirs.Length > 0;
        }

        //C#判断操作系统是否为64位
        public static bool Is64Bit
        {
            get { return (Environment.Is64BitOperatingSystem); }
        }

        /// <summary>
        /// 判断是否为管理员权限
        /// </summary>
        /// <returns></returns>
        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity == null) return false;
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// 设置路径的完全访问权限
        /// </summary>
        /// <param name="directoryPath"></param>
        public static void SetFullAuthority(string directoryPath)
        {
            DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(directoryPath));  
            DirectorySecurity dirSecurity = di.GetAccessControl();  
            dirSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));  
            //dirSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));  
            di.SetAccessControl(dirSecurity);  
        }
    }
}
