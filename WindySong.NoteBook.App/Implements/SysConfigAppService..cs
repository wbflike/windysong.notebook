using Chloe;
using System;
using System.Collections.Generic;
using System.Text;
using WindySong.NoteBook.App.Entity;
using WindySong.NoteBook.App.Interfaces;
using WindySong.NoteBook.App.ViewModels.Admin;

namespace WindySong.NoteBook.App.Implements
{
    /// <summary>
    /// 站点配置信息实现类
    /// </summary>
    public class SysConfigAppService : AppService, ISysConfigAppService
    {
        
        /// <summary>
        /// 设置站点配置信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        public bool SetSysConfig(SysConfigModel model)
        {
            SysConfig config = new SysConfig();
            int i = this.DbContext.Update<SysConfig>(a => 1 == 1, a => new SysConfig()
            {
                siteName = model.siteName,
                siteKeyWords = model.siteKeyWords,
                siteDescription = model.siteDescription,
                cdn = model.cdn
            });
            if (i>=1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取站点配置信息
        /// </summary>
        /// <returns></returns>
        public SysConfigModel GetSysConfig()
        {
            SysConfigModel model = new SysConfigModel();
            SysConfig config;
            IQuery<SysConfig> q = this.DbContext.Query<SysConfig>();
            config = q.Where(a => 1 == 1).FirstOrDefault();
            model.siteName = config.siteName;
            model.siteKeyWords = config.siteKeyWords;
            model.siteDescription = config.siteDescription;
            model.cdn = config.cdn;
            return model;
        }
    }
}
