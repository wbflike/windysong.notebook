using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WindySong.NoteBook.App.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using WindySong.NoteBook.Web.Common;
using Microsoft.AspNetCore.Authorization;

namespace WindySong.NoteBook.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IFrontIndex _frontIndex;
        private ICacheService _cache;
        public HomeController(IFrontIndex frontIndex, ICacheService cashe)
        {
            _frontIndex = frontIndex;
            this._cache = cashe;
        }
        public IActionResult Index()
        {
            ViewData["sitename"] = _cache.Get("sitename").ToString();
            ViewData["sitekey"] = _cache.Get("sitekey").ToString();
            ViewData["sitedes"] = _cache.Get("sitedes").ToString();
            ViewData["cdn"] = _cache.Get("cdn").ToString();
            //直接返回静态页
            //return new RedirectResult("/index.html");
            return View();
        }
        //权限验证
        [Authorize]
        //静态文件生成过滤器
        [StaticPageActionFilter]
        public IActionResult Default()
        {
            ViewData["sitename"] = _cache.Get("sitename").ToString();
            ViewData["sitekey"] = _cache.Get("sitekey").ToString();
            ViewData["sitedes"] = _cache.Get("sitedes").ToString();
            ViewData["cdn"] = _cache.Get("cdn").ToString();
            return View(_frontIndex.GetIndex());
        }
    }
}