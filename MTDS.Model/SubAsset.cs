using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDS.Model
{
  public  class SubAsset
    {
        /// <summary>
        /// 子分类编号
        /// </summary>
        public Guid SubAssetID { get; set; }
        /// <summary>
        /// 系统分类编号
        /// </summary>
        public Guid SystemCategoryID { get; set; }
        /// <summary>
        /// 分类编号
        /// </summary>
        public Guid CategroyID { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public Guid AssetID { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string SubAssetName { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifyTime { get; set; }
        public string ModifyBy { get; set; }
    }
}
