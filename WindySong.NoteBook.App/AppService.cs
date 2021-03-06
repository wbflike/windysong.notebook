﻿using Chloe;
using System;
using System.Collections.Generic;
using System.Text;
using WindySong.NoteBook.App.Entity;
using WindySong.NoteBook.Data;

namespace WindySong.NoteBook.App
{
    /// <summary>
    /// 应用程序顶级父类
    /// </summary>
    public class AppService
    {
        IDbContext _dbContext;
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public IDbContext DbContext
        {
            get
            {
                if (this._dbContext == null)
                    this._dbContext = DbContextFactory.CreateContext();
                return this._dbContext;
            }
            set
            {
                this._dbContext = value;
            }
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public bool IfUser(string userName)
        {

            IQuery<Users> q = this.DbContext.Query<Users>();

            Users users = q.Where(a => a.userName == userName).FirstOrDefault();
            if (users == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <returns>SysConfig</returns>
        public SysConfig GetSiteSysConfig()
        {
            IQuery<SysConfig> q = this.DbContext.Query<SysConfig>();
            SysConfig sysConfig = q.Where(a => 1 == 1).FirstOrDefault();
            return sysConfig;
        }

        /// <summary>
        /// 获取用户头像
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserPhoto(int userId)
        {
            IQuery<Users> q = this.DbContext.Query<Users>();
            Users users = q.Where(a =>a.id == userId).FirstOrDefault();
            return users.photo;
        }

        /// <summary>
        /// 获取usersid集合
        /// </summary>
        /// <returns></returns>
        public List<string> GetUsersId()
        {
            IQuery<Users> q = this.DbContext.Query<Users>();
            List<Users> users = q.Where(a => 1 == 1).ToList();
            List<string> list = new List<string>();
            foreach(var a in users)
            {
                list.Add(a.id.ToString());
            }
            return list;
        }

    }
}
