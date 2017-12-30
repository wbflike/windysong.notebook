using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WindySong.NoteBook.Web.ViewModels.Admin;
using WindySong.NoteBook.Web.Common;

namespace WindySong.NoteBook.Web.Controllers
{
    public class LoginController : BaseController
    {
        
        //[Route("Login")] /*路由特性定义 这个路由特性表示 URL/Login 将访问到这个Action*/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginModel loginModel)
        {
            //验证模型的数据验证是否通过
            if (!ModelState.IsValid)
            {
                this.IfModelState();
                return View();
            }
            return View();
        }

    }
}