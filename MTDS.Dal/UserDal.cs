using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MTDS.Model;
namespace MTDS.Dal
{
    public class UserDal
    {
        
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public List<Users> GetList()
        {
            using (var conn=DapperHelper.CreateConnection())
            {
                string sql = "Select * from User";
                return conn.Query<Users>(sql).ToList();
            }
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Users model)
        {
            return 0;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Users model)
        {
            return 0;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete(Users model)
        {
            return 0;
        }
        /// <summary>
        /// 根据编号获取数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Users GetById(Guid userId)
        {
            using (var conn=DapperHelper.CreateConnection())
            {
                string sql = "Select * from User where Id='" + userId + "'";
                return conn.Query<Users>(sql).SingleOrDefault();
            }
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="password"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int UpdatePassword(string password, Guid userId)
        {
            return 0;
        }

    }
}
