using System;
using System.IO;

namespace MTDS.Common
{
    public class UploadFileInfo
    {
        /// <summary>
        /// 得到上传文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetFileName(string filePath, string fileName)
        {
            while (File.Exists(filePath + fileName))
            {
                fileName = Path.GetFileNameWithoutExtension(fileName) + "_" + Tools.GetRandomString() +
                           Path.GetExtension(fileName);
            }
            return fileName;
        }

        /// <summary>
        /// 得到文件保存路径
        /// </summary>
        /// <returns></returns>
        public string GetFilePath(out string path1)
        {
            DateTime date = DateTime.Now;
            path1 = "/UploadFile/" + date.ToString("yyyyMM") + "/" + date.ToString("dd") + "/";
            return path1;
        }
    }
}
