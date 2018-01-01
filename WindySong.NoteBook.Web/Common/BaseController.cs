using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindySong.NoteBook.App;
using WindySong.NoteBook.App.Entity;

namespace WindySong.NoteBook.Web.Common
{
    /// <summary>
    /// 本项目控制器基类
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseController()
        {           
        }
        /// <summary>
        /// 变量model错误信息
        /// </summary>
        public void IfModelState()
        {
            string error = "";
            //遍历model错误信息
            foreach (var item in ModelState.Keys)
            {
                //strItem属性名也就是表单名 
                string strItem = item;
                foreach (var p in ModelState[item].Errors)
                {
                    //错误信息
                    error += p.ErrorMessage;
                }

            }
            ViewData["formError"] = error;
        }

        /// <summary>
        /// 获取网站配置信息
        /// </summary>
        public void GetSysConfig()
        {
            //获取网站配置信息
            AppService app = new AppService();
            SysConfig sysConfig = app.GetSysConfig();
            ViewData["siteName"] = sysConfig.siteName;
            ViewData["siteKeyWords"] = sysConfig.siteKeyWords;
            ViewData["siteDescription"] = sysConfig.siteDescription;
        }
    }
}
