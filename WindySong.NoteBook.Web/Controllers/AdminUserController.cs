using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WindySong.NoteBook.Web.Common;
using Microsoft.AspNetCore.Authorization;
using WindySong.NoteBook.App.Interfaces;
using WindySong.NoteBook.App.ViewModels.Admin;
using WindySong.NoteBook.App.ViewModels.Json;

namespace WindySong.NoteBook.Web.Controllers
{
    //权限验证
    [Authorize]
    public class AdminUserController : AdminController
    {
        //用户操作接口
        private IUserAppService _userApp;
        public AdminUserController(IUserAppService userApp)
        {
            this._userApp = userApp;
        }
        public IActionResult Password()
        {
            return View();
        }
        //Token验证，防止CSRF XSS攻击
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Password(UpdatePasswordModel model)
        {
            var jsonResults = new JsonResultsString();
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            int i = 0;
            i = _userApp.UpdatePassword(User.Identity.Name, model);
            switch (i)
            {
                case 1:
                    jsonResults = this.GetSwalJson(1, "保存成功", "success", "success");
                    break;
                case 0:
                    jsonResults = this.GetSwalJson(1, "保存失败", "error", "error");
                    break;
                case -1:
                    jsonResults = this.GetSwalJson(-1, "保存失败,用户不存在", "error", "error");
                    break;
                case -2:
                    jsonResults = this.GetSwalJson(-1, "保存失败,旧密码错误", "error", "error");
                    break;
            }
            return Json(jsonResults);
        }
    }
}