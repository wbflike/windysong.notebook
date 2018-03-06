using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Json
{
    public class JsonPagApi
    {
        /// <summary>
        /// 数据总行数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 行数据
        /// </summary>
        public List<JsonApi> rows { get; set; }
    }
    public class JsonApi
    {
        public int id { get; set; }
        public string name { get; set; }
        public string parameter { get; set; }
        public int tabId { get; set; }
        public string tabName { get; set; }
        public int blockId { get; set; }
        public string blockName { get; set; }
        public int listId { get; set; }
        public string listName { get; set; }
        public int rank { get; set; }
        public string lastTime { get; set; }
    }
}
