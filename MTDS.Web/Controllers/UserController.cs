using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private UserBll uBll = new UserBll();
        private const int pageSize = 10;
        private AreaBll areaBll = new AreaBll();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public string GetList()
        {
            var pageIndex = Convert.ToInt32(Request["id1"]);
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
                return JsonConvert.SerializeObject("error");
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
            var hidId = Request.Form["HidUId"];
            var userName = HttpContext.Request.Form["txtRealName"];
            var loginName = Request.Form["txtloginName"];
            var password = Request.Form["txtPassword"];
            var mobile = Request.Form["txtTel"];
            var email = Request.Form["txtEmail"];
            var address = Request.Form["txtAddress"];

            if (hidId.IsNullOrEmpty())
            {
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
                        Username = userName,
                        LoginName = loginName,
                        Password = Md5Encoding.Encoding(password),
                        Mobile = mobile,
                        Email = email,
                        Address = address,
                        CreateTime = DateTime.Now,
                        ModifyTime = DateTime.Now,
                        CreateBy = Session["userName"] != null ? Session["userName"].ToString() : ""
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
            }
            else
            {
                var uu = uBll.GetById(hidId.ToGuid());
                uu.LoginName = loginName;
                uu.Username = userName;
                uu.Mobile = mobile;
                uu.Email = email;
                uu.Address = address;
                uu.ModifyTime=DateTime.Now;
                uu.ModifyBy = Session["userName"] != null ? Session["userName"].ToString() : "";

                var rst = uBll.Update(uu);
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

        //删除
        public string Delete()
        {
            var id = Request.Form["uid"];
            var rst = uBll.Delete(id.ToGuid());

            if (rst > 0)
                return "success";
            else
                return "error";
        }
        /// <summary>
        /// 获取单个用户信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSingle()
        {
            var id = Request.QueryString["uid"];
            var model = uBll.GetById(id.ToGuid());
            if (model != null)
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }



    }
}
