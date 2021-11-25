using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Models
{
    public class Project
    {
        public string projectId { get; set; }
        [DisplayName("專案類型")]
        public string projectGenre { get; set; }

        public string time { get; set; }
        public string season { get; set; }
        public string department { get; set; }
        public string hospitalUser { get; set; }
        public string projectNo { get; set; }
        public bool checkAB { get; set; }

        public bool judgeState { get; set; }

        //[DisplayName("選擇廠商")]
        //public string companyId { get; set; }




    }
}