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
        JsonPagTab GetPageTab(TabPostModel model);
    }
}
