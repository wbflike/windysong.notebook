using System;
using System.Collections.Generic;
using System.Text;
using WindySong.NoteBook.App.Entity;
using WindySong.NoteBook.App.ViewModels.Admin;

namespace WindySong.NoteBook.App.Interfaces
{
    /// <summary>
    /// 用户登录接口
    /// </summary>
    public interface ILoginAppService : IAppService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        LoginModel UserLogin(LoginModel loginModel);
    }
}
