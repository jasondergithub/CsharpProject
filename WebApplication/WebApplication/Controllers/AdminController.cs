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
    public class AdminController : Controller
    {
        private readonly ProjectService dbmanger = new ProjectService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchProject()
        {

            //string account = Session["account"].ToString();
            List<Project> projects = dbmanger.getProjetctJudgeState();
            ViewBag.projects = projects;
            return View();
        }
        public ActionResult checkProject(string projectId)
        {
            List<string> reasons = dbmanger.getReasonByporjectId(projectId);
            ViewBag.reasons = reasons;

            List<bool> essentialValues = dbmanger.getEssentialValue(projectId);
            //List <string > essentiallabels = new List<string>(){ "試用報告", "給付限制文件","產品圖檔","衛福部許可證"
            //    ,"自費特材療效比較表","健保/自費收載","新進自費衛材售價申請書","自費特材保證切結書","自費收載價格參考"
            //    ,"衛材滅菌檢驗報告單","產品品質保證書","報價單","產品授權代理書","產品目錄","其他醫院使用證明"
            //    ,"內含(處置治療表)","公會會員證書號","工廠登記證","審議資格簽核單","醫療法21條","User理由"};
            List<string> essentiallabels = new List<string>() {"reasonA", "reasonB", "reasonC", "reasonD", "reasonE",
                              "reasonF", "reasonG", "reasonH", "reasonI", "reasonJ", "reasonK", "reasonL", "reasonM",
                              "reasonN", "reasonO", "reasonP", "reasonQ", "reasonR", "reasonS", "reasonT", "reasonU"};
            List<string> notShowFolder = new List<string>();
            for (int i = 0; i < 21; i++)
            {
                if (!essentialValues[i])
                {
                    // && i != 0 && i != 18 && i != 20
                    notShowFolder.Add(essentiallabels[i]);
                    // System.Diagnostics.Debug.WriteLine(essentiallabels[i]);
                }
            }
            ViewBag.notShowFolder = notShowFolder;
            //ViewBag.boolValues = essentialValues;
            /* 將理由寫入資料庫 */
            
            return View();
        }
        [HttpPost]
        public ActionResult checkProject(Reason reason, string projectId) //
        {
            System.Diagnostics.Debug.WriteLine(reason.judgeA);
            System.Diagnostics.Debug.WriteLine(reason.judgeB);
            System.Diagnostics.Debug.WriteLine(reason.judgeC);
            //    List<bool> essentialValues = dbmanger.getEssentialValue(projectId);
            //    List<string> essentiallabels = new List<string>() {"reasonA", "reasonB", "reasonC", "reasonD", "reasonE",
            //                      "reasonF", "reasonG", "reasonH", "reasonI", "reasonJ", "reasonK", "reasonL", "reasonM",
            //                      "reasonN", "reasonO", "reasonP", "reasonQ", "reasonR", "reasonS", "reasonT", "reasonU"};
            //    List<string> folderName = new List<string>();
            //    for (int i = 0; i < 21; i++)
            //    {
            //        if (essentialValues[i] && i != 0 && i != 18 && i != 20)
            //        {
            //            folderName.Add(essentiallabels[i]);
            //            //System.Diagnostics.Debug.WriteLine(essentiallabels[i]);
            //        }
            //    }

            return RedirectToAction("SearchProject", "Admin");
        }
    }
}