using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial.Web.ICore;
using Tutorial.Web.Models;

namespace Tutorial.Web.ViewComponents
{
    public class WelcomeViewComponent: ViewComponent
    {
        private readonly IResbiatiy<Students> _resbiatiy;
        public WelcomeViewComponent(IResbiatiy<Students> resbiatiy)
        {
            _resbiatiy = resbiatiy;
        }
        public IViewComponentResult Invoke()
        {
            var count = _resbiatiy.GetAll().Count();
            return View("Default",count.ToString());
        }
    }
}
