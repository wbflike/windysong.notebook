using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WindySong.NoteBook.Web.Common
{
    public class FieldFilterAttribute : Attribute,IActionFilter
    {

        //public  void OnActionExecuted(ActionExecutingContext filterContext)
        //{
        //    //var ps = filterContext.ActionDescriptor.GetParameters();
        //    //获取Action参数
        //    var ps = filterContext.ActionDescriptor.Parameters;
        //    //遍历Action参数
        //    foreach (var p in ps)
        //    {
        //        //当参数类型为string
        //        //if (p.ParameterType.Equals(typeof(string)))
        //        //{
        //        //    filterContext.ActionDescriptor.Parameters[p.Name] = "xxx";

        //        //}
        //        //else if (p.ParameterType.IsClass)
        //        //{
        //        //    ModelFieldFilter(p.Name, p.ParameterType, filterContext.ActionParameters[p.ParameterName]);
        //        }
        //    }


        //    base.OnActionExecuted(filterContext);
        //}

        //private object ModelFieldFilter(string key, Type t, object obj)
        //{
        //    var ats = t.GetCustomAttributes(typeof(FieldFilterAttribute), false);

        //    if (true) //(ats.Length > 0)
        //    {
        //        if (obj != null)
        //        {
        //            var pps = t.GetProperties();

        //            foreach (var pp in pps)
        //            {
        //                if (pp.PropertyType.Equals(typeof(string)))
        //                {
        //                    string value = pp.GetValue(obj).ToString();
        //                    pp.SetValue(obj, value + value);
        //                }
        //                else if (pp.PropertyType.IsClass)
        //                {
        //                    pp.SetValue(obj, ModelFieldFilter(pp.Name, pp.PropertyType, pp.GetValue(obj)));
        //                }
        //            }
        //        }
        //    }

        //    return obj;
        //}

        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    throw new NotImplementedException();
        //}

        //在Action方法之回之后调用
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        //在调用Action方法之前调用
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //获取Action参数集合
            var ps = context.ActionDescriptor.Parameters;
            //遍历参数集合
            foreach (var p in ps)
            {
                //当参数等于字符串
                if (p.ParameterType.Equals(typeof(string)))
                {
                    if (context.ActionArguments[p.Name] != null)
                    {
                        context.ActionArguments[p.Name] = context.ActionArguments[p.Name] + "测试";
                    }                        
                }
                else if (p.ParameterType.IsClass)//当参数等于类
                {
                    ModelFieldFilter(p.Name, p.ParameterType, context.ActionArguments[p.Name]);
                }

            }
        }

        /// <summary>
        /// 遍历修改类的字符串属性
        /// </summary>
        /// <param name="key">类名</param>
        /// <param name="t">数据类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        private object ModelFieldFilter(string key, Type t, object obj)
        {
            //获取类的属性集合
            var ats = t.GetCustomAttributes(typeof(FieldFilterAttribute), false);


            if (obj != null)
            {
                //获取类的属性集合
                var pps = t.GetProperties();

                foreach (var pp in pps)
                {
                    if(pp.GetValue(obj) != null)
                    {
                        //当属性等于字符串
                        if (pp.PropertyType.Equals(typeof(string)))
                        {
                            string value = pp.GetValue(obj).ToString();
                            pp.SetValue(obj, value + "测试");
                        }
                        else if (pp.PropertyType.IsClass)//当属性等于类进行递归
                        {
                            pp.SetValue(obj, ModelFieldFilter(pp.Name, pp.PropertyType, pp.GetValue(obj)));
                        }
                    }
                    
                }
            }

            return obj;
        }
    }
}
