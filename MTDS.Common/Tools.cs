using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Data;
using System.Reflection;

namespace MTDS.Common
{
    public class Tools
    {
        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static MemoryStream GetValidateImg(out string code)
        {
            code = GetValidateCode();
            Random rnd = new Random();
            System.Drawing.Bitmap img = new System.Drawing.Bitmap((int)Math.Ceiling((code.Length * 17.2)), 28);
            System.Drawing.Image bg = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("/Images/vcodebg.png"));
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
            System.Drawing.Font font = new System.Drawing.Font("Arial", 16, (System.Drawing.FontStyle.Regular | System.Drawing.FontStyle.Italic));
            System.Drawing.Font fontbg = new System.Drawing.Font("Arial", 16, (System.Drawing.FontStyle.Regular | System.Drawing.FontStyle.Italic));
            g.DrawImage(bg, 0, 0, new System.Drawing.Rectangle(rnd.Next(bg.Width - img.Width), rnd.Next(bg.Height - img.Height), img.Width, img.Height), System.Drawing.GraphicsUnit.Pixel);
            g.DrawString(code, fontbg, System.Drawing.Brushes.White, 0, 1);
            g.DrawString(code, font, System.Drawing.Brushes.Green, 0, 1);//字颜色

            //画图片的背景噪音线 
            int x = img.Width;
            int y1 = rnd.Next(5, img.Height);
            int y2 = rnd.Next(5, img.Height);
            g.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Green, 2), 1, y1, x - 2, y2);


            g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Transparent), 0, 10, img.Width - 1, img.Height - 1);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms;
        }

        /// <summary>
        /// 产生随机字符串
        /// </summary>
        /// <returns></returns>
        private static string GetValidateCode()
        {
            string checkCode = String.Empty;
            Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                int number = random.Next();
                checkCode += (char)('0' + (char)(number % 10));
            }
            return checkCode;
        }

        /// <summary>
        /// 获取远程浏览器端 IP 地址
        /// </summary>
        /// <returns>返回 IPv4 地址</returns>
        public static string GetIpAddress()
        {
            string userHostAddress = HttpContext.Current.Request.UserHostAddress;
            if (userHostAddress.IsNullOrEmpty())
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return userHostAddress;
        }

        /// <summary>
        /// 得到用户浏览器类型
        /// </summary>
        /// <returns></returns>
        public static string GetBrowse()
        {
            return HttpContext.Current.Request.Browser.Type;
        }

        /// <summary>
        /// 获取浏览器端操作系统名称
        /// </summary>
        /// <returns></returns>
        public static string GetOsName()
        {
            string osVersion = HttpContext.Current.Request.Browser.Platform;
            string userAgent = HttpContext.Current.Request.UserAgent;

            if (userAgent == null) return "UnKnown";

            if (userAgent.Contains("NT 6.2"))
            {
                osVersion = "Windows8";
            }
            if (userAgent.Contains("NT 6.1"))
            {
                osVersion = "Windows7";
            }
            else if (userAgent.Contains("NT 6.0"))
            {
                osVersion = "Windows Vista";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                osVersion = "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                osVersion = "WindowsXP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                osVersion = "Windows2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                osVersion = "WindowsNT4.0";
            }
            else if (userAgent.Contains("Me"))
            {
                osVersion = "WindowsMe";
            }
            else if (userAgent.Contains("98"))
            {
                osVersion = "Windows98";
            }
            else if (userAgent.Contains("95"))
            {
                osVersion = "Windows95";
            }
            else if (userAgent.Contains("Mac"))
            {
                osVersion = "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                osVersion = "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                osVersion = "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                osVersion = "SunOS";
            }
            return osVersion;
        }


        /// <summary>
        /// 得到分页HTML
        /// </summary>
        /// <param name="recordCount">记录总数</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="queryString">查询字符串</param>
        /// <returns></returns>
        public static string GetPagerHtml(long recordCount, int pageSize, int pageIndex, string queryString)
        {
            //得到共有多少页
            long pageCount = recordCount <= 0 ? 1 : recordCount % pageSize == 0 ? recordCount / pageSize : recordCount / pageSize + 1;

            long pNumber = pageIndex;
            if (pNumber < 1)
            {
                pNumber = 1;
            }
            else if (pNumber > pageCount)
            {
                pNumber = pageCount;
            }

            //如果只有一页则返回空分页字符串
            if (pageCount <= 1)
            {
                return "";
            }

            StringBuilder pagerHtml = new StringBuilder(1500);
            string jsFunctionName = string.Empty;

            //构造分页字符串
            int displaySize = 2;//中间显示的页数
            pagerHtml.Append("<div>");
            pagerHtml.Append("<span style='margin-right:15px;'>共 " + recordCount.ToString() + " 条  每页 <input type='text' id='tnt_count' title='输入数字可改变每页显示条数' class='pagertxt' onchange=\"javascript:_toPage_" + jsFunctionName + "(" + pNumber.ToString() + ",this.value);\" value='" + pageSize.ToString() + "' /> 条  ");
            pagerHtml.Append("转到 <input type='text' id='paernumbertext' title='输入数字可跳转页' value=\"" + pNumber.ToString() + "\" onchange=\"javascript:_toPage_" + jsFunctionName + "(this.value," + pageSize.ToString() + ");\" class='pagertxt'/> 页</span>");
            if (pNumber > 1)
                pagerHtml.Append("<a class=\"pager\" href=\"javascript:_toPage_" + jsFunctionName + "(" + (pNumber - 1).ToString() + "," + pageSize.ToString() + ");\"><span class=\"pagerarrow\">«</span></a>");
            //添加第一页
            if (pNumber >= displaySize / 2 + 3)
                pagerHtml.Append("<a class=\"pager\" href=\"javascript:_toPage_" + jsFunctionName + "(1," + pageSize.ToString() + ");\">1…</a>");
            else
                pagerHtml.Append("<a class=\"" + (1 == pNumber ? "pagercurrent" : "pager") + "\" href=\"javascript:_toPage_" + jsFunctionName + "(1," + pageSize.ToString() + ");\">1</a>");

            //添加中间数字
            long star = pNumber - displaySize / 2;
            long end = pNumber + displaySize / 2;
            if (star < 2)
            {
                end += 2 - star;
                star = 2;
            }
            if (end > pageCount - 1)
            {
                star -= end - (pageCount - 1);
                end = pageCount - 1;
            }
            if (star < 2)
                star = 2;

            for (long i = star; i <= end; i++)
                pagerHtml.Append("<a class=\"" + (i == pNumber ? "pagercurrent" : "pager") + "\" href=\"javascript:_toPage_" + jsFunctionName + "(" + i.ToString() + "," + pageSize.ToString() + ");\">" + i.ToString() + "</a>");
            //添加最后页
            if (pNumber <= pageCount - (displaySize / 2))
                pagerHtml.Append("<a class=\"pager\" href=\"javascript:_toPage_" + jsFunctionName + "(" + pageCount.ToString() + "," + pageSize.ToString() + ");\">…" + pageCount.ToString() + "</a>");
            else if (pageCount > 1)
                pagerHtml.Append("<a class=\"" + (pageCount == pNumber ? "pagercurrent" : "pager") + "\" href=\"javascript:_toPage_" + jsFunctionName + "(" + pageCount.ToString() + "," + pageSize.ToString() + ");\">" + pageCount.ToString() + "</a>");
            if (pNumber < pageCount)
                pagerHtml.Append("<a class=\"pager\" href=\"javascript:_toPage_" + jsFunctionName + "(" + (pNumber + 1).ToString() + "," + pageSize.ToString() + ");\"><span class=\"pagerarrow\">»</span></a>");
            pagerHtml.Append("</div>");
            //构造分页JS函数
            pagerHtml.Append("<script type=\"text/javascript\" lanuage=\"javascript\">");
            pagerHtml.Append("function _toPage_" + jsFunctionName + "(page,size){");
            pagerHtml.Append("var par=\"" + queryString + "\";");

            pagerHtml.Append("window.location= \"?pagenumber=\"+page+\"&pagesize=\"+size+par;");

            pagerHtml.Append("}");
            pagerHtml.Append("</script>");
            return pagerHtml.ToString();
        }

        /// <summary>
        /// 得到页尺寸
        /// </summary>
        /// <returns></returns>
        public static int GetPageSize(int defaultSize = 25)
        {
            string size = HttpContext.Current.Request["pagesize"] ?? defaultSize.ToString();
            int size1;
            return size.IsInt(out size1) ? size1 : defaultSize;
        }

        /// <summary>
        /// 得到页号
        /// </summary>
        /// <returns></returns>
        public static int GetPageIndex()
        {
            string number = HttpContext.Current.Request.Form["pagenumber"] ?? HttpContext.Current.Request.QueryString["pagenumber"] ?? "1";
            int number1;
            return number.IsInt(out number1) ? number1 : 1;
        }

        /// <summary>
        /// 得到列表项
        /// </summary>
        /// <param name="list">列表, 标题,值</param>
        /// <param name="value">默认值</param>
        /// <param name="showEmpty">是不显示空选项</param>
        /// <param name="emptyTitle">空选项显示标题</param>
        /// <returns></returns>
        public static System.Web.UI.WebControls.ListItem[] GetListItems(IList<string[]> list, string value, bool showEmpty = false, string emptyTitle = "")
        {
            List<System.Web.UI.WebControls.ListItem> items = new List<System.Web.UI.WebControls.ListItem>();
            if (showEmpty)
            {
                items.Add(new System.Web.UI.WebControls.ListItem(emptyTitle, ""));
            }
            foreach (var li in list)
            {
                if (li.Length < 2)
                {
                    continue;
                }
                var item = new System.Web.UI.WebControls.ListItem(li[0], li[1])
                {
                    Selected = !value.IsNullOrEmpty() && value == li[1] && !items.Exists(p => p.Selected)
                };
                items.Add(item);
            }
            return items.ToArray();
        }

        public static System.Web.UI.WebControls.ListItem[] GetListItems(IList<string> list, string value, bool showEmpty = false, string emptyTitle = "")
        {
            List<string[]> newList = list.Select(str => new[] { str, str }).ToList();
            return GetListItems(newList, value, showEmpty, emptyTitle);
        }

        /// <summary>
        /// 将服务器控件列表项转换为select列表项
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string GetOptionsString(System.Web.UI.WebControls.ListItem[] items)
        {
            StringBuilder options = new StringBuilder(items.Length * 50);
            foreach (var item in items)
            {
                options.AppendFormat("<option value=\"{0}\" {1}>", item.Value.Replace("\"", "'"), item.Selected ? "selected=\"selected\"" : "");
                options.Append(item.Text);
                options.Append("</option>");
            }
            return options.ToString();
        }

        /// <summary>
        /// 将服务器控件列表项转换为Checkbox项
        /// </summary>
        /// <param name="items"></param>
        /// <param name="name"></param>
        /// <param name="values"></param>
        /// <param name="otherAttr"></param>
        /// <returns></returns>
        public static string GetCheckBoxString(System.Web.UI.WebControls.ListItem[] items, string name, string[] values, string otherAttr = "")
        {
            StringBuilder options = new StringBuilder(items.Length * 50);
            foreach (var item in items)
            {
                string tempid = Guid.NewGuid().ToString("N");
                options.AppendFormat("<input type=\"checkbox\" value=\"{0}\" {1} id=\"{2}\" name=\"{3}\" {4} style=\"vertical-align:middle\" />",
                    item.Value.Replace("\"", "'"),
                    values != null && values.Contains(item.Value) ? "checked=\"checked\"" : "",
                    string.Format("{0}_{1}", name, tempid),
                    name,
                    otherAttr
                    );
                options.AppendFormat("<label style=\"vertical-align:middle;margin-right:2px;\" for=\"{0}\">", string.Format("{0}_{1}", name, tempid));
                options.Append(item.Text);
                options.Append("</label>");
            }
            return options.ToString();
        }

        /// <summary>
        /// 将服务器控件列表项转换为Radio项
        /// </summary>
        /// <param name="items"></param>
        /// <param name="name"></param>
        /// <param name="otherAttr"></param>
        /// <returns></returns>
        public static string GetRadioString(System.Web.UI.WebControls.ListItem[] items, string name, string otherAttr = "")
        {
            StringBuilder options = new StringBuilder(items.Length * 50);
            foreach (var item in items)
            {
                string tempid = Guid.NewGuid().ToString("N");
                options.AppendFormat("<input type=\"radio\" value=\"{0}\" {1} id=\"{2}\" name=\"{3}\" {4} style=\"vertical-align:middle\" />",
                    item.Value.Replace("\"", "'"),
                    item.Selected ? "checked=\"checked\"" : "",
                    string.Format("{0}_{1}", name, tempid),
                    name,
                    otherAttr
                    );
                options.AppendFormat("<label style=\"vertical-align:middle;margin-right:2px;\" for=\"{0}\">", string.Format("{0}_{1}", name, tempid));
                options.Append(item.Text);
                options.Append("</label>");
            }
            return options.ToString();
        }
        /// <summary>
        /// 得到是否选择项
        /// </summary>
        /// <param name="value"></param>
        /// <param name="showEmpty"></param>
        /// <param name="emptyString"></param>
        /// <returns></returns>
        public static System.Web.UI.WebControls.ListItem[] GetYesNoListItems(string value, bool showEmpty = false, string emptyString = "")
        {
            List<string[]> list = new List<string[]> { new[] { "是", "1" }, new[] { "否", "0" } };
            return GetListItems(list, value, showEmpty, emptyString);
        }

        /// <summary>
        /// 得到sql语句in里的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="isSingleQuotes"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string GetSqlInString(string str, bool isSingleQuotes = true, string split = ",")
        {
            string[] strArray = str.Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries);

            return GetSqlInString(strArray, isSingleQuotes);
        }

        /// <summary>
        /// 得到sql语句in里的字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strArray"></param>
        /// <param name="isSingleQuotes">是否加单引号，字符串要加，数字不加</param>
        /// <returns></returns>
        public static string GetSqlInString<T>(T[] strArray, bool isSingleQuotes = true)
        {
            StringBuilder inStr = new StringBuilder(strArray.Length * 40);
            foreach (var s in strArray)
            {
                if (s.ToString().IsNullOrEmpty())
                {
                    continue;
                }
                if (isSingleQuotes)
                {
                    inStr.Append("'");
                }
                inStr.Append(s.ToString().Trim());
                if (isSingleQuotes)
                {
                    inStr.Append("'");
                }
                inStr.Append(",");

            }
            return inStr.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 产生不重复随机数
        /// </summary>
        /// <param name="count">共产生多少随机数</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>int[]数组</returns>
        public static int[] GetRandomNum(int count, int minValue, int maxValue)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            int length = maxValue - minValue + 1;
            byte[] keys = new byte[length];
            rnd.NextBytes(keys);
            int[] items = new int[length];
            for (int i = 0; i < length; i++)
            {
                items[i] = i + minValue;
            }
            Array.Sort(keys, items);
            int[] result = new int[count];
            Array.Copy(items, result, count);
            return result;
        }

        /// <summary>
        /// 产生随机字符串
        /// </summary>
        /// <returns>字符串位数</returns> 
        public static string GetRandomString(int length = 5)
        {
            string checkCode = String.Empty;
            Random random = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < length + 1; i++)
            {
                int number = random.Next();
                char code;
                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));
                checkCode += code.ToString();
            }
            return checkCode;
        }

        /// <summary>
        /// 产生随机字母
        /// </summary>
        /// <returns>字符串位数</returns>
        public static string GetRandomLetter(int length = 2)
        {
            string checkCode = String.Empty;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < length; i++)
            {
                int number = random.Next();
                char code = (char)('A' + (char)(number % 26));
                checkCode += code.ToString();
            }
            return checkCode;
        }

        /// <summary>
        /// 得到一个文件的大小
        /// </summary>
        /// <returns></returns>
        public static string GetFileSize(string file)
        {
            if (!File.Exists(file))
            {
                return "";
            }
            FileInfo fi = new FileInfo(file);

            return fi.Length.ToString("###,###");
        }

        /// <summary>
        /// 将对象转换为Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                StringBuilder sb = new StringBuilder();
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                return sb.ToString();
            }
        }

        /// <summary>
        /// 计算工作日（只管周六日）
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="dayType"></param>
        /// <returns></returns>
        public static int GetWorkDays(DateTime dt1, DateTime dt2, string dayType)
        {
            int dayCount = 0;

            TimeSpan tsDiffer = dt2.Date - dt1.Date;

            if (dayType == "工作日")
            {
                int intDiffer = tsDiffer.Days;
                for (int i = 0; i < intDiffer; i++)
                {
                    DateTime dtTemp = dt1.Date.AddDays(i);
                    if ((dtTemp.DayOfWeek != DayOfWeek.Sunday) && (dtTemp.DayOfWeek != DayOfWeek.Saturday))
                    {
                        dayCount++;
                    }

                }
                return dayCount;
            }
            else
            {
                return tsDiffer.Days;
            }
        }

        /// <summary>
        /// 计算从dt1增加多少天后和日期
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dayCount"></param>
        /// <param name="dayType">工作日/天</param>
        /// <returns></returns>
        public static DateTime ComputeWorkDays(DateTime dt1, int dayCount, string dayType)
        {
            if (dayType == "天")
            {
                return dt1.AddDays(dayCount);
            }

            if (dayType == "工作日")
            {
                int actualDayCount = 0;
                DateTime testDate = dt1;
                while (actualDayCount < dayCount)
                {
                    testDate = testDate.AddDays(1);
                    if ((testDate.DayOfWeek != DayOfWeek.Sunday) && (testDate.DayOfWeek != DayOfWeek.Saturday))
                    {
                        actualDayCount++;
                    }
                }

                return testDate;
            }

            return dt1;
        }

        //去除名称首字母
        public static string DitLetter(string input)
        {
            return Regex.Replace(input, @"^[a-zA-Z$]+", "");
        }

        //获取本地的IP地址lizhihong
        public static string GetLocalIPv4()
        {
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                    break;
                }
            }
            return AddressIP;
        }

        //测试IP和Port的连通性lizhihong
        public static bool TestIpConn(string ip, string port)
        {
            bool ok = false;
            try
            {
                IPAddress tmpIp = IPAddress.Parse(ip);
                int tmpPort = int.Parse(port);
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IAsyncResult connResult = sock.BeginConnect(tmpIp, tmpPort, null, null);
                connResult.AsyncWaitHandle.WaitOne(1000, true);  //等待1秒
                ok = connResult.IsCompleted;
                sock.Close();
            }
            catch
            {
                ok = false;
            }
            return ok;
        }

        //根据年月，返回开始日期
        public static bool GetStartTime(string years, string months, out DateTime start)
        {
            start = DateTime.Now;
            //年
            int year = DateTime.Now.Year;
            try
            {
                year = int.Parse(years);
            }
            catch
            {
                return false;
            }
            //月
            int month = DateTime.Now.Month;
            if (months.Trim().IsEmpty())
            {
                month = 1;
            }
            else
            {
                try
                {
                    month = int.Parse(months);
                }
                catch
                {
                    return false;
                }
            }
            if ((year < 1949) || (month < 1) || (month > 12))
            {
                return false;
            }
            start = new DateTime(year, month, 1, 0, 0, 0);
            return true;
        }

        //根据年月，返回结束日期
        public static bool GetEndTime(string years, string months, out DateTime end)
        {
            end = DateTime.Now;
            //年
            int year = DateTime.Now.Year;
            try
            {
                year = int.Parse(years);
            }
            catch
            {
                return false;
            }
            //月
            int month = DateTime.Now.Month;
            if (months.Trim().IsEmpty())
            {
                month = 12;
            }
            else
            {
                try
                {
                    month = int.Parse(months);
                }
                catch
                {
                    return false;
                }
            }
            if ((year < 1949) || (month < 1) || (month > 12))
            {
                return false;
            }
            if (month == 12)
            {
                year = year + 1;
                month = 1;
            }
            else
            {
                month = month + 1;
            }
            end = new DateTime(year, month, 1, 0, 0, 0).AddSeconds(-1);
            return true;
        }

        /// <summary>    
        /// 将集合类转换成DataTable    
        /// </summary>    
        /// <param name="list">集合</param>    
        /// <returns></returns>    
        public static DataTable ToDataTableTow(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize">一页多少条</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="totalCount">总条数</param>
        /// <returns></returns>
        public static string ShowPageNavigate(int pageSize, int currentPage, int totalCount)
        {
            string redirectTo = "";
            pageSize = pageSize == 0 ? 3 : pageSize;
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数
            var output = new StringBuilder();
            if (totalPages > 1)
            {
                if (currentPage != 1)
                {//处理首页连接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex=1&pageSize={1}'>首页</a> ", redirectTo, pageSize);
                }
                if (currentPage > 1)
                {//处理上一页的连接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>上一页</a> ", redirectTo, currentPage - 1, pageSize);
                }
                else
                {
                    // output.Append("<span class='pageLink'>上一页</span>");
                }

                output.Append(" ");
                int currint = 3;//20161201 Updated by Nancy(from '5' to '3')
                for (int i = 0; i <= 6; i++)//20161201 Updated by Nancy(from '10' to '6')
                {//一共最多显示10个页码，前面5个，后面5个
                    if ((currentPage + i - currint) >= 1 && (currentPage + i - currint) <= totalPages)
                    {
                        if (currint == i)
                        {//当前页处理
                            //output.Append(string.Format("[{0}]", currentPage));
                            output.AppendFormat("<a class='cpb pageLink' href='{0}?pageIndex={1}&pageSize={2}'>{3}</a> ", redirectTo, currentPage, pageSize, currentPage);
                        }
                        else
                        {//一般页处理
                            output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>{3}</a> ", redirectTo, currentPage + i - currint, pageSize, currentPage + i - currint);
                        }
                    }
                    output.Append(" ");
                }
                if (currentPage < totalPages)
                {//处理下一页的链接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>下一页</a> ", redirectTo, currentPage + 1, pageSize);
                }
                else
                {
                    //output.Append("<span class='pageLink'>下一页</span>");
                }
                output.Append(" ");
                if (currentPage != totalPages)
                {
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>末页</a> ", redirectTo, totalPages, pageSize);
                }
                output.Append(" ");
            }
            output.AppendFormat("<span class='col-sm-4 page_num' style=\"width:20%\">第{0}页 / 共{1}页</span>", currentPage, totalPages);//这个统计加不加都行
            return output.ToString();
        }

        /// <summary>
        /// 获取设备不正常运转原因列表
        /// </summary>
        /// <returns></returns>
        public static List<Reason> GetReasonList()
        {
            string strReslut = "";
            string strReason = ConfigurationManager.AppSettings["Reason"];
            List<Reason> reasonList = new List<Reason>();
            Reason reason;

            if (!strReason.IsNullOrEmpty())
            {
                string[] reasons = strReason.Split(';');
                string[] paramList;
                if (reasons.Length > 0)
                {
                    foreach (var r in reasons)
                    {
                        if (!r.IsNullOrEmpty())
                        {
                            paramList = r.Split(',');
                            if (paramList.Length > 0)
                            {
                                reason=new Reason();
                                reason.Index = paramList[0];
                                reason.Con = paramList[1];
                                reasonList.Add(reason);
                            }
                        }
                    }
                }
            }

            return reasonList;
        }
    }
}
