using System;
using System.Collections.Generic;
using System.Text;
using WindySong.NoteBook.App.ViewModels.Front;

namespace WindySong.NoteBook.App.Interfaces
{
    public interface IFrontIndex
    {
        /// <summary>
        /// 网站首页
        /// </summary>
        /// <returns></returns>
        IndexModel GetIndex();
    }
}
