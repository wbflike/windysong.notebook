using System;
using System.Collections.Generic;
using System.Text;
using WindySong.NoteBook.App.ViewModels.Admin;
using WindySong.NoteBook.App.ViewModels.Json;

namespace WindySong.NoteBook.App.Interfaces
{
    /// <summary>
    /// 笔记操作接口
    /// </summary>
    public interface INoteBookAppService
    {
        /// <summary>
        /// 获取TAB分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JsonPagTab GetPageTab(TabPostModel model);

        /// <summary>
        /// 添加TABLE
        /// </summary>
        /// <param name="model">TabAddModel</param>
        /// <returns></returns>
        bool AddTab(TabAddModel model);
    }
}
