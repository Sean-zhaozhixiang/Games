using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorial.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name="姓名")]
        public string  UserName { get; set; }
        [Required]
        [Display(Name = "密码"),DataType(DataType.Password)]
        public string  PassWord { get; set; }
    }
}
