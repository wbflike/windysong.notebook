using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WindySong.NoteBook.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //安装mvc服务
            services.AddMvc();
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

            //启用默认静态文件中间件 默认wwwroot底下的静态文件
            app.UseStaticFiles();

            //添加自定义路由
            app.UseMvc(routes =>
            {
                //defatul路由名称 控制器名称Home  方法名称 Index
                routes.MapRoute("defatul", "{controller=Home}/{action=Index}");
            });

            

        }
    }
}
