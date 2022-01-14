using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Models
{
    public class Project
    {
        public string projectId { get; set; }
        [DisplayName("專案類型")]
        [Required(ErrorMessage = "請選擇專案類型")]
        public string projectGenre { get; set; }

        public string companyName { get; set; }
        public string time { get; set; }
        public string season { get; set; }
        public string department { get; set; }
        public string hospitalUser { get; set; }
        public int projectNo { get; set; }
        public bool checkAB { get; set; }

        public bool judgeState { get; set; }

        //[DisplayName("選擇廠商")]
        public string companyId { get; set; }
        [DisplayName("請購原因")]
        [Required(ErrorMessage = "請輸入請購原因")]
        public string buyReason { get; set; }
        [DisplayName("用途")]
        [Required(ErrorMessage = "請輸入用途")]
        public string usage { get; set; }
        [DisplayName("預估月用量")]
        [Required(ErrorMessage = "請輸入預估月用量")]
        public string predictOfUsePerMonth { get; set; }
        [DisplayName("建議院內碼")]
        [Required(ErrorMessage = "請輸入建議院內碼")]
        public string recommandId { get; set; }





    }
}