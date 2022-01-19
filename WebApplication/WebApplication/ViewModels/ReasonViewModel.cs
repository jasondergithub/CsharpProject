using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class ReasonViewModel
    {
        public Reason whyNoFile { get; set; }
        public Judge judgeBox { get; set; }
        public Reject rejectReason { get; set; }
    }
}