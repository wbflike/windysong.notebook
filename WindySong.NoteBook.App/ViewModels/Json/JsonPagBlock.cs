using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Json
{
    public class JsonPagBlock
    {
        /// <summary>
        /// 数据总行数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 行数据
        /// </summary>
        public List<JsonBlock> rows { get; set; }
    }

    public class JsonBlock
    {
        public int id { get; set; }
        public int colId { get; set; }
        public string colName { get; set; }
        public string name { get; set; }
        public int rank { get; set; }
        public string lastTime { get; set; }
    }
}
