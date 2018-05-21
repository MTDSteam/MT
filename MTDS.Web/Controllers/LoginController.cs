using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTDS.BLL;
using MTDS.Common;
using MTDS.Model;

namespace MTDS.Web.Controllers
{
    public class LoginController : Controller
    {
        private UserBll uBll=new UserBll();

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public JsonResult Login()
        {
            var resultMsg = "";
            var userName = HttpContext.Request.Form["userName"];
            var password = HttpContext.Request.Form["password"];

            if (userName != null)
            {
                var model = uBll.GetByAccount(userName);

                if (model == null)
                {
                    resultMsg = "用户不存在";
                }
                else
                {
                    var encryPass = Md5Encoding.Encoding(password);
                    if (!encryPass.Equals(model.Password))
                    {
                        resultMsg = "密码错误";
                    }
                    else
                    {
                        HttpCookie cookie = new HttpCookie("platform");
                        cookie.Values.Add("account", userName.Trim());
                        cookie.Values.Add("password", password.Trim());
                        cookie.Expires = System.DateTime.Now.AddDays(7.0);
                        HttpContext.Response.Cookies.Add(cookie);
                    }

                    Session["userId"] = model.UserID;
                    Session["userName"] = model.LoginName;
                    Session["realName"] = model.Username;
                    resultMsg = "success";
                }
            }
            else
            {
                resultMsg = "请输入用户名";
            }

            return Json(resultMsg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Register()
        {
            var resultMsg = "";
            var userName = HttpContext.Request.Form["txtUserName"];
            var realName = Request.Form["realName"];
            var password = Request.Form["password"];
            var mobile = Request.Form["mobile"];
            var telephone = Request.Form["telephone"];
            var email = Request.Form["email"];
            var address = Request.Form["address"];
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

                };
            }


            return null;
        }
    }
}
