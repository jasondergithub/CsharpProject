using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.ViewModels;
using WebApplication.Services;


namespace WebApplication.Controllers
{
    public class CompanyStaffController : Controller
    {
        private readonly CheckDBList checkDBList = new CheckDBList();
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
        public ActionResult CheckList()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckList(CheckListViewModel checklist)
        {
            CheckList checkList = new CheckList();
            checkList.three_1 = checklist.checklisttest.three_1;
            checkList.three_2 = checklist.checklisttest.three_2;
            checkList.four_1 = checklist.checklisttest.four_1;
            checkDBList.CreateList(checkList);

            return View();
        }
    }
}