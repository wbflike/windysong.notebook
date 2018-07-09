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
    //字段过滤器
    [FieldFilter]
    public class NoteBookController : ManageController
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
        public IActionResult Block()
        {
            return View();
        }
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Api()
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
            JsonPagTab jsonPag = new JsonPagTab();
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                jsonPag.total = 1;
                List<JsonTab> list = new List<JsonTab>();
                list.Add(new JsonTab() { id = 1, name = "参数非法", description = "参数非法", rank = 1, lastTime = "参数非法" });
                jsonPag.rows = list;
                return Json(jsonPag);
            }
            jsonPag = this._noteBookApp.GetPageTab(model);

            return Json(jsonPag);
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
        public IActionResult DeleteTab(DeleteModel model)
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
                    jsonResults = this.GetSwalJson(0, "删除失败", "error", "error");
                    break;
                case -1:
                    jsonResults = this.GetSwalJson(-1, "删除失败", "选中的选项卡存在关联的竖列", "error");
                    break;
            }
            return Json(jsonResults);
        }

        /// <summary>
        /// 获取选项卡
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetTabSelect()
        {
            JsonSelect json = new JsonSelect();
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

        /// <summary>
        /// Col分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ColJson(DataPageModel model)
        {
            JsonPagCol jsonPag = new JsonPagCol();
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                jsonPag.total = 1;
                List<JsonCol> list = new List<JsonCol>();
                list.Add(new JsonCol() { id = 1,tabId = 1,tabName = "参数非法", name = "参数非法",  rank = 1, lastTime = "参数非法" });
                jsonPag.rows = list;
                return Json(jsonPag);
            }
            jsonPag = this._noteBookApp.GetPageCol(model);

            return Json(jsonPag);
        }

        /// <summary>
        /// 更新Col
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult UpdatCol(ColModel model)
        {
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            bool bl = false;
            bl = _noteBookApp.UpdateCol(model);
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
        public IActionResult DeleteCol(DeleteModel model)
        {
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            var jsonResults = new JsonResultsString();
            int i = 0;
            i = _noteBookApp.DeleteCol(model);
            switch (i)
            {
                case 1:
                    jsonResults = this.GetSwalJson(1, "删除成功", "success", "success");
                    break;
                case 0:
                    jsonResults = this.GetSwalJson(0, "删除失败", "error", "error");
                    break;
                case -1:
                    jsonResults = this.GetSwalJson(-1, "删除失败", "选中的竖列存在关联的一级分类", "error");
                    break;
            }
            return Json(jsonResults);
        }

        /// <summary>
        /// 获取选项卡
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetColSelect()
        {
            JsonSelect json = new JsonSelect();
            json = this._noteBookApp.GetColSelect();

            return Json(json);
        }

        /// <summary>
        /// 添加Block
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddBlock(BlockModel model)
        {
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            bool bl = false;
            bl = _noteBookApp.AddBlock(model);
            if (bl)
            {
                return Json(this.GetSwalJson(1, "添加成功", "success", "success"));
            }
            else
            {
                return Json(this.GetSwalJson(0, "添加失败", "error", "error"));
            }
        }

        /// <summary>
        /// Block分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult BlockJson(DataPageModel model)
        {
            JsonPagBlock jsonPag = new JsonPagBlock();
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                jsonPag.total = 1;
                List<JsonBlock> list = new List<JsonBlock>();
                list.Add(new JsonBlock() { id = 1, colId = 1, colName = "参数非法", name = "参数非法", rank = 1, lastTime = "参数非法" });
                jsonPag.rows = list;
                return Json(jsonPag);
            }
            jsonPag = this._noteBookApp.GetPageBlock(model);

            return Json(jsonPag);
        }

        /// <summary>
        /// 更新Block
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult UpdatBlock(BlockModel model)
        {
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            bool bl = false;
            bl = _noteBookApp.UpdateBlock(model);
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
        /// 删除Block
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult DeleteBlock(DeleteModel model)
        {
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            var jsonResults = new JsonResultsString();
            int i = 0;
            i = _noteBookApp.DeleteBlock(model);
            switch (i)
            {
                case 1:
                    jsonResults = this.GetSwalJson(1, "删除成功", "success", "success");
                    break;
                case 0:
                    jsonResults = this.GetSwalJson(0, "删除失败", "error", "error");
                    break;
                case -1:
                    jsonResults = this.GetSwalJson(-1, "删除失败", "选中的一级分类存在关联的二级分类", "error");
                    break;
            }
            return Json(jsonResults);
        }


        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetBlockSelect()
        {
            JsonSelect json = new JsonSelect();
            json = this._noteBookApp.GetBlockSelect();

            return Json(json);
        }

        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <param name="id">TabID</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetBlockSelect(int id)
        {
            JsonSelect json = new JsonSelect();
            json = this._noteBookApp.GetBlockSelect(id);

            return Json(json);
        }

        /// <summary>
        /// 添加List
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddList(ListModel model)
        {
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            bool bl = false;
            bl = _noteBookApp.AddList(model);
            if (bl)
            {
                return Json(this.GetSwalJson(1, "添加成功", "success", "success"));
            }
            else
            {
                return Json(this.GetSwalJson(0, "添加失败", "error", "error"));
            }
        }

        /// <summary>
        /// List分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListJson(DataPageModel model)
        {
            JsonPagList jsonPag = new JsonPagList();
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                jsonPag.total = 1;
                List<JsonList> list = new List<JsonList>();
                list.Add(new JsonList() { id = 1, blockId = 1, blockName = "参数非法", name = "参数非法", rank = 1, lastTime = "参数非法" });
                jsonPag.rows = list;
                return Json(jsonPag);
            }
            jsonPag = this._noteBookApp.GetPageList(model);

            return Json(jsonPag);
        }

        /// <summary>
        /// 更新List
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult UpdatList(ListModel model)
        {
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            bool bl = false;
            bl = _noteBookApp.UpdateList(model);
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
        /// 删除List
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult DeleteList(DeleteModel model)
        {
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            var jsonResults = new JsonResultsString();
            int i = 0;
            i = _noteBookApp.DeleteList(model);
            switch (i)
            {
                case 1:
                    jsonResults = this.GetSwalJson(1, "删除成功", "success", "success");
                    break;
                case 0:
                    jsonResults = this.GetSwalJson(0, "删除失败", "error", "error");
                    break;
                case -1:
                    jsonResults = this.GetSwalJson(-1, "删除失败", "选中的二级分类存在关联的API", "error");
                    break;
            }
            return Json(jsonResults);
        }

        /// <summary>
        /// 获取二级分类
        /// </summary>
        /// <param name="id">BlockID</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetListSelect(int id)
        {
            JsonSelect json = new JsonSelect();
            json = this._noteBookApp.GetListSelect(id);

            return Json(json);
        }

        /// <summary>
        /// 添加Api
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddApi(ApiModel model)
        {
            string strName = System.Web.HttpUtility.HtmlEncode(model.Name);
            string strPar = System.Web.HttpUtility.HtmlEncode(model.Parameter);
            string strDec = System.Web.HttpUtility.HtmlEncode(model.Description);
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            bool bl = false;
            bl = _noteBookApp.AddApi(model);
            if (bl)
            {
                return Json(this.GetSwalJson(1, "添加成功", "success", "success"));
            }
            else
            {
                return Json(this.GetSwalJson(0, "添加失败", "error", "error"));
            }
        }

        /// <summary>
        /// Api分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ApiJson(DataPageModel model)
        {
            JsonPagApi jsonPag = new JsonPagApi();
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                jsonPag.total = 1;
                List<JsonApi> list = new List<JsonApi>();
                list.Add(new JsonApi() { id = 1, name = "参数非法", parameter = "参数非法", tabId = 1, tabName= "参数非法", blockId=1, blockName= "参数非法", listId=1, listName= "参数非法", rank = 1, lastTime = "参数非法" });
                jsonPag.rows = list;
                return Json(jsonPag);
            }
            jsonPag = this._noteBookApp.GetPageApi(model);

            return Json(jsonPag);
        }

        /// <summary>
        /// 获取api
        /// </summary>
        /// <param name="id">TabID</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetApi(int id)
        {
            ApiModel json = new ApiModel();
            json = this._noteBookApp.GetApi(id);

            return Json(json);
        }

        /// <summary>
        /// 更新Api
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult UpdatApi(ApiModel model)
        {
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            bool bl = false;
            bl = _noteBookApp.UpdateApi(model);
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
        /// 删除Api
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult DeleteApi(DeleteModel model)
        {
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            var jsonResults = new JsonResultsString();
            int i = 0;
            i = _noteBookApp.DeleteApi(model);
            switch (i)
            {
                case 1:
                    jsonResults = this.GetSwalJson(1, "删除成功", "success", "success");
                    break;
                case 0:
                    jsonResults = this.GetSwalJson(0, "删除失败", "error", "error");
                    break;
            }
            return Json(jsonResults);
        }
    }
}