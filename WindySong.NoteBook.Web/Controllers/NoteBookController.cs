﻿using System;
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
        public IActionResult Col()
        {
            return View();
        }

        /// <summary>
        /// cTab分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetTab(DataPageModel model)
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

        /// <summary>
        /// 添加TAB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddTab(TabModel model)
        {
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            bool bl = false;
            bl = _noteBookApp.AddTab(model);
            if(bl)
            {
                return Json(this.GetSwalJson(1, "添加成功", "success", "success"));
            }
            else
            {
                return Json(this.GetSwalJson(0, "添加失败", "error", "error"));
            }
        }

        /// <summary>
        /// 更新TAB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult UpdatTab(TabModel model)
        {
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            bool bl = false;
            bl = _noteBookApp.UpdateTab(model);
            if (bl)
            {
                return Json(this.GetSwalJson(1, "更新成功", "success", "success"));
            }
            else
            {
                return Json(this.GetSwalJson(0, "更新失败", "error", "error"));
            }
        }

        /// <summary>
        /// 删除TAB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult DeleteTab(TabDeleteModel model)
        {
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            var jsonResults = new JsonResultsString();
            int i = 0;
            i = _noteBookApp.DeleteTab(model);
            switch (i)
            {
                case 1:
                    jsonResults = this.GetSwalJson(1, "删除成功", "success", "success");
                    break;
                case 0:
                    jsonResults = this.GetSwalJson(1, "删除失败", "error", "error");
                    break;
                case -1:
                    jsonResults = this.GetSwalJson(-1, "删除失败", "选中的竖列存在未删除的选项卡", "error");
                    break;
            }
            return Json(jsonResults);
        }

        /// <summary>
        /// 获取选项卡
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTabSelect()
        {
            JsonTabSelect json = new JsonTabSelect();
            json = this._noteBookApp.GetTabSelect();

            return Json(json);
        }

        /// <summary>
        /// 添加Col
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddCol(ColModel model)
        {
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            bool bl = false;
            bl = _noteBookApp.AddCol(model);
            if (bl)
            {
                return Json(this.GetSwalJson(1, "添加成功", "success", "success"));
            }
            else
            {
                return Json(this.GetSwalJson(0, "添加失败", "error", "error"));
            }
        }
    }
}