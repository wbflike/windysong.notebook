using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WindySong.NoteBook.Web.Common;
using WindySong.NoteBook.App.ViewModels.Admin;
using WindySong.NoteBook.App.Interfaces;
using WindySong.NoteBook.App.Entity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using WindySong.NoteBook.App;

namespace WindySong.NoteBook.Web.Controllers
{
    public class LoginController : BaseController
    {
        //用户登录业务接口
        private ILoginAppService _loginApp;
        public LoginController(ILoginAppService loginApp)
        {
            this._loginApp = loginApp;           
        }

        //[Route("Login")] /*路由特性定义 这个路由特性表示 URL/Login 将访问到这个Action*/
        public IActionResult Index()
        {
            this.GetSysConfig();
            //当存在cookies
            if (User.Identity.Name != null)
            {
                AppService app = new AppService();
                bool bl = false;
                bl = app.IfUser(User.Identity.Name);
                //当存在的cookies用户名在数据库不存在则删除cookies
                if (bl == false)
                {
                    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
            }
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
            //登录验证方法
            Users users = _loginApp.UserLogin(loginModel);
            //登录失败
            if(users == null)
            {
                ViewData["formError"] = "用户名密码失败!";
                return View();
            }
            else
            {
                //添加cookies
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, users.userName));
                identity.AddClaim(new Claim(ClaimTypes.Sid, users.id.ToString()));
                //设置有效期
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
                {
                    // 持久保存
                    IsPersistent = true,

                    // 指定过期时间AddMinutes 添加分钟
                    //ExpiresUtc = DateTime.UtcNow.AddMinutes(20)\
                    // 指定过期时间AddMinutes 添加天
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                });
                return RedirectToRoute(new { controller = "AdminIndex", action = "Index" });//跳转到其他controller RedirectToAction("AdminIndex/Index");//调整到其他Action
            }
        }

    }
}