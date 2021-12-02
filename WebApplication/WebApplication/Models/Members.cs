using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Members
    {
        [DisplayName("公司名稱")]
        [Required(ErrorMessage = "請輸入公司名稱")]
        public string companyName { get; set; }

        [DisplayName("公司統一編號(帳號)")]
        [Required(ErrorMessage = "請輸入公司統一編號 ")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "帳號長度介於6-30字元")]
        public string companyId { get; set; }
        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "密碼長度需介於8~30字元")]
        public string password { get; set; }
        [DisplayName("確認密碼")]
        [System.ComponentModel.DataAnnotations.Compare("password", ErrorMessage = "兩次輸入密碼不一致")]
        [Required(ErrorMessage = "請輸入確認密碼")]
        public string PasswordCheck { get; set; }
        [DisplayName("負責人姓名")]
        [StringLength(20, ErrorMessage = "姓名長度最多20字元")]
        [Required(ErrorMessage = "請輸入姓名")]
        public string companyLeader { get; set; }
        [DisplayName("公司地址")]
        [Required(ErrorMessage = "請輸入公司地址")]
        public string companyAddress { get; set; }
        [DisplayName("傳真電話")]
        [Required(ErrorMessage = "請輸入傳真電話")]
        public string fax { get; set; }


        [DisplayName("聯絡電話")]
        [Required(ErrorMessage = "請輸入聯絡電話")]
        public string telephone { get; set; }
        [DisplayName("Email1")]
        [StringLength(200, ErrorMessage = "Email長度最多200字元")]
        [Required(ErrorMessage = "請輸入Email")]
        [EmailAddress(ErrorMessage = "這不是Email格式")]
        public string email1 { get; set; }
        [DisplayName("Email2")]
        [StringLength(200, ErrorMessage = "Email長度最多200字元")]
        [EmailAddress(ErrorMessage = "這不是Email格式")]
        public string email2 { get; set; }
        [DisplayName("Email3")]
        [StringLength(200, ErrorMessage = "Email長度最多200字元")]
        [EmailAddress(ErrorMessage = "這不是Email格式")]
        public string email3 { get; set; }
        [DisplayName("Email4")]
        [StringLength(200, ErrorMessage = "Email長度最多200字元")]
        [EmailAddress(ErrorMessage = "這不是Email格式")]
        public string email4 { get; set; }
        [DisplayName("Email5")]
        [StringLength(200, ErrorMessage = "Email長度最多200字元")]
        [EmailAddress(ErrorMessage = "這不是Email格式")]
        public string email5 { get; set; }

    }
}