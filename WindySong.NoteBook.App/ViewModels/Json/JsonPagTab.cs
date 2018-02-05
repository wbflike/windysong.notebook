using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Json
{
    /// <summary>
    /// Tab客户端分页
    /// </summary>
    public class JsonPagTab
    {
        /// <summary>
        /// 数据总行数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 行数据
        /// </summary>
        public List<JsonTab> rows { get; set; }
    }
}
