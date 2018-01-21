using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WindySong.NoteBook.Web.Common;
using Microsoft.AspNetCore.Authorization;

namespace WindySong.NoteBook.Web.Controllers
{
    public class AdminIndexController : AdminController
    {
        //权限验证
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        //权限验证
        [Authorize]
        public IActionResult Main()
        {
            return View();
        }
        //权限验证
        [Authorize]
        public IActionResult Test()
        {
            return View();
        }
    }
}