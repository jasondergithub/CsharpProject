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
        public string projectId { get; set; }
        public string two { get; set; }
        [DisplayName("許可證效期")]
        public string two_1 { get; set; }
        [DisplayName("醫療器材級數")]
        public string two_2 { get; set; }
        [DisplayName("醫器主類別一")]
        public string two_3 { get; set; }
        [DisplayName("醫器次類別一")]     
        public string two_4 { get; set; }
        [DisplayName("適用範圍、科別")]
        public string two_5 { get; set; }
        [DisplayName("請四擇一")]
        public string three_six { get; set; }
        [DisplayName("請二擇一")]
        public string three { get; set; }
        [DisplayName("健保碼")]
        public string three_1{ get; set; }
        [DisplayName("健保價")]
        public string three_2 { get; set; }
        [DisplayName("請三擇一")]
        public string four { get; set; }
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
        [DisplayName("是否為滅菌品")]
        public string seven { get; set; }
        /* 品質保證書 */
        public string eight { get; set; }
        /* 報價單 */
        public string nine { get; set; }
        /* 其他醫院使用證明 */
        [DisplayName("其他醫院使用證明")]
        public string ten { get; set; }
        public string sub_ten { get; set; }
        [DisplayName("醫院名稱一")]
        public string ten_1 { get; set; }
        [DisplayName("醫院名稱一")]
        public string ten_2 { get; set; }
        [DisplayName("醫院名稱二")]
        public string ten_3 { get; set; }
        /* 清楚圖檔 */
        public string eleven { get; set; }
        /* 試用報告 */
        public string thirteen { get; set; }
        /* 審議資格簽核單 */
        public string fourteen { get; set; }
        public string fifteen { get; set; }



    }
}