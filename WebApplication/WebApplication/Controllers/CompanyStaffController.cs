using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class CompanyStaffController : Controller
    {
        //登出
        [Authorize(Roles = "CompanyStaff")]
        public ActionResult Logout()
        {
            //使用者登出
            if (Request.Cookies["authCookie"] != null)
            {
                var cookie = new HttpCookie("authCookie");
                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie.Values.Clear();
                Response.Cookies.Set(cookie);
                Session.Abandon();
            }
            // 重新導向至首頁
            return RedirectToAction("Index", "Home");
        }
        
        // GET: CompanyStaff
        [Authorize(Roles = "CompanyStaff")]
        public ActionResult Index()
        {
            return View();
        }
    }
}