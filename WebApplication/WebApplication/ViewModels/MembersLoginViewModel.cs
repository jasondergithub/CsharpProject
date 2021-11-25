using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.ViewModels
{
    public class MembersLoginViewModel 
    { 
            [DisplayName("會員帳號(統編)")]
            [Required(ErrorMessage = "請輸入會員帳號")]
            public string companyId { get; set; }
            [DisplayName("會員密碼")]
            [Required(ErrorMessage = "請輸入會員密碼")]
            public string password { get; set; }
    }
}

