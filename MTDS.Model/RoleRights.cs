using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDS.Model
{
   public class RoleRights
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public System.Guid RoleId { get; set; }
        /// <summary>
        /// 模块编号
        /// </summary>
        public Guid ModuleID { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifyTime { get; set; }
        public string ModifyBy { get; set; }
    }
}
