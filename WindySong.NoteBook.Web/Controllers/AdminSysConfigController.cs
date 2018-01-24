using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WindySong.NoteBook.Web.Common;
using WindySong.NoteBook.App.Interfaces;
using WindySong.NoteBook.App.ViewModels.Admin;
using WindySong.NoteBook.App.ViewModels.Json;

namespace WindySong.NoteBook.Web.Controllers
{
    //权限验证
    [Authorize]
    public class AdminSysConfigController : AdminController
    {
        //用户登录业务接口
        private ISysConfigAppService _sysConfig;
        public AdminSysConfigController(ISysConfigAppService loginApp)
        {
            this._sysConfig = loginApp;
        }
        public IActionResult Index()
        {
            SysConfigModel model = _sysConfig.GetSysConfig();
            return View(model);
        }
        //Token验证，防止CSRF XSS攻击
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(SysConfigModel model)
        {
            var jsonResults = new JsonResultsString();
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                jsonResults.state = 0;
                jsonResults.str = this.IfModelStateString();
                return Json(jsonResults);
            }
            if(_sysConfig.SetSysConfig(model))
            {
                jsonResults.state = 1;
                jsonResults.str = "success";
                WebSiteSysConfig.siteName = model.siteName;
                WebSiteSysConfig.siteKeyWords = model.siteKeyWords;
                WebSiteSysConfig.siteDescription = model.siteDescription;
                return Json(jsonResults);
            }
            else
            {
                jsonResults.state = 0;
                jsonResults.str = "error";
                return Json(jsonResults);
            }
        }
    }
}