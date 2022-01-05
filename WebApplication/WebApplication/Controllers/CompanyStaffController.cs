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
            Console.WriteLine("stage 0");
            CheckList checkList = new CheckList();
            checkList.two = "衛福部許可證或特別同意函文";
            checkList.two_1 = checklist.checklisttest.two_1;
            checkList.two_2 = checklist.checklisttest.two_2;
            checkList.two_3 = checklist.checklisttest.two_3;
            checkList.two_4 = checklist.checklisttest.two_4;
            checkList.two_5 = checklist.checklisttest.two_5;
            checkList.three_1 = checklist.checklisttest.three_1;
            checkList.three_2 = checklist.checklisttest.three_2;
            checkList.three_six = checklist.checklisttest.three_six;
            checkList.three = checklist.checklisttest.three;
            checkList.four = checklist.checklisttest.four;
            checkList.four_1 = checklist.checklisttest.four_1;
            checkList.four_2 = checklist.checklisttest.four_2;
            checkList.five_1= checklist.checklisttest.five_1;
            checkList.five_2 = checklist.checklisttest.five_2;
            checkList.five_3 = checklist.checklisttest.five_3;
            checkList.seven = checklist.checklisttest.seven;
            checkList.eight = "品質保證書";
            checkList.nine = "報價單";
            checkList.ten = "其他醫院使用證明";
            checkList.sub_ten = checklist.checklisttest.sub_ten;
            checkList.ten_1 = checklist.checklisttest.ten_1;
            checkList.ten_2 = checklist.checklisttest.ten_2;
            checkList.ten_3 = checklist.checklisttest.ten_3;
            checkList.eleven = "清楚圖檔";
            checkList.thirteen = "試用報告";
            checkList.fourteen = "審議資格簽核單";

            checkDBList.CreateList(checkList);

            return View();
        }
    }
}