using System;
using System.Collections.Generic;
using System.Text;
using WindySong.NoteBook.App.Interfaces;
using WindySong.NoteBook.App;
using WindySong.NoteBook.App.ViewModels.Admin;
using Chloe;
using Common.EncAndDec;
using WindySong.NoteBook.App.Entity;

namespace WindySong.NoteBook.App.Implements
{
    /// <summary>
    /// 用户登录实现类 
    /// </summary>
    public class LoginAppService:AppService, ILoginAppService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginModel">用户数据模型</param>
        /// <returns>true登录成功 false登录失败</returns>
        public LoginModel UserLogin(LoginModel loginModel)
        {
            IQuery<Users> q = this.DbContext.Query<Users>();
            //密码加密
            string strMd5 = MD5Enc.MD5Encrypt(loginModel.password);
            string strSHA1 = SHA1Enc.SHA1Encrypt(strMd5);
            Users _users = q.Where(a => a.userName == loginModel.name && a.passWord == strSHA1).FirstOrDefault();
            if(_users == null)
            {
                return null;
            }
            else
            {
                //更新最后更新时间
                string strDate = DateTime.Now.ToString();
                this.DbContext.Update<Users>(a=>a.id == _users.id,a=>new Users() {lastTime= strDate });
                return new LoginModel() { id=_users.id,name=_users.userName,photo=_users.photo};
            }
            
        }
    }
}
