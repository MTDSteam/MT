using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDS.Model
{
    public class AssetProperty
    {
        /// <summary>
        /// 属性编号
        /// </summary>
        public Guid PropertyID { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string PropertyValue { get; set; }
        /// <summary>
        /// 列编号
        /// </summary>
        public Guid ColumnID { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifyTime { get; set; }
        public string ModifyBy { get; set; }
    }
}
