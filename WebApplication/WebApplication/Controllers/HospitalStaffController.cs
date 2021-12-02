using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Services;
using WebApplication.ViewModels;
using WebApplication.Models;
using System.Configuration;

namespace WebApplication.Controllers
{
    public class HospitalStaffController : Controller
    {
        private readonly createDirectory createDirectory = new createDirectory();
        // GET: HospitalStaff
        private readonly ProjectService projectService = new ProjectService();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult CreateProject()
        {
            var drop_down = new List<string>();
            drop_down = projectService.getCompanyName();
            ViewBag.drop_list = drop_down;

            return View();
        }
        [HttpPost]
        public ActionResult CreateProject(CreateProject newProject)
        {
            if (ModelState.IsValid)
            {
                Session["department"] = "200";
                string x = Session["department"].ToString();
                newProject.project.projectNo = projectService.getProjectNo(x);              /* 流水號*/
                newProject.project.time = projectService.getDate();                                    /*時間 (yyyy/MM/dd)*/
                newProject.project.season = projectService.getSeason();                           /*季 (Q1,Q2,Q3,Q4)*/
                newProject.project.department = x;                                                                  /*部門代號*/
                newProject.project.projectId = projectService.getProjectId(newProject.project.projectGenre, newProject.project.time, newProject.project.season, newProject.project.department, newProject.project.projectNo);        /*專案編號*/
                newProject.project.companyId = projectService.getIdByName(newProject.Name);

                // 呼叫createDirectory.cs 中的createDir 創建資料夾
                ViewBag.DirCreateSucc = createDirectory.createDir(newProject.project.companyId + '/'+newProject.project.projectId);
                //建立廠商專案資料夾下的AZ資料夾 
                List<string> folderLists = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                foreach (string i in folderLists)
                {
                    string subDirName = newProject.project.companyId + '/' + newProject.project.projectId + "/" + i;
                    ViewBag.DirCreateSucc = createDirectory.createDir(subDirName);
                }
                projectService.Createproject(newProject.project);
                //return RedirectToAction();
            }

            var drop_down = new List<string>();
            drop_down = projectService.getCompanyName();
            ViewBag.drop_list = drop_down;

            return View();
        }
    }
}