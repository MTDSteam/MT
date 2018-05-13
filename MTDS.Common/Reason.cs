using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTDS.Common
{
    public class Reason
    {
        private string _index;
        private string _con;

        public string Index
        {
            get { return _index; }
            set { _index = value; }
        }

        public string Con
        {
            get { return _con; }
            set { _con = value; }
        }

    }
}
