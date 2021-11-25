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
        // GET: HospitalStaff
        private readonly ProjectService projectService = new ProjectService();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult CreateProject()
        {
            List<SelectListItem> mySelectItemList = new List<SelectListItem>();
            CreateProject model = new CreateProject()
            {
                companyNameList = projectService.getCompanyName(mySelectItemList)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateProject(Project project)
        {
            //projectService.Createroject(newProject.project);

            return View();
        }
    }
}