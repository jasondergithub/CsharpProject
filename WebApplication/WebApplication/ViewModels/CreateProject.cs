using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class CreateProject
    {
        public List<SelectListItem> companyNameList { get; set; }
        public Project project { get; set; }

        public string Name { get; set; }
    }
}