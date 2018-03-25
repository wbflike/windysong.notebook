using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WindySong.NoteBook.Web.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("errors/{statusCode}")]
        public IActionResult CustomError(int statusCode)
        {
            if (statusCode == 404)
            {
                //直接返回静态页
                return new RedirectResult("/404.html");
            }
            return new RedirectResult("/404.html");
        }
    }
}