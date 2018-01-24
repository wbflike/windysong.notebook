using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Common;
using System.IO;
using WindySong.NoteBook.App.Interfaces;
using WindySong.NoteBook.App.Implements;
using Microsoft.AspNetCore.Authentication.Cookies;
using WindySong.NoteBook.Web.Common;
using WindySong.NoteBook.App;
using WindySong.NoteBook.App.Entity;

namespace WindySong.NoteBook.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //初始化Globals的配置文件读取
            Globals.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //安装mvc服务
            services.AddMvc();

            //作用域（Scoped）生命周期服务在每次请求被创建一次
            //注入接口和实现类
            services.AddScoped<ILoginAppService, LoginAppService>();
            services.AddScoped<ISysConfigAppService, SysConfigAppService>();

            //安装Cookies服务
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;//方案名称
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,//cookie名称
                o =>
                {
                    o.Cookie.HttpOnly = true;//禁止客户端JS读取cookie
                    o.LoginPath = new PathString("/Login/Index");//登录页面
                    o.Events = new MyCookieEvents();//注册自己的cookie事件
                });

            //获取网站配置信息
            this.GetSysConfig();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //遇到异常如果是处于开发模式进入错误信息页面
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else//否则定向到自定义错误页
            {
                app.UseExceptionHandler("/Error");
            }
            //添加 app.UseAuthentication()，使用授权中间件：
            app.UseAuthentication();

            //启用默认静态文件中间件 默认wwwroot底下的静态文件
            app.UseStaticFiles();

            //添加自定义路由
            app.UseMvc(routes =>
            {
                //defatul路由名称 控制器名称Home  方法名称 Index
                routes.MapRoute("defatul", "{controller=Home}/{action=Index}");
            });

        }

        /// <summary>
        /// 获取网站配置信息
        /// </summary>
        public void GetSysConfig()
        {
            //获取网站配置信息
            AppService app = new AppService();
            SysConfig sysConfig = app.GetSysConfig();
            WebSiteSysConfig.siteName = sysConfig.siteName;
            WebSiteSysConfig.siteKeyWords = sysConfig.siteKeyWords;
            WebSiteSysConfig.siteDescription = sysConfig.siteDescription;
        }

    }
}
