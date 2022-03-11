using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial.Web.ViewModels;

namespace Tutorial.Web.Controllers
{
    public class AccountController: Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;//鉴权验证
        private readonly UserManager<IdentityUser> _userManager;//数据存储

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "输入不正确,请重新输入!");
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);//数据存储里查有没有这个user
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.PassWord, false, false);//验证密码是否成功
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "用户名/密码不正确");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser {
                    UserName=model.UserName
                };
                var result = await _userManager.CreateAsync(user, model.PassWord);
                if (result.Succeeded) { return RedirectToAction("Index", "Home"); }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}
