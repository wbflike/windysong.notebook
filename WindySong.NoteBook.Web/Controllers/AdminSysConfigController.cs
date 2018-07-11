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
    //属性过滤器
    [FieldFilter]
    public class AdminSysConfigController : ManageController
    {
        //用户登录业务接口
        private ISysConfigAppService _sysConfig;
        private ICacheService _cache;
        public AdminSysConfigController(ISysConfigAppService loginApp, ICacheService cashe)
        {
            this._sysConfig = loginApp;
            this._cache = cashe;
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
            if(string.IsNullOrEmpty(model.cdn))
            {
                model.cdn = "";
            }
            if(_sysConfig.SetSysConfig(model))
            {
                //更新静态类数据
                _cache.ReplaceAsync("sitename", model.siteName);
                _cache.ReplaceAsync("sitekey", model.siteKeyWords);
                _cache.ReplaceAsync("sitedes", model.siteDescription);
                _cache.ReplaceAsync("cdn", model.cdn);

                return Json(this.GetSwalJson(1, "保存成功", "success", "success"));
            }
            else
            {
                return Json(this.GetSwalJson(0, "保存失败", "error", "error"));
            }
        }
        public IActionResult StaticIndex()
        {
            return View();
        }

    }
}