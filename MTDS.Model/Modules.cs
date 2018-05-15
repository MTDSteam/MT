using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDS.Model
{
   public class Modules
   {
       /// <summary>
       /// 模块编号
       /// </summary>
        public Guid ModuleID { get; set; }
       /// <summary>
       /// 模块名称
       /// </summary>
        public string ModuleName { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifyTime { get; set; }
        public string ModifyBy { get; set; }
    }
}
