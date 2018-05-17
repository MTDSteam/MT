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
            using (var conn = DapperHelper.CreateConnection())
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
            using (var conn=DapperHelper.CreateConnection())
            {
                string sql =
                    "Insert into User(UserID, loginName, username, password, email, mobile, telephone, provinceID, AreaID, CountyID, address, remark, lastlogintime, ParentID, CreateTime, CreateBy, ModifyTime, ModifyBy) values(@UserID, @loginName, @username, @password, @email, @mobile, @telephone, @provinceID, @AreaID, @CountyID, @address, @remark, @lastlogintime, @ParentID, @CreateTime, @CreateBy, @ModifyTime, @ModifyBy)";
                return conn.Execute(sql, model);
            }
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Users model)
        {
            using (var conn=DapperHelper.CreateConnection())
            {
                string sql =
                    "Update user set loginName=@loginName,username=@username,email=@email,mobile=@mobile,telephone=@telephone," +
                    "CreateTime=@CreateTime,CreateBy=@CreateBy,ModifyTime=@ModifyTime," +
                    "ModifyBy=@ModifyBy where UserID=@UserID";
                return conn.Execute(sql, model);
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int Delete(Guid userId)
        {
            using (var conn=DapperHelper.CreateConnection())
            {
                string sql = "Delete from User where UserId=@UserId";
                return conn.Execute(sql, new {UserId = userId});
            }
        }
        /// <summary>
        /// 根据编号获取数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Users GetById(Guid userId)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                string sql = "Select * from User where Id='" + userId + "'";
                return conn.Query<Users>(sql).SingleOrDefault();
            }
        }

       
    }
}
