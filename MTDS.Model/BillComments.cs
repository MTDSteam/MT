using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDS.Model
{
   public class BillComments
    {
        public int BillType { get; set; }
        public Guid BillID { get; set; }
        public string BillColumnID { get; set; }
        public string BillColumnValue { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifyTime { get; set; }
        public string ModifyBy { get; set; }
    }
}
