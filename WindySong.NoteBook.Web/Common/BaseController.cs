using Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WindySong.NoteBook.App;
using WindySong.NoteBook.App.Entity;
using WindySong.NoteBook.App.ViewModels.Json;

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
        /// 遍历model错误信息
        /// </summary>
        public void IfModelStateViewData()
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
        /// 遍历model错误信息
        /// </summary>
        public string IfModelStateString()
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
            //清空br标签
            return error;
        }

        public byte[] GetValidCodeByte( string code)
        {
            string path = Directory.GetCurrentDirectory() + @"/Resources/Font/WRYH.ttf";
            return VerificationCode.GetValidCodeByte(code, path, 75, 25);
        }

        public JsonResultsString GetSwalJson(int state,string title,string text,string type)
        {
            return new JsonResultsString() { state = state, title = title, text = text, type = type };
        }
    }
}
