using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WindySong.NoteBook.App.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace WindySong.NoteBook.Web.Controllers
{
    public class HomeController : Controller
    {
        private IMemoryCache _cache;
        public HomeController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        public IActionResult Index()
        {
            string bl = "";
            bl = _cache.Set("123", "Hello");
            return View();
        }
    }
}