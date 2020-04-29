using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Me.StudentManagement.Common;
using Me.StudentManagement.Entity.Models;
using Me.StudentManagement.Logic.Common;
using Me.StudentManagement.Logic.Controller;
using Me.StudentManagement.Logic.InterfaceAll;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Me.StudentManagement.Web
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvc().AddRazorOptions(opt =>
                opt.AreaViewLocationFormats.Add("/Views/Shared/PartialPage/{0}.cshtml" +
                                                RazorViewEngine.ViewExtension));
            services.AddDbContext<MsSqlContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("BloggingDatabase"));
                });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                option.LoginPath = new PathString("/Account/Login/");
                option.Cookie.Name = "studentManagement";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                option.SlidingExpiration = true;
                option.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            services.AddScoped<ILoggerHelper,LoggerHelper>();

            services.AddScoped<MsSqlContext>();
            services.AddTransient<Encryption>();
            services.AddTransient<ICreateInterface, CreateToDB>();
            services.AddTransient<IQueryDBData, QueryDBData>();
            services.AddTransient<IUpdateToDB, UpdataToDB>();
            services.AddTransient<IDeleteFormDB, DeleteFromDB>();
            services.AddTransient<UseSqlServerOptionBuilder>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            MsSqlContext msSqlContext = new MsSqlContext(new UseSqlServerOptionBuilder().Option);
            msSqlContext.Database.EnsureCreated();

            

            logFactory.AddNLog();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });

            //app.Run(run => run.Response.WriteAsync(env.EnvironmentName + Configuration.GetConnectionString("BloggingDatabase")));
        }
    }
}