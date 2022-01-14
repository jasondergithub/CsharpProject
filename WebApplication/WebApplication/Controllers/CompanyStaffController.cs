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
        private readonly ProjectService dbmanger = new ProjectService();
        private readonly CheckDBList checkDBList = new CheckDBList();
        private readonly uploadFileService uploadManager = new uploadFileService();
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
        public ActionResult CheckList() //string projectId
        {
            //System.Diagnostics.Debug.WriteLine(projectId);
            return View();
        }
        [HttpPost]
        public ActionResult CheckList(CheckListViewModel checklist, string projectId)
        {
            // System.Diagnostics.Debug.WriteLine(projectId);
            
            CheckList checkList = new CheckList();
            checkList.projectId = projectId;
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
            checkList.fifteen = "User理由";


            checkDBList.CreateList(checkList);

            return View();
        }
        public ActionResult SearchProject()
        {

            string account = Session["account"].ToString();
            List<Project> projects = dbmanger.getProjetctByCompany(account);
            ViewBag.projects = projects;
            return View();
        }
        public ActionResult UploadFile(string projectId)
        {
            List<bool> essentialValues = dbmanger.getEssentialValue(projectId);
            //List <string > essentiallabels = new List<string>(){ "試用報告", "給付限制文件","產品圖檔","衛福部許可證"
            //    ,"自費特材療效比較表","健保/自費收載","新進自費衛材售價申請書","自費特材保證切結書","自費收載價格參考"
            //    ,"衛材滅菌檢驗報告單","產品品質保證書","報價單","產品授權代理書","產品目錄","其他醫院使用證明"
            //    ,"內含(處置治療表)","公會會員證書號","工廠登記證","審議資格簽核單","醫療法21條","User理由"};
            List<string> essentiallabels = new List<string>() {"reasonA", "reasonB", "reasonC", "reasonD", "reasonE",
                              "reasonF", "reasonG", "reasonH", "reasonI", "reasonJ", "reasonK", "reasonL", "reasonM",
                              "reasonN", "reasonO", "reasonP", "reasonQ", "reasonR", "reasonS", "reasonT", "reasonU"};
            List<string> folderName = new List<string>();
            for (int i = 0; i < 21; i++)
            {
                if (essentialValues[i]&&i!=0&&i!=18&&i!=20)
                {
                    folderName.Add(essentiallabels[i]);
                    // System.Diagnostics.Debug.WriteLine(essentiallabels[i]);
                }
            }
            ViewBag.folderName = folderName;
            //ViewBag.boolValues = essentialValues;
            /* 將理由寫入資料庫 */

            Session["projectId"] = projectId;
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile( Reason reason,  HttpPostedFileBase[] photos) //
        {
            string projectId =Session["projectId"].ToString();
            System.Diagnostics.Debug.WriteLine(projectId);
            List<bool> essentialValues = dbmanger.getEssentialValue(projectId);

            List<string> essentiallabels = new List<string>() {"reasonA", "reasonB", "reasonC", "reasonD", "reasonE",
                              "reasonF", "reasonG", "reasonH", "reasonI", "reasonJ", "reasonK", "reasonL", "reasonM",
                              "reasonN", "reasonO", "reasonP", "reasonQ", "reasonR", "reasonS", "reasonT", "reasonU"};
            string SubpathToProject = dbmanger.getCompanyIdbyProjectId(projectId) + "/" + projectId;

            List<string> folderName = new List<string>();
            for (int i = 0; i < 21; i++)
            {
                if (essentialValues[i] && i != 0 && i != 18 && i != 20)
                {
                    if ((HttpPostedFileBase)photos[i] != null)
                    {
                        // System.Diagnostics.Debug.WriteLine(SubpathToProject + '/'+ essentiallabels[i][essentiallabels[i].Length - 1]);
                        uploadManager.UploadToFtp(photos[i], SubpathToProject + '/' + essentiallabels[i][essentiallabels[i].Length - 1]);
                    }
                        
                    folderName.Add(essentiallabels[i]);
                    //System.Diagnostics.Debug.WriteLine(essentiallabels[i]);
                }
            }
            dbmanger.writeReason2DB(folderName, reason, projectId);
            return View();
        }

    }
}