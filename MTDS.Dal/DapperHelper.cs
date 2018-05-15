using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTDS.Common;

namespace MTDS.Dal
{
   public class DapperHelper
    {
        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection CreateConnection()
        {
            var strConn = Config.PlatformConnectionString;
            IDbConnection conn = new SqlConnection(strConn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        /// <summary>
        /// 分页方法（SQLServer）
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="fileds">字段</param>
        /// <param name="where">WHERE条件</param>
        /// <param name="order">排序列</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="count">记录条数</param>
        /// <returns>分页SQL语句</returns>
        public static string GetPagerSql(string table, string fileds, string where, string order,
            int pageSize, int pageIndex, long count)
        {
            return string.Format("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {3}) AS rowNum, {1}" +
                                 " FROM {0} WHERE 1 = 1 {2}) AS tmpT WHERE rowNum BETWEEN {4} AND {5}",
                table, fileds, where, order, pageIndex * pageSize - pageSize + 1, pageIndex * pageSize);
        }
    }
}
