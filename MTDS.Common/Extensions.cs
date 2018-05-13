using System;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections.Generic;
using Microsoft.International.Converters.PinYinConverter;

namespace MTDS.Common
{

    /// <summary>
    /// 扩展方法类
    /// </summary>
    public static class MyExtensions
    {
        public static bool IsMyDecimal(this string str)
        {
            str = str.Replace(",", "");
            decimal test;
            return decimal.TryParse(str, out test);
        }

        public static bool IsMyDecimal(this string str, out decimal test)
        {
            str = str.Replace(",", "");
            return decimal.TryParse(str, out test);
        }

        public static decimal ToDecimal(this string str, int decimals)
        {
            if (str.IsNullOrEmpty()) return 0;
            str = str.Replace(",", "");
            decimal test;
            return decimal.TryParse(str, out test) ? decimal.Round(test, decimals, MidpointRounding.AwayFromZero) : 0;
        }

        public static decimal ToDecimal(this string str)
        {
            if (str.IsNullOrEmpty()) return 0;
            str = str.Replace(",", "");
            decimal test;
            return decimal.TryParse(str, out test) ? test : 0;
        }

        public static bool IsDouble(this string str)
        {
            double test;
            return double.TryParse(str, out test);
        }

        public static bool IsDouble(this string str, out double test)
        {
            return double.TryParse(str, out test);
        }

        /// <summary>
        /// decimal 去末尾的零
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToNumString(this decimal num)
        {
            var splitNum = num.ToString().Split('.');
            if (splitNum.Length > 1)
            {
                if (splitNum[1].ToInt32() == 0)
                    return splitNum[0];
                else
                    return splitNum[0] + "." + splitNum[1];
            }
            else
            {
                return num.ToString("0");
            }
        }

        /// <summary>
        /// decimal 转金额，去末尾零
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToCurrency(this decimal num)
        {
            string currency = num.ToString("c");
            int lastIndex = currency.LastIndexOf(".00", StringComparison.Ordinal);
            if (lastIndex > 0 && lastIndex == currency.Length - 3)
                return currency.Substring(0, lastIndex);
            return currency;
        }

        public static string ToCurrency(this int num)
        {
            string currency = num.ToString("c");
            int lastIndex = currency.LastIndexOf(".00", StringComparison.Ordinal);
            if (lastIndex > 0 && lastIndex == currency.Length - 3)
                return currency.Substring(0, lastIndex);
            return currency;
        }

