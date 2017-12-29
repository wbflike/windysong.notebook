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

        public void IfModelState()
        {
            string error = "";
            //遍历model错误信息
            foreach (var item in ModelState.Keys)
            {
                //strItem属性名也就是表单名 
                string strItem = item;
                foreach (var p in ModelState[item].Errors)
                {
                    //错误信息
                    error += p.ErrorMessage;
                }

            }
            ViewData["formError"] = error;
        }

    }
}