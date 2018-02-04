using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WindySong.NoteBook.Web.Common;
using Microsoft.AspNetCore.Authorization;
using WindySong.NoteBook.App.ViewModels.Admin;

namespace WindySong.NoteBook.Web.Controllers
{
    //权限验证
    [Authorize]
    public class NoteBookController : AdminController
    {
        public IActionResult Tab()
        {
            return View();
        }
        public IActionResult TabTwo()
        {
            return View();
        }

        //Token验证，防止CSRF XSS攻击
        [ValidateAntiForgeryToken]
        [HttpGet]
        public IActionResult TabJson(TabPostModel model)
        {
            return View();
        }
    }
}