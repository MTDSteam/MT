using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using MTDS.BLL;
using MTDS.Model;
using System.Web.Script.Serialization;
namespace MTDS.Web.Controllers
{
    public class PropertyController : BaseController
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
        public string getAssetTree()
        {
            DataTable dt = bll.GetAssetTree();
            return JsonConvert.SerializeObject(dt);
        }
        public string getAssetProperty()
        {
            var assetTypeID= Request["assetTypeID"];
            var iList = bll.GetByAssetType(new Guid(assetTypeID));
            return JsonConvert.SerializeObject(iList);
        }
        [HttpPost]
        public string saveAsset()
        {
            
            string json = Request["json"];
            string assetTypeID= Request["assetTypeID"];
            AssetPropertyBll bll = new AssetPropertyBll();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> data = (Dictionary<string, object>)serializer.DeserializeObject(json);

            var guid = Guid.NewGuid();
            var  assetList = new List<Asset>();
            foreach (var item in data)
            {
                Asset ass = new Asset()
                {
                    Id = guid,
                    AssetTypeId= new Guid(assetTypeID),
                    PropertyID = new Guid(item.Key),
                    PropertyValue = item.Value.ToString(),
                    CreateTime = DateTime.Now,
                    CreateBy = "9470C4AF-C9E3-40E2-9512-3B5798888AFB",
                    ModifyTime = DateTime.Now,
                    ModifyBy = "9470C4AF-C9E3-40E2-9512-3B5798888AFB"


                };
                assetList.Add(ass);

            }
            var returni= bll.InsertAsset(assetList);
            if(returni>0)
            {
                return "1";
            }
            else
            {
                return "0";
            }

        }

    }
}
