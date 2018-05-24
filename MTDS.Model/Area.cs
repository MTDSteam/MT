using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDS.Model
{
   public class Area
    {
        /// <summary>
        /// 城市编号
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 长途区号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 地区等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 上级地区
        /// </summary>
        public int ParentNo { get; set; }

        /// <summary>
        /// 地区类型
        /// 省、市、区
        /// </summary>
        public string TypeName { get; set; }
    }
}
