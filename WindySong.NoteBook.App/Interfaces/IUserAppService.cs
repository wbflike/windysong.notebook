using System;
using System.Collections.Generic;
using System.Text;
using WindySong.NoteBook.App.ViewModels.Admin;

namespace WindySong.NoteBook.App.Interfaces
{
    /// <summary>
    /// 用户操作接口
    /// </summary>
    public interface IUserAppService
    {
        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="model">UpdatePasswordModel</param>
        /// <returns>(1 更新成功)(0 更新失败)(-1 错误用户不存在)(-2 旧密码错误)</returns>
        int UpdatePassword(string userName, UpdatePasswordModel model);
    }
}
