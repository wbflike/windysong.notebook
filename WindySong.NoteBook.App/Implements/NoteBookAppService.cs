using Chloe;
using System;
using System.Collections.Generic;
using System.Text;
using WindySong.NoteBook.App.Entity;
using WindySong.NoteBook.App.Interfaces;
using WindySong.NoteBook.App.ViewModels.Admin;
using WindySong.NoteBook.App.ViewModels.Json;

namespace WindySong.NoteBook.App.Implements
{
    /// <summary>
    /// 笔记操作接口实现类
    /// </summary>
    public class NoteBookAppService : AppService, INoteBookAppService
    {
        /// <summary>
        /// 返回cTab分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonPagTab GetPageTab(TabPostModel model)
        {
            var json = new JsonPagTab();
            //返回json的tab数据
            List<JsonTab> listJsonTab = new List<JsonTab>();
            //数据库查询的tab数据
            List<CTab> list = new List<CTab>();
            IQuery<CTab> q = this.DbContext.Query<CTab>();
            if(string.IsNullOrEmpty(model.searchKey))
            {
                //获取数据行数
                json.total = q.Count();
                //TakePage 第一个参数是page,第二个是pageNumber  offset表示走第几条数据开始取  limit表示取几条
                list = q.Where(a => 1 == 1).OrderBy(a => a.rank).TakePage((model.offset/ model.limit)+1, model.limit).ToList();
            }
            else
            {
                //获取数据行数
                json.total = q.Where(a => a.name.Contains(model.searchKey) || a.description.Contains(model.searchKey)).Count();
                //TakePage 第一个参数是page,第二个是pageNumber
                list = q.Where(a => a.name.Contains(model.searchKey) || a.description.Contains(model.searchKey)).OrderBy(a => a.rank).TakePage((model.offset / model.limit) + 1, model.limit).ToList();
            }
            foreach (var cTab in list)
            {
                var jsonTab = new JsonTab();
                jsonTab.id = cTab.id;
                jsonTab.name = cTab.name;
                jsonTab.description = cTab.description;
                jsonTab.rank = cTab.rank;
                jsonTab.lastTime = cTab.lastTime;
                listJsonTab.Add(jsonTab);
            }
            json.rows = listJsonTab;

            return json;
        }

        /// <summary>
        /// 添加TAB
        /// </summary>
        /// <param name="model">TabAddModel</param>
        /// <returns></returns>
        public bool AddTab(TabAddModel model)
        {
            CTab tab = new CTab();
            tab.name = model.addName;
            if(model.addDescription == null)
            {
                tab.description = "";
            }
            else
            {
                tab.description = model.addDescription;
            }
            if(model.addRank == 0)
            {
                IQuery<CTab> q = this.DbContext.Query<CTab>();
                var max = q.Max(a => a.rank);
                tab.rank = max + 1;
            }
            else
            {
                tab.rank = model.addRank;
            }
            tab.lastTime = DateTime.Now.ToString();
            try
            {
                tab = this.DbContext.Insert(tab);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
