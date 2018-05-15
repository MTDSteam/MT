using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDS.Model
{
   public class Asset
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid AssetID { get; set; }
        /// <summary>
        /// 系统分类编号
        /// </summary>
        public Guid SystemCategoryID { get; set; }
        /// <summary>
        /// 分类编号
        /// </summary>
        public Guid CartegoryId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string AssetName { get; set; }
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
