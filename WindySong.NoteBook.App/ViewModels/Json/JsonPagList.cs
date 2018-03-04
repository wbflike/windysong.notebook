using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Json
{
    public class JsonPagList
    {
        /// <summary>
        /// 数据总行数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 行数据
        /// </summary>
        public List<JsonList> rows { get; set; }
    }
    public class JsonList
    {
        public int id { get; set; }
        public int blockId { get; set; }
        public string blockName { get; set; }
        public string name { get; set; }
        public int rank { get; set; }
        public string lastTime { get; set; }
    }
}
