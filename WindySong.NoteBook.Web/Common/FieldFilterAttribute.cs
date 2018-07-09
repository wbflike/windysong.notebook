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
            int i = 0;
            string str = "";
        }
    }
}
