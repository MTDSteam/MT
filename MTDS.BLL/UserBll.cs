using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTDS.Dal;
using MTDS.Model;

namespace MTDS.BLL
{
    public class UserBll
    {
        private readonly UserDal _dal = new UserDal();

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public List<Users> GetList()
        {
            return _dal.GetList();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Users model)
        {
            return _dal.Insert(model);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Users model)
        {
            return _dal.Update(model);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int Delete(Guid userId)
        {
            return _dal.Delete(userId);
        }

        /// <summary>
        /// 根据编号获取数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Users GetById(Guid userId)
        {
            return _dal.GetById(userId);
        }

        /// <summary>
        /// 根据账号获取数据
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Users GetByAccount(string account)
        {
            return _dal.GetByAccount(account);
        }
    }
}
