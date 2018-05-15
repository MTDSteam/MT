using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTDS.Model
{
   public  class BillColumns
    {
        public int BillType { get; set; }
        public string BillColumnName { get; set; }
        public string BillColumnID { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime  ModifyTime { get; set; }
        public string ModifyBy { get; set; }
    }
}
