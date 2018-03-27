using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindySong.NoteBook.App.Interfaces;

namespace WindySong.NoteBook.Web.ViewComponents
{
    public class HeadViewComponent : ViewComponent
    {
        private ICacheService _cache;
        public HeadViewComponent(ICacheService cashe)
        {
            this._cache = cashe;
        }
        public IViewComponentResult Invoke()
        {
            ViewData["sitename"] = _cache.Get("sitename").ToString();
            ViewData["sitekey"] = _cache.Get("sitekey").ToString();
            ViewData["sitedes"] = _cache.Get("sitedes").ToString();
            ViewData["cdn"] = _cache.Get("cdn").ToString();
            return View();
        }
    }
}
