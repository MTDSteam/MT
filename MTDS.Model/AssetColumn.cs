using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDS.Model
{
   public  class AssetColumn
    {
        public Guid ColumnID { get; set; }
        public Guid SystemCategoryID { get; set; }
        public Guid CategoryID { get; set; }
        public Guid AssetID { get; set; }
        public Guid SubAssetID { get; set; }
        public string ColumnName { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifyTime { get; set; }
        public string ModifyBy { get; set; }
    }
}
