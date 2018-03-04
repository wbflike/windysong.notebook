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
            List<JsonTab> listJson = new List<JsonTab>();
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
                listJson.Add(jsonTab);
            }
            json.rows = listJson;

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
                try
                {
                    var max = qCTab.Max(a => a.rank);
                    tab.rank = max + 1;
                }
                catch (Exception ex)
                {
                    tab.rank = 1;
                }
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
        public int DeleteTab(DeleteModel model)
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
        public JsonSelect GetTabSelect()
        {
            var json = new JsonSelect();
            //返回json的tab数据
            List<JsonSelectValue> listJsonValue = new List<JsonSelectValue>();
            //数据库查询的tab数据
            List<CTab> list = new List<CTab>();
            IQuery<CTab> q = this.DbContext.Query<CTab>();
            list = q.Where(a => 1 == 1).OrderBy(a => a.rank).ToList();
            foreach (var cTab in list)
            {
                var jsonTab = new JsonSelectValue();
                jsonTab.id = cTab.id;
                jsonTab.name = cTab.name;
                listJsonValue.Add(jsonTab);
            }
            json.options = listJsonValue;

            return json;
        }

        /// <summary>
        /// 添加TAB
        /// </summary>
        /// <param name="model">TabAddModel</param>
        /// <returns></returns>
        public bool AddCol(ColModel model)
        {
            UCol col = new UCol();
            col.name = model.Name;
            col.cTabId = model.CTabId;
            if (model.Rank == 0 || model.Rank == null)
            {
                IQuery<UCol> q = this.DbContext.Query<UCol>();
                try
                {
                    var max = q.Max(a => a.rank);
                    col.rank = max + 1;
                }
                catch (Exception ex)
                {
                    col.rank = 1;
                }
            }
            else
            {
                col.rank = model.Rank.Value;
            }
            col.lastTime = DateTime.Now.ToString();
            try
            {
                col = this.DbContext.Insert(col);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 返回Col分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonPagCol GetPageCol(DataPageModel model)
        {
            var json = new JsonPagCol();
            //返回json的数据
            List<JsonCol> listJson = new List<JsonCol>();

            IQuery<UCol> q = this.DbContext.Query<UCol>();
            if (string.IsNullOrEmpty(model.searchKey))
            {
                //获取数据行数
                json.total = q.Count();
                //TakePage 第一个参数是page,第二个是pageNumber  offset表示走第几条数据开始取  limit表示取几条
                //list = q.Where(a => 1 == 1).OrderBy(a => a.rank).TakePage((model.offset / model.limit) + 1, model.limit).ToList();

                //多表查询
                //1 建立连接
                var col_tab = q.InnerJoin<CTab>((col, tab) => col.cTabId == tab.id);

                /* 调用 Select 方法返回一个泛型为包含 UCol、CTab 匿名类型的 IQuery 对象。
                 * Select 方法也可以返回自定义类型 。
                */
                var qq = col_tab.Select((col, tab) => new
                {
                    UCol = col,
                    CTab = tab
                });

                /* 根据条件筛选，然后调用 ToList 就会返回一个泛型为 new { UCol = col, CTab = tab } 的 List 集合 */
                var result = qq.Where(a => 1 == 1)
                .Select(a => new { id = a.UCol.id, tabId = a.UCol.cTabId, tabName = a.CTab.name, name = a.UCol.name, rank=a.UCol.rank, lastTime=a.UCol.lastTime })
                .OrderBy(a => a.rank).TakePage((model.offset / model.limit) + 1, model.limit).ToList();

                foreach(var col in result)
                {
                    var jsonCol = new JsonCol();
                    jsonCol.id = col.id;
                    jsonCol.tabId = col.tabId;
                    jsonCol.tabName = col.tabName;
                    jsonCol.name = col.name;
                    jsonCol.rank = col.rank;
                    jsonCol.lastTime = col.lastTime;
                    listJson.Add(jsonCol);
                }
            }
            else
            {
                var col_tab = q.InnerJoin<CTab>((col, tab) => col.cTabId == tab.id);
                var qq = col_tab.Select((col, tab) => new
                {
                    UCol = col,
                    CTab = tab
                });

                //获取数据行数
                json.total = qq.Where(a => a.CTab.name.Contains(model.searchKey) || a.UCol.name.Contains(model.searchKey)).Count();

                //TakePage 第一个参数是page,第二个是pageNumber
                var result = qq.Where(a => a.CTab.name.Contains(model.searchKey) || a.UCol.name.Contains(model.searchKey))
                .Select(a => new { id = a.UCol.id, tabId = a.UCol.cTabId, tabName = a.CTab.name, name = a.UCol.name, rank = a.UCol.rank, lastTime = a.UCol.lastTime })
                .OrderBy(a => a.rank).TakePage((model.offset / model.limit) + 1, model.limit).ToList();

                foreach (var col in result)
                {
                    var jsonCol = new JsonCol();
                    jsonCol.id = col.id;
                    jsonCol.tabId = col.tabId;
                    jsonCol.tabName = col.tabName;
                    jsonCol.name = col.name;
                    jsonCol.rank = col.rank;
                    jsonCol.lastTime = col.lastTime;
                    listJson.Add(jsonCol);
                }
            }

            json.rows = listJson;

            return json;
        }

        /// <summary>
        /// 更新Col
        /// </summary>
        /// <param name="model">ColModel</param>
        /// <returns></returns>
        public bool UpdateCol(ColModel model)
        {
            UCol col = new UCol();
            IQuery<UCol> qCol = this.DbContext.Query<UCol>();
            col = qCol.Where(a => a.id == model.Id).FirstOrDefault();
            col.name = model.Name;
            col.cTabId = model.CTabId;
            if (model.Rank == 0 || model.Rank == null)
            {
                try
                {
                    var max = qCol.Max(a => a.rank);
                    col.rank = max + 1;
                }
                catch (Exception ex)
                {
                    col.rank = 1;
                }
            }
            else
            {
                col.rank = model.Rank.Value;
            }
            

            try
            {
                this.DbContext.Update(col);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除Col
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteCol(DeleteModel model)
        {
            string str = model.deleteid;
            str = str.Substring(0, str.Length - 1);
            string[] arrId = str.Split(',');
            foreach (var id in arrId)
            {
                int i = 0;
                i = this.DbContext.Query<UBlock>().Where(u => u.uColId == int.Parse(id)).Count();
                if (i > 0)//uCol存在记录
                {
                    return -1;
                }
            }
            try
            {
                foreach (var id in arrId)
                {
                    this.DbContext.Delete<UCol>(a => a.id == int.Parse(id));
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取Col Select
        /// </summary>
        /// <returns></returns>
        public JsonSelect GetColSelect()
        {
            var json = new JsonSelect();
            //返回json的tab数据
            List<JsonSelectValue> listJsonValue = new List<JsonSelectValue>();
            //数据库查询的tab数据
            IQuery<UCol> q = this.DbContext.Query<UCol>();
            var col_tab = q.InnerJoin<CTab>((col, tab) => col.cTabId == tab.id);
            var qq = col_tab.Select((col, tab) => new
            {
                UCol = col,
                CTab = tab
            });
            var result = qq.Where(a => 1 == 1)
                .Select(a => new { id = a.UCol.id, tabName = a.CTab.name, name = a.UCol.name, rank = a.UCol.rank })
                .OrderBy(a => a.tabName).ToList();

            foreach (var col in result)
            {
                var jsonValue = new JsonSelectValue();
                jsonValue.id = col.id;
                jsonValue.name = col.tabName + "->" + col.name;
                listJsonValue.Add(jsonValue);
            }
            json.options = listJsonValue;

            return json;
        }

        /// <summary>
        /// 添加Block
        /// </summary>
        /// <param name="model">TabAddModel</param>
        /// <returns></returns>
        public bool AddBlock(BlockModel model)
        {
            UBlock block = new UBlock();
            block.name = model.Name;
            block.uColId = model.UColId;
            if (model.Rank == 0 || model.Rank == null)
            {
                IQuery<UBlock> q = this.DbContext.Query<UBlock>();
                try
                {
                    var max = q.Max(a => a.rank);
                    block.rank = max + 1;
                }
                catch (Exception ex)
                {
                    block.rank = 1;
                }
            }
            else
            {
                block.rank = model.Rank.Value;
            }
            block.lastTime = DateTime.Now.ToString();
            try
            {
                block = this.DbContext.Insert(block);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 返回Block分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonPagBlock GetPageBlock(DataPageModel model)
        {
            var json = new JsonPagBlock();
            //返回json的数据
            List<JsonBlock> listJson = new List<JsonBlock>();

            IQuery<UBlock> q = this.DbContext.Query<UBlock>();
            if (string.IsNullOrEmpty(model.searchKey))
            {
                //获取数据行数
                json.total = q.Count();
                //TakePage 第一个参数是page,第二个是pageNumber  offset表示走第几条数据开始取  limit表示取几条
                //list = q.Where(a => 1 == 1).OrderBy(a => a.rank).TakePage((model.offset / model.limit) + 1, model.limit).ToList();

                //多表查询
                //1 建立连接
                var join = q.InnerJoin<UCol>((block, col) => block.uColId == col.id)
                    .InnerJoin<CTab>((block, col, tab) => col.cTabId == tab.id);

                /* 调用 Select 方法返回一个泛型为包含 UCol、CTab 匿名类型的 IQuery 对象。
                 * Select 方法也可以返回自定义类型 。
                */
                var qq = join.Select((block, col, tab) => new
                {
                    UBlock = block,
                    UCol = col,
                    CTab = tab
                });

                /* 根据条件筛选，然后调用 ToList 就会返回一个泛型为 new { UCol = col, CTab = tab } 的 List 集合 */
                var result = qq.Where(a => 1 == 1)
                .Select(a => new { id = a.UBlock.id,tabName=a.CTab.name,colId=a.UCol.id,colName=a.UCol.name,name=a.UBlock.name,rank=a.UBlock.rank, lastTime=a.UBlock.lastTime })
                .OrderBy(a => a.tabName).TakePage((model.offset / model.limit) + 1, model.limit).ToList();

                foreach (var temp in result)
                {
                    var jsonData = new JsonBlock();
                    jsonData.id = temp.id;
                    jsonData.colId = temp.colId;
                    jsonData.colName = temp.tabName + "->" + temp.colName;
                    jsonData.name = temp.name;
                    jsonData.rank = temp.rank;
                    jsonData.lastTime = temp.lastTime;
                    
                    listJson.Add(jsonData);
                }
            }
            else
            {
                var join = q.InnerJoin<UCol>((block, col) => block.uColId == col.id)
                    .InnerJoin<CTab>((block, col, tab) => col.cTabId == tab.id);
                var qq = join.Select((block, col, tab) => new
                {
                    UBlock = block,
                    UCol = col,
                    CTab = tab
                });

                //获取数据行数
                json.total = qq.Where(a => a.CTab.name.Contains(model.searchKey) || a.UCol.name.Contains(model.searchKey) || a.UBlock.name.Contains(model.searchKey)).Count();

                //TakePage 第一个参数是page,第二个是pageNumber
                var result = qq.Where(a => a.CTab.name.Contains(model.searchKey) || a.UCol.name.Contains(model.searchKey) || a.UBlock.name.Contains(model.searchKey))
                .Select(a => new { id = a.UBlock.id, tabName = a.CTab.name, colId = a.UCol.id, colName = a.UCol.name, name = a.UBlock.name, rank = a.UBlock.rank, lastTime = a.UBlock.lastTime })
                .OrderBy(a => a.tabName).TakePage((model.offset / model.limit) + 1, model.limit).ToList();

                foreach (var temp in result)
                {
                    var jsonData = new JsonBlock();
                    jsonData.id = temp.id;
                    jsonData.colId = temp.colId;
                    jsonData.colName = temp.tabName + "->" + temp.colName;
                    jsonData.name = temp.name;
                    jsonData.rank = temp.rank;
                    jsonData.lastTime = temp.lastTime;

                    listJson.Add(jsonData);
                }
            }

            json.rows = listJson;

            return json;
        }

        /// <summary>
        /// 更新Block
        /// </summary>
        /// <param name="model">ColModel</param>
        /// <returns></returns>
        public bool UpdateBlock(BlockModel model)
        {
            UBlock block = new UBlock();
            IQuery<UBlock> qBlock = this.DbContext.Query<UBlock>();
            block = qBlock.Where(a => a.id == model.Id).FirstOrDefault();
            block.name = model.Name;
            block.uColId = model.UColId;
            if (model.Rank == 0 || model.Rank == null)
            {
                try
                {
                    var max = qBlock.Max(a => a.rank);
                    block.rank = max + 1;
                }
                catch (Exception ex)
                {
                    block.rank = 1;
                }
            }
            else
            {
                block.rank = model.Rank.Value;
            }


            try
            {
                this.DbContext.Update(block);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除Blcok
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteBlock(DeleteModel model)
        {
            string str = model.deleteid;
            str = str.Substring(0, str.Length - 1);
            string[] arrId = str.Split(',');
            foreach (var id in arrId)
            {
                int i = 0;
                i = this.DbContext.Query<UList>().Where(u => u.uBlockId == int.Parse(id)).Count();
                if (i > 0)//uCol存在记录
                {
                    return -1;
                }
            }
            try
            {
                foreach (var id in arrId)
                {
                    this.DbContext.Delete<UBlock>(a => a.id == int.Parse(id));
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取Block Select
        /// </summary>
        /// <returns></returns>
        public JsonSelect GetBlockSelect()
        {
            var json = new JsonSelect();
            //返回json的tab数据
            List<JsonSelectValue> listJsonValue = new List<JsonSelectValue>();
            //数据库查询的block数据
            IQuery<UBlock> q = this.DbContext.Query<UBlock>();
            var join = q.InnerJoin<UCol>((block, col) => block.uColId == col.id)
                .InnerJoin<CTab>((block, col, tab) => col.cTabId == tab.id);
            var qq = join.Select((block, col, tab) => new
            {
                UBlock=block,
                UCol = col,
                CTab = tab
            });
            var result = qq.Where(a => 1 == 1)
                .Select(a => new { id = a.UBlock.id, tabName = a.CTab.name, name = a.UBlock.name, rank = a.UBlock.rank })
                .OrderBy(a => a.tabName).ToList();

            foreach (var a in result)
            {
                var jsonValue = new JsonSelectValue();
                jsonValue.id = a.id;
                jsonValue.name = a.tabName + "->" + a.name;
                listJsonValue.Add(jsonValue);
            }
            json.options = listJsonValue;

            return json;
        }

        /// <summary>
        /// 添加List
        /// </summary>
        /// <param name="model">ListModel</param>
        /// <returns></returns>
        public bool AddList(ListModel model)
        {
            UList list = new UList();
            list.name = model.Name;
            list.uBlockId = model.BlockId;
            if (model.Rank == 0 || model.Rank == null)
            {
                IQuery<UList> q = this.DbContext.Query<UList>();
                try
                {
                    var max = q.Max(a => a.rank);
                    list.rank = max + 1;
                }
                catch (Exception ex)
                {
                    list.rank = 1;
                }
            }
            else
            {
                list.rank = model.Rank.Value;
            }
            list.lastTime = DateTime.Now.ToString();
            try
            {
                list = this.DbContext.Insert(list);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 返回List分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonPagList GetPageList(DataPageModel model)
        {
            var json = new JsonPagList();
            //返回json的数据
            List<JsonList> listJson = new List<JsonList>();

            IQuery<UList> q = this.DbContext.Query<UList>();
            if (string.IsNullOrEmpty(model.searchKey))
            {
                //获取数据行数
                json.total = q.Count();
                //TakePage 第一个参数是page,第二个是pageNumber  offset表示走第几条数据开始取  limit表示取几条
                //list = q.Where(a => 1 == 1).OrderBy(a => a.rank).TakePage((model.offset / model.limit) + 1, model.limit).ToList();

                //多表查询
                //1 建立连接
                var join = q.InnerJoin<UBlock>((list, block) => list.uBlockId == block.id)
                    .InnerJoin<UCol>((list, block, col) => block.uColId == col.id)
                    .InnerJoin<CTab>((list, block, col,tab) => col.cTabId == tab.id);

                /* 调用 Select 方法返回一个泛型为包含 UCol、CTab 匿名类型的 IQuery 对象。
                 * Select 方法也可以返回自定义类型 。
                */
                var qq = join.Select((list, block, col, tab) => new
                {
                    UList = list,
                    UBlock = block,
                    UCol = col,
                    CTab = tab
                });

                /* 根据条件筛选，然后调用 ToList 就会返回一个泛型为 new { UCol = col, CTab = tab } 的 List 集合 */
                var result = qq.Where(a => 1 == 1)
                .Select(a => new { id = a.UList.id, tabName = a.CTab.name, blockId = a.UBlock.id, blockName = a.UBlock.name, name = a.UList.name, rank = a.UList.rank, lastTime = a.UList.lastTime })
                .OrderBy(a => a.tabName).TakePage((model.offset / model.limit) + 1, model.limit).ToList();

                foreach (var temp in result)
                {
                    var jsonData = new JsonList();
                    jsonData.id = temp.id;
                    jsonData.blockId = temp.blockId;
                    jsonData.blockName = temp.tabName + "->" + temp.blockName;
                    jsonData.name = temp.name;
                    jsonData.rank = temp.rank;
                    jsonData.lastTime = temp.lastTime;

                    listJson.Add(jsonData);
                }
            }
            else
            {
                var join = q.InnerJoin<UBlock>((list, block) => list.uBlockId == block.id)
                    .InnerJoin<UCol>((list, block, col) => block.uColId == col.id)
                    .InnerJoin<CTab>((list, block, col, tab) => col.cTabId == tab.id);
                var qq = join.Select((list, block, col, tab) => new
                {
                    UList = list,
                    UBlock = block,
                    UCol = col,
                    CTab = tab
                });

                //获取数据行数
                json.total = qq.Where(a => a.CTab.name.Contains(model.searchKey) || a.UCol.name.Contains(model.searchKey) || a.UBlock.name.Contains(model.searchKey) || a.UList.name.Contains(model.searchKey)).Count();

                //TakePage 第一个参数是page,第二个是pageNumber
                var result = qq.Where(a => a.CTab.name.Contains(model.searchKey) || a.UCol.name.Contains(model.searchKey) || a.UBlock.name.Contains(model.searchKey) || a.UList.name.Contains(model.searchKey))
                .Select(a => new { id = a.UList.id, tabName = a.CTab.name, blockId = a.UBlock.id, blockName = a.UBlock.name, name = a.UList.name, rank = a.UList.rank, lastTime = a.UList.lastTime })
                .OrderBy(a => a.tabName).TakePage((model.offset / model.limit) + 1, model.limit).ToList();

                foreach (var temp in result)
                {
                    var jsonData = new JsonList();
                    jsonData.id = temp.id;
                    jsonData.blockId = temp.blockId;
                    jsonData.blockName = temp.tabName + "->" + temp.blockName;
                    jsonData.name = temp.name;
                    jsonData.rank = temp.rank;
                    jsonData.lastTime = temp.lastTime;

                    listJson.Add(jsonData);
                }
            }

            json.rows = listJson;

            return json;
        }

        /// <summary>
        /// 更新List
        /// </summary>
        /// <param name="model">ColModel</param>
        /// <returns></returns>
        public bool UpdateList(ListModel model)
        {
            UList list = new UList();
            IQuery<UList> qList = this.DbContext.Query<UList>();
            list = qList.Where(a => a.id == model.Id).FirstOrDefault();
            list.name = model.Name;
            list.uBlockId = model.BlockId;
            if (model.Rank == 0 || model.Rank == null)
            {
                try
                {
                    var max = qList.Max(a => a.rank);
                    list.rank = max + 1;
                }
                catch (Exception ex)
                {
                    list.rank = 1;
                }
            }
            else
            {
                list.rank = model.Rank.Value;
            }


            try
            {
                this.DbContext.Update(list);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除List
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteList(DeleteModel model)
        {
            string str = model.deleteid;
            str = str.Substring(0, str.Length - 1);
            string[] arrId = str.Split(',');
            foreach (var id in arrId)
            {
                int i = 0;
                i = this.DbContext.Query<Api>().Where(u => u.uListId == int.Parse(id)).Count();
                if (i > 0)//list存在记录
                {
                    return -1;
                }
            }
            try
            {
                foreach (var id in arrId)
                {
                    this.DbContext.Delete<UList>(a => a.id == int.Parse(id));
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
