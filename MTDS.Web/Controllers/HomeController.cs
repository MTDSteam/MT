﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTDS.Model;
using System.Text;

namespace MTDS.Web.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Home()
        {
            
            return View("Home");
        }
        public ActionResult LoadAccordionMenu()
        {
            List<Modules> list = new List<Modules>();
            var mod1 = new Modules();
            mod1.ModuleId = "9f8ce93a-fc2d-4914-a59c-a6b49494108f";
            mod1.ParentId = "0";
            mod1.Category = "目录";
            mod1.Code = "10";
            mod1.FullName = "平台管理";
            mod1.Icon = "application_view_tile.png";
            mod1.Location = "&nbsp;";
            mod1.Target = "click";
            mod1.Level = 1;

            mod1.SortCode = 10001;
            list.Add(mod1);
            var mod2 = new Modules();
            mod2.ModuleId = "6f757aee-0bcc-406d-938e-fa2a3043dc68";
            mod2.ParentId = "9f8ce93a-fc2d-4914-a59c-a6b49494108f";
            mod2.Category = "目录";
            mod2.Code = "10.02";
            mod2.FullName = "设备维护";
            mod2.Icon = "setting_tools.png";
            mod2.Location = "&nbsp;";
            mod2.Target = "click";
            mod2.Level = 1;

            mod2.SortCode = 10002;
            list.Add(mod2);

            var mod3 = new Modules();
            mod3.ModuleId = "b29cabd8-ffb6-4d34-9d08-ee1dba2b5b6b";
            mod3.ParentId = "6f757aee-0bcc-406d-938e-fa2a3043dc68";
            mod3.Category = "页面";
            mod3.Code = "10.02.01";
            mod3.FullName = "类别维护";
            mod3.Icon = "house.png";
            mod3.Location = "/User/Index";//需修改
            mod3.Target = "iframe";
            mod3.Level = 2;

            mod3.SortCode = 10003;
            list.Add(mod3);
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            StringBuilder sb = new StringBuilder();
            jss.Serialize(list, sb);
            
            return Content(sb.ToString().Replace("&nbsp;", ""));
        }
       
    }
    
}
