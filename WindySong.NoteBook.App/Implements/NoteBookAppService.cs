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
        public JsonPagTab GetPageTab(DataPageModel model)
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
        public bool AddTab(TabModel model)
        {
            CTab tab = new CTab();
            tab.name = model.Name;
            if(model.Description == null)
            {
                tab.description = "";
            }
            else
            {
                tab.description = model.Description;
            }
            if(model.Rank == 0 || model.Rank == null)
            {
                IQuery<CTab> q = this.DbContext.Query<CTab>();
                var max = q.Max(a => a.rank);
                tab.rank = max + 1;
            }
            else
            {
                tab.rank = model.Rank.Value;
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

        /// <summary>
        /// 更新TAB
        /// </summary>
        /// <param name="model">UpdateAddModel</param>
        /// <returns></returns>
        public bool UpdateTab(TabModel model)
        {
            CTab tab = new CTab();
            IQuery<CTab> qCTab = this.DbContext.Query<CTab>();
            tab = qCTab.Where(a => a.id == model.Id).FirstOrDefault();
            tab.name = model.Name;
            if (model.Description == null)
            {
                tab.description = "";
            }
            else
            {
                tab.description = model.Description;
            }
            if (model.Rank == 0 || model.Rank == null)
            {
                IQuery<CTab> q = this.DbContext.Query<CTab>();
                var max = q.Max(a => a.rank);
                tab.rank = max + 1;
            }
            else
            {
                tab.rank = model.Rank.Value;
            }
            try
            {
                this.DbContext.Update(tab);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除TAB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteTab(TabDeleteModel model)
        {
            string str = model.deleteid;
            str = str.Substring(0, str.Length - 1);
            string[] arrId = str.Split(',');                                                                                          
            foreach(var id in arrId)
            {
                int i = 0;
                i = this.DbContext.Query<UCol>().Where(u => u.cTabId == int.Parse(id)).Count();
                if(i>0)//uCol存在cTab记录
                {
                    return -1;
                }
            }
            try
            {
                foreach (var id in arrId)
                {
                    this.DbContext.Delete<CTab>(a => a.id == int.Parse(id));
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取Tab Select
        /// </summary>
        /// <returns></returns>
        public JsonTabSelect GetTabSelect()
        {
            var json = new JsonTabSelect();
            //返回json的tab数据
            List<JsonTabValue> listJsonTabValue = new List<JsonTabValue>();
            //数据库查询的tab数据
            List<CTab> list = new List<CTab>();
            IQuery<CTab> q = this.DbContext.Query<CTab>();
            list = q.Where(a => 1 == 1).OrderBy(a => a.rank).ToList();
            foreach (var cTab in list)
            {
                var jsonTab = new JsonTabValue();
                jsonTab.id = cTab.id;
                jsonTab.name = cTab.name;
                listJsonTabValue.Add(jsonTab);
            }
            json.options = listJsonTabValue;

            return json;
        }
    }
}
