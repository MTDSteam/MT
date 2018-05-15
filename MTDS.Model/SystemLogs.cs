using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDS.Model
{
    public class SystemLogs
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Ip地址
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public int EventType { get; set; }
        /// <summary>
        /// 事件内容
        /// </summary>
        public string EventMessage { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
    }
}
