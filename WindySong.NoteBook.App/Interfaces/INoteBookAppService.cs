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
        JsonPagTab GetPageTab(DataPageModel model);

        /// <summary>
        /// 添加TAB
        /// </summary>
        /// <param name="model">TabAddModel</param>
        /// <returns></returns>
        bool AddTab(TabModel model);

        /// <summary>
        /// 更新TAB
        /// </summary>
        /// <param name="model">UpdateAddModel</param>
        /// <returns></returns>
        bool UpdateTab(TabModel model);

        /// <summary>
        /// 删除TAB
        /// </summary>
        /// <param name="model">TabDeleteModel</param>
        /// <returns></returns>
        int DeleteTab(TabDeleteModel model);

        /// <summary>
        /// 获取Tab Select
        /// </summary>
        /// <returns></returns>
        JsonTabSelect GetTabSelect();
    }
}
