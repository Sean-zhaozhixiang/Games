using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Tutorial.Web.Controllers
{

    //[Route("about")]
    //[Route("[controller]")]
    //[Route("v2/[controller]/[action]")]
    public class AboutController : Controller
    {
        //[Route("[action]")]
        public string Me()
        {
            return "zzx";
        }
        //[Route("[action]")]
        public string You()
        {
            return "wlq";
        }
    }
}