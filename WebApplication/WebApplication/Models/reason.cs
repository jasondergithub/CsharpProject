using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Reason
    {

        [DisplayName("試用報告")]
        public string reasonA { get; set; }
        [DisplayName("給付限制文件")]
        public string reasonB { get; set; }
        [DisplayName("產品圖檔")]
        public string reasonC { get; set; }
        [DisplayName("衛福部許可證")]
        public string reasonD { get; set; }
        [DisplayName("自費特材療效比較表")]
        public string reasonE { get; set; }
        [DisplayName("健保/自費收載")]
        public string reasonF { get; set; }
        [DisplayName("新進自費衛材售價申請書")]
        public string reasonG { get; set; }
        [DisplayName("自費特材保證切結書")]
        public string reasonH { get; set; }
        [DisplayName("自費收載價格參考")]
        public string reasonI { get; set; }
        [DisplayName("衛材滅菌檢驗報告單")]
        public string reasonJ { get; set; }
        [DisplayName("產品品質保證書")]
        public string reasonK { get; set; }
        [DisplayName("報價單")]
        public string reasonL { get; set; }
        [DisplayName("產品授權代理書")]    /* 13 */
        public string reasonM { get; set; }
        [DisplayName("產品目錄")]           /* 14 */
        public string reasonN { get; set; }
        [DisplayName("其他醫院使用證明")]
        public string reasonO { get; set; }
        [DisplayName("內含(處置治療表)")]
        public string reasonP { get; set; }
        [DisplayName("公會會員證書號")]
        public string reasonQ { get; set; }
        [DisplayName("工廠登記證")]
        public string reasonR { get; set; }
        [DisplayName("審議資格簽核單")]
        public string reasonS { get; set; }
        [DisplayName("醫療法21條")]
        public string reasonT { get; set; }
        [DisplayName("User理由")]
        public string reasonU { get; set; }


    }

}
