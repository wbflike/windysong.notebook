using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WindySong.NoteBook.Web.Common;
using Microsoft.AspNetCore.Authorization;
using WindySong.NoteBook.App.Interfaces;
using WindySong.NoteBook.App.ViewModels.Admin;
using WindySong.NoteBook.App.ViewModels.Json;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Security.Claims;
using Common;
using Microsoft.Extensions.Caching.Memory;

namespace WindySong.NoteBook.Web.Controllers
{
    //权限验证
    [Authorize]
    public class AdminUserController : ManageController
    {
        //用户操作接口
        private IUserAppService _userApp;
        public AdminUserController(IUserAppService userApp)
        {
            this._userApp = userApp;
        }
        public IActionResult Password()
        {
            return View();
        }
        //Token验证，防止CSRF XSS攻击
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Password(UpdatePasswordModel model)
        {
            var jsonResults = new JsonResultsString();
            //判断数据是否合法
            if (!ModelState.IsValid)
            {
                return Json(this.GetSwalJson(0, "非法数据", this.IfModelStateString(), "error"));
            }
            int i = 0;
            i = _userApp.UpdatePassword(User.Identity.Name, model);
            switch (i)
            {
                case 1:
                    jsonResults = this.GetSwalJson(1, "更新成功", "success", "success");
                    break;
                case 0:
                    jsonResults = this.GetSwalJson(0, "更新失败", "error", "error");
                    break;
                case -1:
                    jsonResults = this.GetSwalJson(-1, "更新失败,用户不存在", "error", "error");
                    break;
                case -2:
                    jsonResults = this.GetSwalJson(-2, "更新失败,旧密码错误", "error", "error");
                    break;
            }
            return Json(jsonResults);
        }

        public IActionResult UserPhoto()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult UserPhoto(IFormFile userPhoto,int dataX,int dataY,int dataWidth,int dataHeight)
        {
            string originalPath = Directory.GetCurrentDirectory() + @"/Upload/UserPhoto/";
            string thumbnailPath = Directory.GetCurrentDirectory() + @"/Upload/UserPhotoThumbnail/";
            string cutPath = Directory.GetCurrentDirectory() + @"/Upload/UserPhotoCut/";
            var jsonResults = new JsonResultsString();
            // 文件大小
            long size = 0;
            // 原文件名（包括路径）
            var filename = ContentDispositionHeaderValue.Parse(userPhoto.ContentDisposition).FileName;
            // 扩展名
            var extName = filename.Substring(filename.LastIndexOf('.')).Replace("\"", "").ToLower();
            //判断后缀是否合法
            if(extName != ".jpg" && extName != ".png" && extName != ".gif")
            {
                return Json(this.GetSwalJson(0, "只能上传图片格式", "error", "error"));
            }
            // 设置文件大小
            size += userPhoto.Length;
            //判断文件大小
            if (size < 1)
            {
                return Json(this.GetSwalJson(0, "图片过小", "error", "error"));
            }

            // 新文件名
            string shortfilename = $"{Guid.NewGuid()}{extName}";
            // 新文件名（包括路径）
            filename = originalPath + shortfilename;
            
            


            Stream stream = userPhoto.OpenReadStream();
            //通过byte正确安全的判断上传文件格式 
            //第一种方法
            byte[] fileByte = new byte[2];//contentLength，这里我们只读取文件长度的前两位用于判断就好了，这样速度比较快，剩下的也用不到。
            stream.Read(fileByte, 0, 2);//contentLength，还是取前两位
            string fileFlag = ""; 
            if (fileByte != null && fileByte.Length > 0)//图片数据是否为空
            {
                fileFlag = fileByte[0].ToString() + fileByte[1].ToString();
            }
            //说明255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar 
            string[] fileTypeStr = { "255216", "7173", "13780" };//对应的图片格式jpg,gif,bmp,png
            if (!fileTypeStr.Contains(fileFlag))
            {
                return Json(this.GetSwalJson(0, "只能上传图片格式", "error", "error"));
            }
            ////第二种方法
            //BinaryReader r = new BinaryReader(stream);
            //string fileclass = "";
            ////这里的位长要具体判断
            //for (int i = 0; i < 2; i++)
            //{
            //    fileclass += r.ReadByte().ToString();
            //}
            //r.Close();
            //string[] fileTypeStr = { "255216", "7173", "13780" };//对应的图片格式jpg,gif,bmp,png
            //if (!fileTypeStr.Contains(fileclass))
            //{
            //    return Json(this.GetSwalJson(-1, "只能上传图片格式", "error", "error"));
            //}


            // 创建新文件
            using (FileStream fs = System.IO.File.Create(filename))
            {
                try
                {
                    // 复制文件
                    userPhoto.CopyTo(fs);
                    // 清空缓冲区数据
                    fs.Flush();
                }
                catch (Exception e)
                {
                    jsonResults = this.GetSwalJson(0, "上传失败，服务器保存错误", "error", "error");
                }

            }
            try
            {
                bool cut = false;
                cut = Images.CutImage(dataX, dataY, dataWidth, dataHeight, filename, cutPath + shortfilename);

                bool thumbnail = false;
                thumbnail = Images.ThumbnailImage(cutPath + shortfilename, thumbnailPath+ shortfilename, 64, 64);
                if(thumbnail)
                {
                    _userApp.SetUserPhoto(int.Parse(User.FindFirstValue(ClaimTypes.Sid)), shortfilename);
                    jsonResults = this.GetSwalJson(1, "上传成功", "success", "success");
                }
                else
                {
                    jsonResults = this.GetSwalJson(0, "上传失败，缩图失败", "error", "error");
                }
                
            }
            catch (Exception e)
            {
                jsonResults = this.GetSwalJson(0, "上传失败，图片处理异常", "error", "error");
            }

            return Json(jsonResults);
        }
    }
}