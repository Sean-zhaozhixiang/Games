using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tutorial.Web.Models;

namespace Tutorial.Web.ViewModels
{
    public class StudentCreateViewModel
    {
        [Display(Name = "姓名")]
        [Required]
        public string username { get; set; }
        [Display(Name = "出生日期")]
        public DateTime birthDate { get; set; }
        [Display(Name = "性别")]
        public Gender gender { get; set; }
    }
}
