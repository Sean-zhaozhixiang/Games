using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Tutorial.Web.Core;
using Tutorial.Web.Data;
using Tutorial.Web.ICore;
using Tutorial.Web.Models;

namespace Tutorial.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
           // var conntr = _configuration["ConnectionStrings:DefaultConnection"];
            var conntr = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options=>
            {
                options.UseSqlServer(conntr);

            });
            //注入服务 AddScoped是每次Http请求时的服务注册(容器)
            services.AddScoped<IResbiatiy<Students>, Resbiatiy>();
            //每次Http请求生成实力，保证在一个逻辑线程内
            services.AddScoped<IResbiatiy<Students>, EfCoreResbiatiy>();
            //配置IdentityDbContext 的数据存储
            services.AddDbContext<IdentityDbContext>(option=> 
            {
                option.UseSqlServer(conntr,
                     b => b.MigrationsAssembly("Tutorial.Web"));
            });
            //注册identity的服务
            services.AddDefaultIdentity<IdentityUser>().
                AddEntityFrameworkStores<IdentityDbContext>();
            //改变密码加密
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app
            , IHostingEnvironment env
            ,IConfiguration configuration)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            //app.UseFileServer();//包含下面2个
            //app.UseDefaultFiles(); //只会改变请求路径 改成 index.html
             app.UseStaticFiles();//静态文件
            app.UseStaticFiles(new StaticFileOptions {

                RequestPath = "/node_modules",
                FileProvider=new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules"))//根目录下
            });
            //app.UseMvcWithDefaultRoute();
            app.UseAuthentication();
            app.UseMvc(builder => 
            {
                builder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");//按约定来配置路由
            });

            //app.Use(next =>
            //{
            //    return async httpContext => 
            //    {
            //        if (httpContext.Request.Path.StartsWithSegments("/first"))
            //        {
            //            await httpContext.Response.WriteAsync("first");
            //        }
            //        else
            //        {
            //            await next(httpContext);
            //        }
            //    };
            //});
            //app.UseWelcomePage(new WelcomePageOptions {

            //    Path="/welcome"
            //});
           
        }
    }
}
