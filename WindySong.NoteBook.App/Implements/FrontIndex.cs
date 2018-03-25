using Chloe;
using System;
using System.Collections.Generic;
using System.Text;
using WindySong.NoteBook.App.Entity;
using WindySong.NoteBook.App.Interfaces;
using WindySong.NoteBook.App.ViewModels.Front;

namespace WindySong.NoteBook.App.Implements
{
    public class FrontIndex : AppService, IFrontIndex
    {
        public IndexModel GetIndex()
        {
            IQuery<CTab> qTab = this.DbContext.Query<CTab>();
            IQuery<UCol> qCol = this.DbContext.Query<UCol>();
            IQuery<UBlock> qBlock = this.DbContext.Query<UBlock>();
            IQuery<UList> qList = this.DbContext.Query<UList>();
            IQuery<Api> qApi = this.DbContext.Query<Api>();

            IndexModel indexModel = new IndexModel();
            List<Tab> tab = new List<Tab>();
            
            var listTabOne = qTab.Where(a => 1 == 1).OrderBy(a => a.rank).ToList();
            foreach(var a in listTabOne)
            {
                Tab model = new Tab();
                model.TabName = a.name;
                tab.Add(model);
            }
            List<ListTab> ListTab = new List<ListTab>();
            var listTabTwo = qTab.Where(a => 1 == 1).OrderBy(a => a.rank).ToList();
            foreach (var a in listTabTwo)
            {
                ListTab modelTab = new ListTab();
                modelTab.TabName = a.name;
                List<ListCol> listCol = new List<ListCol>();
                var cList = qCol.Where(c => c.cTabId == a.id).OrderBy(c => c.rank).ToList();
                foreach(var c in cList)
                {
                    ListCol modelCol = new ListCol();
                    modelCol.ColName = c.name;
                    List<ListBlock> listBolck = new List<ListBlock>();
                    var bList = qBlock.Where(b => b.uColId == c.id).OrderBy(b => b.rank).ToList();
                    foreach(var b in bList)
                    {
                        ListBlock modelBlock = new ListBlock();
                        modelBlock.BlockName = b.name;
                        List<ListList> listList = new List<ListList>();
                        var lList = qList.Where(l => l.uBlockId == b.id).OrderBy(l => l.rank).ToList();
                        foreach(var l in lList)
                        {
                            ListList modelList = new ListList();
                            modelList.ListName = l.name;
                            List<ApiList> apiList = new List<ApiList>();
                            var aList = qApi.Where(ap => ap.uListId == l.id).OrderBy(ap => ap.rank).ToList();
                            foreach(var ap in aList)
                            {
                                ApiList api = new ApiList();
                                api.ApiName = ap.name;
                                api.ApiParameter = ap.parameter;
                                api.ApiDescription = ap.description;
                                apiList.Add(api);
                            }
                            modelList.ApiList = apiList;
                            listList.Add(modelList);
                        }
                        modelBlock.ListList = listList;
                        listBolck.Add(modelBlock);
                    }
                    modelCol.ListBlock = listBolck;
                    listCol.Add(modelCol);
                }
                modelTab.ListCol = listCol;
                ListTab.Add(modelTab);
            }
            indexModel.Tab = tab;
            indexModel.ListTab = ListTab;
            return indexModel;
        }
    }
}
