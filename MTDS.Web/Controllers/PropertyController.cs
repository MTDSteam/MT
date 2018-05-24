using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MTDS.Web.Controllers
{
    public class PropertyController : Controller
    {
        //
        // GET: /Property/

        public ActionResult Index()
        {
            return View("Property");
        }


        public string PP()
        {
            //通过Dictionary在AssetType表中知道属于这个大分类下的小分类，然后后侧进行属性维护



            return null;
        }


    }
}
