using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tutorial.Web.ICore;
using Tutorial.Web.Models;
using Tutorial.Web.ViewModels;

namespace Tutorial.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IResbiatiy<Students> _resbiatiy;
        //依赖注入
        public HomeController(IResbiatiy<Students> resbiatiy)
        {
            _resbiatiy = resbiatiy;
        }
        public IActionResult Index()
        {
            var list = _resbiatiy.GetAll();
            var vms = list.Select(s => new StudenViewModel
            {
                id=s.id,
                Name = $"我的表示序号是{s.id}  姓名是:{s.username}",
                Age = DateTime.Now.Subtract(s.birthDate).Days / 365
            });
            var vm = new HomeIndexViewModel
            {
                Students = vms
            };
            if (vms != null)
            {
                return View(vm);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Detail(int id)
        {

            var student = _resbiatiy.GetUserById(id);
            if (student == null)
            {
                //return NotFound();
                return RedirectToAction("Index");
            }
            return View(student);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]//针对跨站请求
        public IActionResult Create(StudentCreateViewModel student)
        {
            if (!ModelState.IsValid) { ModelState.AddModelError(string.Empty, "Model Level Error"); return View(); }
            var newModel = new Students {
                username = student.username,
                birthDate=student.birthDate,
                gender=student.gender
            };
            var result = _resbiatiy.Add(newModel);
            //为了防止post的重复提交，应该重定向到另外一个页面里来重新请求数据 就可以解决重复提交的问题(浏览器的地址会改变 重新请求了)
            return RedirectToAction(nameof(Detail), new { id=result.id});//nameof有利于重构
        }

    }
}