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
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            if(_sysConfig.SetSysConfig(model))
            {
                //更新静态类数据
                WebSiteSysConfig.siteName = model.siteName;
                WebSiteSysConfig.siteKeyWords = model.siteKeyWords;
                WebSiteSysConfig.siteDescription = model.siteDescription;
                return Json(this.GetSwalJson(1, "保存成功", "success", "success"));
            }
            else
            {
                return Json(this.GetSwalJson(0, "保存失败", "error", "error"));
            }
        }
    }
}