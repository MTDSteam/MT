﻿using System;
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
            var assetTypeID = Request["assetTypeID"];
            if (!string.IsNullOrEmpty(assetTypeID))
            {
                DataTable dt = bll.GetAssetbyType(new Guid(assetTypeID));
                return JsonConvert.SerializeObject(dt);
            }
            else
                return null;
      
        }


    }
}
