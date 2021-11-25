using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Services;
using WebApplication.ViewModels;


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
            return View();
        }
        [HttpPost]
        public ActionResult CreateProject(CreateProject newProject)
        {
            projectService.Createroject(newProject.project);
            
            return View();
        }
    }
}