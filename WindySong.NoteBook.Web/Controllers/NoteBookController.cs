using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WindySong.NoteBook.Web.Common;
using Microsoft.AspNetCore.Authorization;
using WindySong.NoteBook.App.ViewModels.Admin;
using WindySong.NoteBook.App.Interfaces;
using WindySong.NoteBook.App.ViewModels.Json;

namespace WindySong.NoteBook.Web.Controllers
{
    //权限验证
    [Authorize]
    public class NoteBookController : AdminController
    {
        //笔记操作接口
        private INoteBookAppService _noteBookApp;
        public NoteBookController(INoteBookAppService noteBookApp)
        {
            this._noteBookApp = noteBookApp;
        }
        public IActionResult Tab()
        {
            return View();
        }

        /// <summary>
        /// cTab分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TabJson(TabPostModel model)
        {
            JsonPagTab jsonPagTab = new JsonPagTab();
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                jsonPagTab.total = 1;
                List<JsonTab> list = new List<JsonTab>();
                list.Add(new JsonTab() { id = 1, name = "参数非法", description = "参数非法", rank = 1, lastTime = "参数非法" });
                jsonPagTab.rows = list;
                return Json(jsonPagTab);
            }
            jsonPagTab = this._noteBookApp.GetPageTab(model);

            return Json(jsonPagTab);
        }
    }
}