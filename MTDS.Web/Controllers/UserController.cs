using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTDS.BLL;
using MTDS.Common;
using MTDS.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MTDS.Web.Controllers
{
    public class UserController : Controller
    {
        private UserBll uBll=new UserBll();
        private const int pageSize = 10;
        public ActionResult Index()
        {
            return View();
        }

        public string GetList()
        {
            var pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            var list = uBll.GetList();

            if (list.Count() >= 0)
            {
                int pageCount = (list.Count() - 1) / pageSize + 1; //总页数
                int totalCount = list.Count; //总记录数

                list = list.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                string navStr = Tools.ShowPageNavigate(pageSize, pageIndex, totalCount);

                var dataT = new { T = list, NavPageStr = navStr, Total = totalCount };
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd";

               return JsonConvert.SerializeObject(dataT, Formatting.Indented, timeFormat);
            }
            else
            {
              return  JsonConvert.SerializeObject("error");
            }

        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Register()
        {
            var resultMsg = "";
            var userName = HttpContext.Request.Form["txtRealName"];
            var loginName = Request.Form["txtloginName"];
            var password = Request.Form["txtPassword"];
            var mobile = Request.Form["txtTel"];
            var email = Request.Form["txtEmail"];
            var address = Request.Form["txtAddress"];
            var provinceId = Request.Form["area1"];
            var cityId = Request.Form["area2"];
            var countryId = Request.Form["area3"];

            var model = uBll.GetByAccount(userName);
            if (model != null)
            {
                resultMsg = "当前用户已存在!";
            }
            else
            {
                var pp = new Users()
                {
                    UserID = Guid.NewGuid(),
                    Username = "系统管理员",
                    LoginName = "Admin",
                    Password = Md5Encoding.Encoding("123456"),
                    Address = "山西省太原市小店区",
                    Lastlogintime = DateTime.Now,
                    CreateTime = DateTime.Now,
                    ModifyTime = DateTime.Now
                };


                var rst = uBll.Insert(pp);
                if (rst > 0)
                {
                    resultMsg = "success";
                }
                else
                {
                    resultMsg = "error";
                }
            }

            return Json(resultMsg, JsonRequestBehavior.AllowGet);
        }
    }
}
