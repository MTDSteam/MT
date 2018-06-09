using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDS.Model
{
   public class Users
    {
        public Users()
        {
            Lastlogintime=DateTime.Now;
            ModifyTime=DateTime.Now;
        }
       //序号  非数据库字段
        public int RowNum { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserID { get; set; }
       /// <summary>
       /// 登录名
       /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }
        /// <summary>
        /// 省编号
        /// </summary>
        public int ProvinceID { get; set; }
        /// <summary>
        /// 市编号
        /// </summary>
        public int AreaID { get; set; }
        /// <summary>
        /// 县编号
        /// </summary>
        public int CountyID { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime Lastlogintime { get; set; }
        /// <summary>
        /// 父级编号
        /// </summary>
        public Guid ParentID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyBy { get; set; }
    }
}