        /// <summary>
        /// decimal 转为中文金额
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToChnString(this decimal num)
        {
            string s = double.Parse(num.ToString()).ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            string d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            return Regex.Replace(d, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
        }

        /// <summary>
        /// 相加
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static double Add(this double d1, double d2)
        {
            return (double)((decimal)d1 + (decimal)d2);
        }

        /// <summary>
        /// 相减
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static double Sub(this double d1, double d2)
        {
            return (double)((decimal)d1 - (decimal)d2);
        }

        /// <summary>
        /// 相乖
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static double Mul(this double d1, double d2)
        {
            return (double)((decimal)d1 * (decimal)d2);
        }

        /// <summary>
        /// 相除
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static double Div(this double d1, double d2)
        {
            return Math.Abs(d2) < 0.0000000000000000000000000001f ? 0 : (double)((decimal)d1 / (decimal)d2);
        }

        public static bool IsInt(this string str)
        {
            int test;
            return int.TryParse(str, out test);
        }

        public static bool IsInt(this string str, out int test)
        {
            return int.TryParse(str, out test);
        }

        /// <summary>
        /// 将数组转换为符号分隔的字符串
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="split">分隔符</param>
        /// <returns></returns>
        public static string Join1<T>(this T[] arr, string split = ",")
        {
            StringBuilder sb = new StringBuilder(arr.Length * 36);
            for (int i = 0; i < arr.Length; i++)
            {
                sb.Append(arr[i].ToString());
                if (i < arr.Length - 1)
                {
                    sb.Append(split);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 去除所有空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSpace(this string str)
        {
            return str.IsNullOrEmpty() ? "" : str.Replace("", " ").Replace("\r", "").Replace("\n", "");
        }

        public static bool IsEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        public static bool IsEmpty(this Guid? guid)
        {
            return !guid.HasValue || guid.Value.IsEmpty();
        }

        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsLong(this string str)
        {
            long test;
            return long.TryParse(str, out test);
        }

        public static bool IsLong(this string str, out long test)
        {
            return long.TryParse(str, out test);
        }

        public static bool IsDateTime(this string str)
        {
            DateTime test;
            return DateTime.TryParse(str, out test);
        }

        public static bool IsDateTime(this string str, out DateTime test)
        {
            return DateTime.TryParse(str, out test);
        }

        public static bool IsGuid(this string str)
        {
            Guid test;
            return Guid.TryParse(str, out test);
        }

        public static bool IsGuid(this string str, out Guid test)
        {
            return Guid.TryParse(str, out test);
        }

        /// <summary>
        /// 判断是否为Guid.Empty
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool IsEmptyGuid(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        public static bool IsUrl(this string str)
        {
            if (str.IsNullOrEmpty())
                return false;
            string pattern = @"^(http|https|ftp|rtsp|mms):(\/\/|\\\\)[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]+[A-Za-z0-9\.\/=\?%\-&_~`@:\+!;]*$";
            return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsEmail(this string str)
        {
            return Regex.IsMatch(str, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 判断一个整型是否包含在指定的值内
        /// </summary>
        /// <param name="i"></param>
        /// <param name="ints"></param>
        /// <returns></returns>
        public static bool In(this int i, params int[] ints)
        {
            return ints.Any(k => i == k);
        }

        /// <summary>
        /// 返回值或DBNull.Value
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static object DbValueOrNull(this string str)
        {
            if (IsNullOrEmpty(str))
            {
                return null;
            }
            return str;
        }

        public static string ToHtml(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return str.Replace("\r\n", "<br />");
        }

        public static decimal Round(this decimal dec, int decimals = 2)
        {
            return Math.Round(dec, decimals, MidpointRounding.AwayFromZero);
        }

        public static double ToDouble(this string str, int digits)
        {
            double test;
            return double.TryParse(str, out test) ? test.Round(digits) : 0;
        }

        public static double ToDouble(this string str)
        {
            double test;
            return double.TryParse(str, out test) ? test : 0;
        }

        public static double Round(this double value, int decimals)
        {
            if (value < 0)
                return Math.Round(value + 5 / Math.Pow(10, decimals + 1), decimals, MidpointRounding.AwayFromZero);
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }

        public static short ToShort(this string str)
        {
            short test;
            short.TryParse(str, out test);
            return test;
        }

        public static int? ToIntOrNull(this string str)
        {
            int test;
            if (int.TryParse(str, out test))
            {
                return test;
            }
            return null;
        }

        public static int ToInt(this string str)
        {
            int test;
            int.TryParse(str, out test);
            return test;
        }

        public static int ToInt(this string str, int defaultValue)
        {
            int test;
            return int.TryParse(str, out test) ? test : defaultValue;
        }

        public static long ToLong(this string str)
        {
            long test;
            long.TryParse(str, out test);
            return test;
        }

        public static Int16 ToInt16(this string str)
        {
            Int16 test;
            Int16.TryParse(str, out test);
            return test;
        }

        public static Int32 ToInt32(this string str)
        {
            Int32 test;
            Int32.TryParse(str, out test);
            return test;
        }

        public static Int64 ToInt64(this string str)
        {
            Int64 test;
            Int64.TryParse(str, out test);
            return test;
        }

        public static DateTime ToDateTime(this string str)
        {
            DateTime test;
            DateTime.TryParse(str, out test);
            return test == DateTime.MinValue ? SqlMinDateTime : test;
        }

        public static DateTime? ToDateTimeOrNull(this string str)
        {
            DateTime test;
            if (DateTime.TryParse(str, out test))
            {
                return test;
            }
            return null;
        }

        public static Guid ToGuid(this string str)
        {
            Guid test;
            return Guid.TryParse(str, out test) ? test : Guid.Empty;
        }

        /// <summary>
        /// 尝试转换为Boolean类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string str)
        {
            bool b;
            return Boolean.TryParse(str, out b) && b;
        }

        /// <summary>
        /// 尝试格式化日期字符串
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string DateFormat(this object date, string format = "yyyy/MM/dd")
        {
            if (date == null) { return string.Empty; }
            DateTime d;
            string strDate = !date.ToString().IsDateTime(out d) ? date.ToString() : d.ToString(format);
            return d.IsMinDateTime() || strDate == "0001/01/01" || strDate == "0001-01-01" || strDate == "1753/01/01" ||
                strDate == "1753-01-01" ? string.Empty : strDate;
        }

        /// <summary>
        /// 数据库最小日期
        /// </summary>
        public static DateTime SqlMinDateTime = new DateTime(1753, 1, 1);

        /// <summary>
        /// 判断是否为数据库最小日期
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static bool IsMinDateTime(this DateTime datetime)
        {
            return datetime == DateTime.MinValue || datetime == SqlMinDateTime || datetime.Year < 1753;
        }

        public static string ToMyShortDateString(this DateTime datetime)
        {
            return datetime.ToString("yyyy/MM/dd");
        }

        public static string ToMyLongDateString(this DateTime datetime)
        {
            return datetime.ToString("yyyy月MM日dd日");
        }

        public static string ToMyShortTimeString(this DateTime datetime)
        {
            return datetime.ToString("yyyy/MM/dd HH:mm");
        }

        public static string ToMyLongTimeString(this DateTime datetime)
        {
            return datetime.ToString("yyyy年MM月dd日 HH时mm分");
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string ToString(this IList<string> strList, char split)
        {
            return strList.ToString(split.ToString());
        }

        public static string ToString(this IList<string> strList, string split)
        {
            StringBuilder sb = new StringBuilder(strList.Count * 10);
            for (int i = 0; i < strList.Count; i++)
            {
                sb.Append(strList[i]);
                if (i < strList.Count - 1)
                {
                    sb.Append(split);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 过滤sql
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceSql(this string str)
        {
            str = str.Replace("'", "").Replace("--", " ").Replace(";", "");
            return str;
        }

        /// <summary>
        /// 过滤查询sql
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceSelectSql(this string str)
        {
            if (str.IsNullOrEmpty()) return "";
            str = str.Replace1("DELETE", "").Replace1("UPDATE", "").Replace1("INSERT", "");
            return str;
        }

        /// <summary>
        /// 过滤字符串(不区分大小写)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="oldString"></param>
        /// <param name="newString"></param>
        /// <returns></returns>
        public static string Replace1(this string str, string oldString, string newString)
        {
            return str.IsNullOrEmpty() ? "" : str.Replace(oldString, newString);
        }

        /// <summary>
        /// 截取字符串，汉字两个字节，字母一个字节
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <param name="show"></param>
        /// <returns></returns>
        public static string Interruption(this string str, int len, string show)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 63)
                { tempLen += 2; }
                else
                { tempLen += 1; }
                try
                { tempString += str.Substring(i, 1); }
                catch
                { break; }
                if (tempLen > len) break;
            }
            //如果截过则加上半个省略号 
            byte[] mybyte = Encoding.Default.GetBytes(str);
            if (mybyte.Length > len)
                tempString += show;
            tempString = tempString.Replace("&nbsp;", " ");
            tempString = tempString.Replace("&lt;", "<");
            tempString = tempString.Replace("&gt;", ">");
            tempString = tempString.Replace('\n'.ToString(), "<br>");
            return tempString;
        }

        /// <summary>
        /// 截取字符串，汉字两个字节，字母一个字节
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <param name="show"></param>
        /// <returns></returns>
        public static string CutString(this string str, int len, string show = "...")
        {
            return Interruption(str, len, show);
        }

        /// <summary>
        /// 获取左边多少个字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Left(this string str, int len)
        {
            if (str == null || len < 1) { return ""; }
            return len < str.Length ? str.Substring(0, len) : str;
        }

        /// <summary>
        /// 获取右边多少个字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Right(this string str, int len)
        {
            if (str == null || len < 1) { return ""; }
            return len < str.Length ? str.Substring(str.Length - len) : str;
        }

        /// <summary>
        /// 得到实符串实际长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Size(this string str)
        {
            byte[] strArray = Encoding.Default.GetBytes(str);
            int res = strArray.Length;
            return res;
        }

        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveHtml(this string html)
        {
            //删除脚本   
            html = Regex.Replace(html, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            html = Regex.Replace(html, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"-->", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<!--.*", "", RegexOptions.IgnoreCase);

            html = Regex.Replace(html, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            html = html.Replace("<", "").Replace(">", "").Replace("\r\n", "");
            html = HttpContext.Current.Server.HtmlEncode(html).Trim();

            return html;
        }

        /// <summary>
        /// 过滤js脚本
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveScript(this string html)
        {
            if (html.IsNullOrEmpty()) return string.Empty;
            Regex regex1 = new Regex(@"<script[\s\S]+</script *>", RegexOptions.IgnoreCase);
            Regex regex2 = new Regex(@" href *= *[\s\S]*script *:", RegexOptions.IgnoreCase);
            Regex regex3 = new Regex(@" on[\s\S]*=", RegexOptions.IgnoreCase);
            Regex regex4 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
            Regex regex5 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            return html;
        }

        /// <summary>
        /// 替换页面标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemovePageTag(this string html)
        {
            if (html.IsNullOrEmpty()) return string.Empty;
            Regex regex0 = new Regex(@"<!DOCTYPE[^>]*>", RegexOptions.IgnoreCase);
            Regex regex1 = new Regex(@"<html\s*", RegexOptions.IgnoreCase);
            Regex regex2 = new Regex(@"<head[\s\S]+</head\s*>", RegexOptions.IgnoreCase);
            Regex regex3 = new Regex(@"<body\s*", RegexOptions.IgnoreCase);
            Regex regex4 = new Regex(@"<form\s*", RegexOptions.IgnoreCase);
            Regex regex5 = new Regex(@"</(form|body|head|html)>", RegexOptions.IgnoreCase);
            html = regex0.Replace(html, ""); //过滤<html>标记
            html = regex1.Replace(html, "<html\u3000 "); //过滤<html>标记
            html = regex2.Replace(html, ""); //过滤<head>属性
            html = regex3.Replace(html, "<body\u3000 "); //过滤<body>属性
            html = regex4.Replace(html, "<form\u3000 "); //过滤<form>属性
            html = regex5.Replace(html, "</$1\u3000>"); //过滤</html></body></head></form>属性
            return html;
        }

        /// <summary>
        /// 取得html中的图片
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetImg(this string text)
        {
            string str = string.Empty;
            //注意这里的(?<url>\S+)是按正则表达式中的组来处理的，下面的代码中用使用到，也可以更改成其它的HTML标签，以同样的方法获得内容！
            Regex r = new Regex(@"<img\s+[^>]*\s*src\s*=\s*([']?)(?<url>\S+)'?[^>]*>", RegexOptions.Compiled);
            Match m = r.Match(text.ToLower());
            if (m.Success)
                str = m.Result("${url}").Replace("\"", "").Replace("'", "");
            return str;
        }

        /// <summary>
        /// 取得html中的所有图片
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string[] GetImgs(this string text)
        {
            List<string> imgs = new List<string>();
            Regex r = new Regex(@"<img\s+[^>]*\s*src\s*=\s*([']?)(?<url>\S+)'?[^>]*>", RegexOptions.Compiled);
            Match m = r.Match(text.ToLower());
            while (m.Success)
            {
                imgs.Add(m.Result("${url}").Replace("\"", "").Replace("'", ""));
                m = m.NextMatch();
            }
            return imgs.ToArray();
        }

        /// <summary>
        /// 产生随机字符串
        /// </summary>
        /// <returns>字符串位数</returns>
        public static string GetRandom(int length)
        {
            StringBuilder sbCheckCode = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length + 1; i++)
            {
                int number = random.Next();
                char code;
                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));
                sbCheckCode.Append(code);
            }

            return sbCheckCode.ToString();
        }

        /// <summary>
        /// 字符串是否包含标点符号(不包括_下画线)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool InPunctuation(this string str)
        {
            return str.ToCharArray().Any(c => char.IsPunctuation(c) && c != '_');
        }

        /// <summary>
        /// 去除字符串标点符号和空字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemovePunctuationOrEmpty(this string str)
        {
            StringBuilder newString = new StringBuilder(str.Length);
            char[] charArr = str.ToCharArray();
            foreach (char symbols in charArr)
            {
                if (!char.IsPunctuation(symbols) && !char.IsWhiteSpace(symbols))
                {
                    newString.Append(symbols);
                }
            }
            return newString.ToString();
        }

        /// <summary>
        /// 返回带星期的日期格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateWeekString(this DateTime date)
        {
            string week = string.Empty;
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Friday: week = "五"; break;
                case DayOfWeek.Monday: week = "一"; break;
                case DayOfWeek.Saturday: week = "六"; break;
                case DayOfWeek.Sunday: week = "日"; break;
                case DayOfWeek.Thursday: week = "四"; break;
                case DayOfWeek.Tuesday: week = "二"; break;
                case DayOfWeek.Wednesday: week = "三"; break;
            }
            return date.ToString("yyyy年M月d日 ") + "星期" + week;
        }

        /// <summary>
        /// 返回带星期的日期时间格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateTimeWeekString(this DateTime date)
        {
            string week = string.Empty;
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Friday: week = "五"; break;
                case DayOfWeek.Monday: week = "一"; break;
                case DayOfWeek.Saturday: week = "六"; break;
                case DayOfWeek.Sunday: week = "日"; break;
                case DayOfWeek.Thursday: week = "四"; break;
                case DayOfWeek.Tuesday: week = "二"; break;
                case DayOfWeek.Wednesday: week = "三"; break;
            }
            return date.ToString("yyyy年M月d日H时m分") + " 星期" + week;
        }

        /// <summary>
        /// HTML编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string str)
        {
            return HttpContext.Current.Server.HtmlEncode(str);
        }

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(this string str)
        {
            return str.IsNullOrEmpty() ? string.Empty : HttpContext.Current.Server.UrlEncode(str);
        }

        /// <summary>
        /// 获取与 Web 服务器上的指定虚拟路径相对应的物理文件路径。
        /// </summary>
        /// <param name="server"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPathExt(this HttpServerUtility server, string path)
        {
            if (path.StartsWith(@"\\") || path.Contains(":"))
            {
                return path;
            }

            return server.MapPath(path);
        }

        /// <summary>
        /// 使用CDATA的方式将value保存在指定的element中
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetCDataValue(this System.Xml.Linq.XElement element, string value)
        {
            element.RemoveNodes();
            element.Add(new System.Xml.Linq.XCData(value));
        }

        /// <summary>
        /// 返回一个值，该值指示指定的 System.String 对象是否出现在此字符串中。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value">要搜寻的字符串。</param>
        /// <param name="comparisonType">指定搜索规则的枚举值之一</param>
        /// <returns>如果 value 参数出现在此字符串中，或者 value 为空字符串 ("")，则为 true；否则为 false</returns>
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            if (source == null || value == null) { return false; }
            if (value == "") { return true; }
            return (source.IndexOf(value, comparisonType) >= 0);
        }

        /// <summary>
        /// 通过使用默认的相等或字符比较器确定序列是否包含指定的元素。
        /// </summary>
        /// <param name="source">要在其中定位某个值的序列。</param>
        /// <param name="value">要在序列中定位的值。</param>
        /// <param name="comparisonType">指定搜索规则的枚举值之一</param>
        /// <exception cref="System.ArgumentNullException">source 为 null</exception>
        /// <returns>如果源序列包含具有指定值的元素，则为 true；否则为 false。</returns>
        public static bool Contains(this string[] source, string value, StringComparison comparisonType)
        {
            return source.Contains(value, new CompareText(comparisonType));
        }

        /// <summary>
        /// 比较字符串
        /// </summary>
        private class CompareText : IEqualityComparer<string>
        {
            private StringComparison ComparisonType { get; set; }
            public int GetHashCode(string t) { return t.GetHashCode(); }
            public CompareText(StringComparison comparisonType) { ComparisonType = comparisonType; }
            public bool Equals(string x, string y)
            {
                if (x == y) { return true; }
                if (x == null || y == null) { return false; }
                return x.Equals(y, ComparisonType);
            }
        }

        /// <summary>
        /// 序列化对象为xml字符串
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>xml格式字符串</returns>
        public static string Serialize(this object obj)
        {
            if (obj == null) { return string.Empty; }
            Type type = obj.GetType();
            if (!type.IsSerializable) return string.Empty;
            try
            {
                StringBuilder sb = new StringBuilder();
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type);
                XmlWriterSettings xset = new XmlWriterSettings
                {
                    CloseOutput = true,
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    CheckCharacters = false
                };
                XmlWriter xw = XmlWriter.Create(sb, xset);
                xs.Serialize(xw, obj);
                xw.Flush();
                xw.Close();
                return sb.ToString();
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// AES加密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AesEncrypt(this string str)
        {
            return Encryption.Encrypt(str);
        }
        /// <summary>
        /// AES解密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AesDecrypt(this string str)
        {
            return Encryption.Decrypt(str);
        }

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DesEncrypt(this string str)
        {
            return EncryptionDes.Encrypt(str);
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DesDecrypt(this string str)
        {
            return EncryptionDes.Decrypt(str);
        }

        /// <summary>
        /// 让服务器按钮点击后变灰
        /// </summary>
        /// <param name="button"></param>
        /// <param name="newText">点后的显示文本</param>
        public static void ClickDisabled(this System.Web.UI.WebControls.Button button, string newText = "")
        {
            if (button == null) return;
            StringBuilder js = new StringBuilder();
            System.Web.UI.Page page = (System.Web.UI.Page)HttpContext.Current.Handler;
            page.ClientScript.GetPostBackEventReference(button, string.Empty);

            if (!button.ValidationGroup.IsNullOrEmpty())
            {
                js.AppendFormat("if({0})", button.ValidationGroup);
                js.Append("{");
                if (!button.OnClientClick.IsNullOrEmpty())
                {
                    js.Append(button.OnClientClick);
                }
                js.Append("this.disabled=true;");
                if (!newText.IsNullOrEmpty())
                {
                    js.AppendFormat("this.value=\"{0}\";", newText);
                }
                js.AppendFormat("__doPostBack(\"{0}\",\"\");", button.ID);
                js.Append("}else{return false;}");
            }
            else
            {
                if (!button.OnClientClick.IsNullOrEmpty())
                {
                    js.Append(button.OnClientClick);
                }
                js.Append("this.disabled=true;");
                if (!newText.IsNullOrEmpty())
                {
                    js.AppendFormat("this.value=\"{0}\";", newText);
                }
                js.AppendFormat("__doPostBack(\"{0}\",\"\");", button.ID);
            }
            button.OnClientClick = js.ToString();
        }

        /// <summary>
        /// 获得字符串的拼音全拼
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string GetFullSpell(this string strText)
        {
            if (string.IsNullOrEmpty(strText))
                return string.Empty;

            StringBuilder sbOut = new StringBuilder();

            foreach (char ch in strText)
            {
                if (!ChineseChar.IsValidChar(ch))
                    continue;
                var chnChar = new ChineseChar(ch);
                string pinyin = chnChar.Pinyins[0];
                pinyin = Regex.Replace(pinyin, "[0-9]", "");
                sbOut.Append(pinyin.ToLower());
            }

            return sbOut.ToString();
        }
    }
}
