using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Json
{
    /// <summary>
    /// Json返回客户端弹出窗口提示
    /// </summary>
    public class JsonResultsString
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int state { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string type { get; set; }
    }
}
