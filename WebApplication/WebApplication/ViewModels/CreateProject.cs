using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class CreateProject
    { 
        [DisplayName("選擇廠商")]
        [Required(ErrorMessage = "請選擇廠商")]
        public string Name { get; set; }
        public Project project { get; set; }      
    }
}