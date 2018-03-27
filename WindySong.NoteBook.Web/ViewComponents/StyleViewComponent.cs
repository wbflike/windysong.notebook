using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindySong.NoteBook.App.Interfaces;

namespace WindySong.NoteBook.Web.ViewComponents
{
    public class StyleViewComponent : ViewComponent
    {
        private ICacheService _cache;
        public StyleViewComponent(ICacheService cashe)
        {
            this._cache = cashe;
        }
        public IViewComponentResult Invoke()
        {
            ViewData["cdn"] = _cache.Get("cdn").ToString();
            return View();
        }
    }
}
