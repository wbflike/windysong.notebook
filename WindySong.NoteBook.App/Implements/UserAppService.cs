using Chloe;
using Common.EncAndDec;
using System;
using System.Collections.Generic;
using System.Text;
using WindySong.NoteBook.App.Entity;
using WindySong.NoteBook.App.Interfaces;
using WindySong.NoteBook.App.ViewModels.Admin;

namespace WindySong.NoteBook.App.Implements
{
    public class UserAppService : AppService, IUserAppService
    {
        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="model">UpdatePasswordModel</param>
        /// <returns>(1 更新成功)(0 更新失败)(-1 错误用户不存在)(-2 旧密码错误)</returns>
        public int UpdatePassword(string userName, UpdatePasswordModel model)
        {
            IQuery<Users> q = this.DbContext.Query<Users>();
            //密码加密
            string strOldMd5 = MD5Enc.MD5Encrypt(model.oldPassword);
            string strOldSHA1 = SHA1Enc.SHA1Encrypt(strOldMd5);
            Users _users = q.Where(a => a.userName == userName).FirstOrDefault();
            if (_users == null)
            {
                return -1;
            }
            if(strOldSHA1 != _users.passWord)
            {
                return -2;
            }
            else
            {
                //密码加密
                string strNewMd5 = MD5Enc.MD5Encrypt(model.newPassword);
                string strNewSHA1 = SHA1Enc.SHA1Encrypt(strNewMd5);
                _users.passWord = strNewSHA1;
                try
                {
                    this.DbContext.Update(_users);
                    return 1;
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
        }
    }
}
