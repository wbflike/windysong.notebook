using System;
using System.Collections.Generic;
using System.Text;
using WindySong.NoteBook.App.ViewModels.Admin;

namespace WindySong.NoteBook.App.Interfaces
{
    /// <summary>
    /// 站点信息配置接口
    /// </summary>
    public interface ISysConfigAppService
    {
        /// <summary>
        /// 获取站点配置信息
        /// </summary>
        /// <returns></returns>
        SysConfigModel GetSysConfig();

        /// <summary>
        /// 设置站点配置信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        bool SetSysConfig(SysConfigModel model);
    }
}
