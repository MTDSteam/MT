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
        // public Guid ModuleID { get; set; }
        ///// <summary>
        ///// 模块名称
        ///// </summary>
        // public string ModuleName { get; set; }
        // public DateTime CreateTime { get; set; }
        // public string CreateBy { get; set; }
        // public DateTime ModifyTime { get; set; }
        // public string ModifyBy { get; set; }
      

        public string ModuleId { get; set; }
    
        public string ParentId { get; set; }
    
        public string Category { get; set; }
    
        public string Code { get; set; }
 
        public string FullName { get; set; }
  
        public string Icon { get; set; }
  
        public string Location { get; set; }
 
        public string Target { get; set; }
    
        public int? Level { get; set; }
 
        public int? Isexpand { get; set; }

        public int? AllowButton { get; set; }
    
        public int? AllowView { get; set; }

        public int? AllowForm { get; set; }
       
        public int? Authority { get; set; }
 
        public int? DataScope { get; set; }
   
        public string Remark { get; set; }
  
        public int? Enabled { get; set; }

        public int? SortCode { get; set; }

        public int? DeleteMark { get; set; }
  
        public DateTime? CreateDate { get; set; }

        public string CreateUserId { get; set; }
 
        public string CreateUserName { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string ModifyUserId { get; set; }

        public string ModifyUserName { get; set; }

    }
}
