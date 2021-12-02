using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication.Services;
using WebApplication.Models;
using WebApplication.ViewModels;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly MembersDBService membersDBServices = new MembersDBService();
        private readonly createDirectory createDirectory = new createDirectory();
        private readonly uploadFileService uploadObject = new uploadFileService();
        public ActionResult Index()
        {
            return View();   
        }

        public ActionResult CompanyLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CompanyLogin(MembersLoginViewModel LoginMember)
        {
            string ValidateStr = membersDBServices.LoginCheck(LoginMember.companyId, LoginMember.password);
            //判斷驗證結果是否有錯誤訊息
            if (String.IsNullOrEmpty(ValidateStr)) 
            {
                // 無錯誤訊息則登入
                // 先藉由 Services 取得登入者角色資料
                string RoleData = "CompanyStaff";
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, LoginMember.companyId, DateTime.Now, DateTime.Now.AddMinutes(10), false, RoleData);
                string encryptTicket = FormsAuthentication.Encrypt(ticket);
                System.Web.HttpCookie authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                //FormsAuthentication.RedirectFromLoginPage(LoginMember.Account, true);
                Session["account"] = LoginMember.companyId;
                return RedirectToAction("Index", "CompanyStaff");
            }
            else
            {
                //有驗證錯誤訊息
                ModelState.AddModelError("", ValidateStr);
                //將資料填回至View
                return View(LoginMember);
            }
        }
        public ActionResult StaffLogin()
        {
            return View();
        }
        public ActionResult AdminLogin()
        {
            return View();
        }
        public ActionResult CreateProject()
        {
            return View();
        }
        public ActionResult Register()
        {
            ViewBag.accountExist = false;
            ViewBag.DirCreateSucc = false;
            ViewBag.notEnoughPhoto = false;
            return View();
        }
        [HttpPost]
        public ActionResult Register(MembersRegisterViewModel RegisterMember, HttpPostedFileBase[] photos)
        {

            if (ModelState.IsValid)
            {
                if (photos[0] == null || photos[1] == null || photos[2] == null)
                {
                    ViewBag.notEnoughPhoto = true;
                    ViewBag.accountExist = false;
                    ViewBag.DirCreateSucc = false;
                }
                else
                {
                    ViewBag.notEnoughPhoto = false;
                    //新建Members, 
                    Members newMember = new Members();
                    newMember.companyName = RegisterMember.newMember.companyName;
                    newMember.companyId = RegisterMember.newMember.companyId;                             //帳號
                    newMember.password = RegisterMember.newMember.password;
                    newMember.companyLeader = RegisterMember.newMember.companyLeader;
                    newMember.companyAddress = RegisterMember.newMember.companyAddress;
                    newMember.fax = RegisterMember.newMember.fax;
                    newMember.telephone = RegisterMember.newMember.telephone;
                    newMember.email1 = RegisterMember.newMember.email1;

                    //呼叫MemberDBService.cs中的Register創建公司資料表
                    ViewBag.accountExist = membersDBServices.Register(newMember);
                    // 呼叫createDirectory.cs 中的createDir 創建資料夾
                    ViewBag.DirCreateSucc = createDirectory.createDir(newMember.companyId);
                    //建立公司(統編)資料夾下的三個資料夾 (1->販售許可(sp),  2->3個月(3m),  3->公司證(cp))
                    List<string> myStringLists = new List<string>();
                    myStringLists.Add("sp");
                    myStringLists.Add("3m");
                    myStringLists.Add("cp");
                    foreach (string i in myStringLists)
                    {
                        string subDirName = newMember.companyId + "/" + i;
                        ViewBag.DirCreateSucc = createDirectory.createDir(subDirName);
                    }

                    for (int order = 0; order <= 2; order++)
                    {
                        HttpPostedFileBase f = (HttpPostedFileBase)photos[order];
                        uploadObject.UploadToFtp(f, newMember.companyId, order + 1);
                    }
                }
            }
                       
            return View();
        }
        public ActionResult RegisterResult()
        {
            return View();
        }

    }
}