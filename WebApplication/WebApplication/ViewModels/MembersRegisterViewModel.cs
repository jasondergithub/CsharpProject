using WebApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;


namespace WebApplication.ViewModels
{
    public class MembersRegisterViewModel
    {
        public Members newMember { get; set; }
    }
}