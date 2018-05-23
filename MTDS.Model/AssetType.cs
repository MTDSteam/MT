using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDS.Model
{
    /// <summary>
    /// 设备分类
    /// </summary>
   public class AssetType
    {
       public Guid Id { get; set; }
       public Guid DictionaryId { get; set; }
       public string Name { get; set; }
       public Guid ParentID { get; set; }
       public short isAssetName { get; set; }
       public DateTime CreateTime { get; set; }
       public string CreateBy { get; set; }
       public DateTime ModifyTime { get; set; }
       public string ModifyBy { get; set; }
    }
}
