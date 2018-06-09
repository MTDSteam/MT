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
        public List<Users> GetList(string loginName="")
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                string sql = "select ROW_NUMBER() over(order by UserType) as RowNum,u.* from Users as u where 1=1";

                if (!string.IsNullOrEmpty(loginName))
                {
                    sql += " and u.loginName like '%" + loginName + "%' ";
                }

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
            using (var conn = DapperHelper.CreateConnection())
            {
                string sql =
                    "Insert into Users(UserID, loginName, username, password, email, mobile, telephone, provinceID, AreaID, CountyID, address, remark, lastlogintime, ParentID, CreateTime, CreateBy, ModifyTime, ModifyBy,UserType) values(@UserID, @loginName, @username, @password, @email, @mobile, @telephone, @provinceID, @AreaID, @CountyID, @address, @remark, @lastlogintime, @ParentID, @CreateTime, @CreateBy, @ModifyTime, @ModifyBy,@UserType)";
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
            using (var conn = DapperHelper.CreateConnection())
            {
                string sql =
                    "Update users set loginName=@loginName,username=@username,email=@email,mobile=@mobile," +
                    "Address=@Address,ModifyTime=@ModifyTime," +
                    "ModifyBy=@ModifyBy,UserType=@UserType where UserID=@UserID";
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
            using (var conn = DapperHelper.CreateConnection())
            {
                string sql = "Delete from Users where UserId=@UserId";
                return conn.Execute(sql, new { UserId = userId });
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
                string sql = "Select * from Users where UserID='" + userId + "'";
                return conn.Query<Users>(sql).SingleOrDefault();
            }
        }

        /// <summary>
        /// 根据账号获取数据
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Users GetByAccount(string account)
        {
            using (var conn = DapperHelper.CreateConnection())
            {
                string sql = "Select * from Users where loginName='"+account+"'";
                return conn.Query<Users>(sql).FirstOrDefault();
            }
        }
    }
}
