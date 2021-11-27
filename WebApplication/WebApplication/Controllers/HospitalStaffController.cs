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
                projectService.Createproject(newProject.project, newProject.Name);
                //return RedirectToAction();
            }

            var drop_down = new List<string>();
            drop_down = projectService.getCompanyName();
            ViewBag.drop_list = drop_down;

            return View();
        }
    }
}