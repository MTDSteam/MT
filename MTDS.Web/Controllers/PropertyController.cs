using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using MTDS.BLL;
namespace MTDS.Web.Controllers
{
    public class PropertyController : Controller
    {
        //
        // GET: /Property/

        public ActionResult Property()
        {
            return View();
        }

        AssetPropertyBll bll = new AssetPropertyBll();
  
        public string getPropertyList()
        {
          

            var pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            var pageSize= Request["rows"] != null ? int.Parse(Request["rows"]) : 10;
            int totalCount=1;

            var assetTypeID = Request["assetTypeID"];
            if (!string.IsNullOrEmpty(assetTypeID))
            {
                DataTable dt = bll.GetAssetbyType(new Guid(assetTypeID));
                //return JsonConvert.SerializeObject(dt);
                var totalPages = 1;
                var jsonData = new
                {
                    recordsTotal = totalCount,
                    draw = pageIndex,
                    recordsFiltered = totalPages,
                    aaData = dt
                };
                return JsonConvert.SerializeObject(jsonData);
            }
            else
                return null;
        }


    }
}
