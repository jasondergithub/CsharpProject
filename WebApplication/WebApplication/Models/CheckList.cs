using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CheckList
    {
        [DisplayName("請四擇一")]
        public int three_six { get; set; }
        [DisplayName("請二擇一")]
        public int three { get; set; }
        [DisplayName("健保碼")]
        public string three_1{ get; set; }
        [DisplayName("健保價")]
        public string three_2 { get; set; }
        [DisplayName("請三擇一")]
        public int four { get; set; }
        [DisplayName("自費碼")]
        public string four_1 { get; set; }
        [DisplayName("HTA")]
        public string four_2 { get; set; }
        [DisplayName("相關處置碼")]
        public string five_1 { get; set; }
        [DisplayName("使用說明")]
        public string five_2 { get; set; }
        [DisplayName("內含金額")]
        public string five_3 { get; set; }



    }
}