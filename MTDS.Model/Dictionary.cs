using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDS.Model
{
   public class Dictionary
    {
        /// <summary>
        /// 编号
        /// </summary>
       public Guid Id { get; set; }
        /// <summary>
        /// 父级编号
        /// </summary>
       public Guid ParentId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
       public string Title { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
       public string Code { get; set; }
        /// <summary>
        /// 值
        /// </summary>
       public string Value { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
       public string Note { get; set; }
        /// <summary>
        /// 其他
        /// </summary>
       public string Other { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
       public string Sort { get; set; }
    }
}
