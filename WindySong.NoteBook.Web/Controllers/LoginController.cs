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
using Microsoft.AspNetCore.Http;
using Common;

namespace WindySong.NoteBook.Web.Controllers
{
    //属性过滤器
    [FieldFilter]
    public class LoginController : BaseController
    {
        private ICacheService _cache;
        //用户登录业务接口
        private ILoginAppService _loginApp;
        public LoginController(ILoginAppService loginApp, ICacheService cashe)
        {
            this._loginApp = loginApp;
            this._cache = cashe;
        }

        //[Route("Login")] /*路由特性定义 这个路由特性表示 URL/Login 将访问到这个Action*/
        [HttpGet]
        public IActionResult Index(String ReturnUrl)
        {
            ViewData["cdn"] = _cache.Get("cdn").ToString();
            //如果用户没有登录
            if (!User.Identity.IsAuthenticated)
            {
                //URL不为空
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    ViewBag.returnUrl = ReturnUrl;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return new RedirectResult(ReturnUrl);
                }
                else
                {
                    return RedirectToRoute(new { controller = "Admin", action = "Index" });
                }
                
            }
            
            return View();
        }

        //Token验证，防止CSRF XSS攻击
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(LoginModel loginModel)
        {
            ViewData["cdn"] = _cache.Get("cdn").ToString();
            //验证模型的数据验证是否通过
            if (!ModelState.IsValid)
            {
                this.IfModelStateViewData();
                return View();
            }
            //判断验证码是否合法
            string clientValidCode = HttpContext.Session.GetString("ValidCode");
            if(loginModel.validateCode != clientValidCode)
            {
                ViewData["formError"] = "验证码错误!";
                return View();
            }

            //登录验证方法
            LoginModel _logigModel = _loginApp.UserLogin(loginModel);
            //登录失败
            if(_logigModel == null)
            {
                ViewData["formError"] = "用户名密码失败!";
                return View();
            }
            else
            {
                //添加cookies
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, _logigModel.name));
                identity.AddClaim(new Claim(ClaimTypes.Sid, _logigModel.id.ToString()));
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
                if (!string.IsNullOrEmpty(loginModel.returnUrl))
                {
                    //返回URL
                    return new RedirectResult(loginModel.returnUrl);
                }
                else
                {
                    return RedirectToRoute(new { controller = "Admin", action = "Index" });//跳转到其他controller RedirectToAction("Admin/Index");//调整到其他Action
                }
                
            }
        }

        public IActionResult ValidateCode()
        {
            string code = Commonly.GetRandomString(4);
            HttpContext.Session.SetString("ValidCode", code);
            return File(this.GetValidCodeByte(code), "image/png");
        }

    }
}