using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDS.Model
{
   public class AssetProperty
    {
       public Guid Id { get; set; }
       public string Name { get; set; } 
       public Guid DictionaryId { get; set; }
       public Guid AssetTypeId { get; set; }
       public DateTime CreateTime { get; set; }
       public string CreateBy { get; set; }
       public DateTime ModifyTime { get; set; }
       public string ModifyBy { get; set; }
    }
}
