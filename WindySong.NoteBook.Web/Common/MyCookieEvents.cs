using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WindySong.NoteBook.App;
using WindySong.NoteBook.App.Interfaces;

namespace WindySong.NoteBook.Web.Common
{
    public class MyCookieEvents : CookieAuthenticationEvents
    {
        private ICacheService _cache;
        public MyCookieEvents(ICacheService cache)
        {
            this._cache = cache;
        }
        //重写ValidatePrincipal方法，每次获取cookie的时候判断cookie用户是否存在，如果不存在则删除cookie
        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            //获取cooke信息
            var sid = context.Principal.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Sid).Value;
            //当存在cookies
            if(sid != null)
            {
                //当存在的cookies用户名在缓存不存在则删除cookies
                if (!_cache.Exists("userid:"+sid))
                {
                    context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
            }
            //if (name != null)
            //{
            //    AppService app = new AppService();
            //    bool bl = false;
            //    bl = app.IfUser(name);
            //    //当存在的cookies用户名在数据库不存在则删除cookies
            //    if (bl == false)
            //    {
            //        context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //    }
            //}
            return base.ValidatePrincipal(context);
        }
    }
}
