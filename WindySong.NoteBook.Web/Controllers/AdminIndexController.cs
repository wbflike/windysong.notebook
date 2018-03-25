using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WindySong.NoteBook.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WindySong.NoteBook.Web.Controllers
{
    //权限验证
    [Authorize]
    public class AdminController : ManageController
    {

        public IActionResult Index()
        {
            ViewData["UserName"] = User.Identity.Name;
            ViewBag.UserPhoto = this.GetUserPhoto();
            return View();
        }

        public IActionResult Main()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            //删除cookies
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
    }
}