﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WindySong.NoteBook.Web.Common;

namespace WindySong.NoteBook.Web.Controllers
{
    public class AdminIndexController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Main()
        {
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
    }
}