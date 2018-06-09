using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MTDS.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //下面实现用户信息相关验证的代码
            if (requestContext.HttpContext.Session["userId"] == null)
            {
                requestContext.HttpContext.Response.Redirect("/Login/Index");
            }
        }

    }
}
