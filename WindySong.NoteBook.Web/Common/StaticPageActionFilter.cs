using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindySong.NoteBook.App.ViewModels.Json;

namespace WindySong.NoteBook.Web.Common
{
    /// <summary>
    /// ActionFilter过滤器 用于生成静态文件
    /// </summary>
    public class StaticPageActionFilter : Attribute,IActionFilter
    {
        /// <summary>
        /// OnActionExecuted在Action之后执行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            string jsonStr = "";
            StringBuilder builder = new StringBuilder();
            //获取结果
            IActionResult actionResult = context.Result;
            try
            {
                //判断结果是否是一个ViewResult
                if (actionResult is ViewResult)
                {
                    ViewResult viewResult = actionResult as ViewResult;
                    //下面的代码就是执行这个ViewResult，并把结果的html内容放到一个StringBuiler对象中
                    var services = context.HttpContext.RequestServices;
                    var executor = services.GetRequiredService<ViewResultExecutor>();
                    var option = services.GetRequiredService<IOptions<MvcViewOptions>>();
                    var result = executor.FindView(context, viewResult);
                    result.EnsureSuccessful(originalLocations: null);
                    var view = result.View;

                    using (var writer = new StringWriter(builder))
                    {
                        var viewContext = new ViewContext(
                            context,
                            view,
                            viewResult.ViewData,
                            viewResult.TempData,
                            writer,
                            option.Value.HtmlHelperOptions);

                        view.RenderAsync(viewContext).GetAwaiter().GetResult();
                        //这句一定要调用，否则内容就会是空的
                        writer.Flush();
                    }

                    //写入文件
                    string filePath = Directory.GetCurrentDirectory() + @"/wwwroot/" + "index.html";
                    using (FileStream fs = File.Open(filePath, FileMode.Create))
                    {
                        using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                        {
                            sw.Write(builder.ToString());
                        }
                    }
                    jsonStr = JsonConvert.SerializeObject(new JsonResultsString() { state = 1, title = "生成成功", text = "success", type = "success" });
                }
            }
            catch (Exception ex)
            {
                jsonStr = JsonConvert.SerializeObject(new JsonResultsString() { state = 1, title = "生成失败", text = "error", type = "error" });
            }

            //输出当前的结果
            ContentResult contentresult = new ContentResult();
            contentresult.Content = jsonStr;
            //contentresult.Content = builder.ToString();
            contentresult.ContentType = "text/html";
            context.Result = contentresult;

            //throw new NotImplementedException();
        }

        /// <summary>
        /// OnActionExecuting将在Action之前执行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
