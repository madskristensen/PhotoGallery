using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using PhotoGallery.Models;
using System;
using WebOptimizer;

namespace PhotoGallery
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc((options) =>
            {
                options.CacheProfiles.Add("default",
                    new CacheProfile
                    {
                        Duration = (int)TimeSpan.FromDays(1).TotalSeconds, // 7 days
                        Location = ResponseCacheLocation.Any
                    });
            });
            services.AddResponseCaching();
            services.AddSingleton<AlbumCollection>();
            services.AddSingleton<ImageProcessor>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });
            services.AddCookieAuthentication(o =>
            {
                o.LoginPath = "/admin/login";
                o.LogoutPath = "/admin/logout";
            });
            services.AddWebOptimizer(assets =>
            {
                assets.EnableCaching = true;
                assets.AddFiles("text/css", "css/site.css", "css/login.css", "css/admin.css")
                      .MinifyCss();

                assets.AddFiles("application/javascript", "js/admin.js", "js/lazyload.js")
                      .MinifyJavaScript(new NUglify.JavaScript.CodeSettings { PreserveImportantComments = false });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseResponseCaching();

            app.UseWebOptimizer();

            app.UseAuthentication();

            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (context) =>
                {
                    var headers = context.Context.Response.GetTypedHeaders();
                    headers.CacheControl = new CacheControlHeaderValue()
                    {
                        MaxAge = TimeSpan.FromDays(365)
                    };
                }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
