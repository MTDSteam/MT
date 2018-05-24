using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDS.Model
{
    /// <summary>
    /// 属性表
    /// </summary>
   public class Asset
    {
       public Guid Id { get; set; }
       public Guid AssetTypeId { get; set; }
       public Guid PropertyID { get; set; }
       public string PropertyValue { get; set; }
       public DateTime CreateTime { get; set; }
       public string CreateBy { get; set; }
       public DateTime ModifyTime { get; set; }
       public string ModifyBy { get; set; }
    }
}
