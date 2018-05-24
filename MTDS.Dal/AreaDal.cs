using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MTDS.Model;

namespace MTDS.Dal
{
   public class AreaDal
    {
        /// <summary>
        /// 获得所有地区列表
        /// </summary>
        /// <returns></returns>
        public List<Area> GetAll()
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                string sql = "SELECT * FROM Area";
                return conn.Query<Area>(sql).ToList();
            }
        }

        /// <summary>
        /// 根据名称模糊匹配
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Area GetByName(string name)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                string sql = "SELECT * FROM Area WHERE CHARINDEX(@Name, Name) > 0";
                return conn.Query<Area>(sql, new { Name = name }).FirstOrDefault();
            }
        }
    }
}
