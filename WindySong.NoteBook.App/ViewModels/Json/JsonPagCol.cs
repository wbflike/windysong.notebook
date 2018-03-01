using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Json
{
    public class JsonPagCol
    {
        /// <summary>
        /// 数据总行数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 行数据
        /// </summary>
        public List<JsonCol> rows { get; set; }
    }

    public class JsonCol
    {
        public int id { get; set; }
        public int tabId { get; set; }
        public string tabName { get; set; }
        public string name { get; set; }
        public int rank { get; set; }
        public string lastTime { get; set; }
    }
}
