using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Front
{
    public class IndexModel
    {
        public List<Tab> Tab { get; set; }
        public List<ListTab> ListTab { get; set; }
    }

    public class Tab
    {
        public string TabName { get; set; }
    }
    public class ListTab
    {
        public string TabName { get; set; }
        public List<ListCol> ListCol { get; set; }
    }
    public class ListCol
    {
        public string ColName { get; set; }
        public List<ListBlock> ListBlock { get; set; }
    }
    public class ListBlock
    {
        public string BlockName { get; set; }
        public List<ListList> ListList { get; set; }
    }
    public class ListList
    {
        public string ListName { get; set; }
        public List<ApiList> ApiList { get; set; }
    }
    public class ApiList
    {
        public string ApiName { get; set; }
        public string ApiParameter { get; set; }
        public string ApiDescription { get; set; }
    }
}
