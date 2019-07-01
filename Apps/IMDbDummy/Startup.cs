using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IMDBDummy.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace IMDBDummy
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            _config = config;
            _env = env;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IMDBContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("IMDBConnectionString"));
                //cfg.EnableSensitiveDataLogging();
            });
            services.AddMvc(opt =>
            {
                if (_env.IsProduction() && _config["EnableSSL"] == "true")
                {
                    opt.Filters.Add(new RequireHttpsAttribute());
                }
            })
            .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }
            );
            services.AddAutoMapper();

            services.AddScoped<IIMDBRepository, IMDBRepository>();

            services.AddTransient<IMDBSeeder>();
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

                app.UseExceptionHandler("/error");
            }
            // app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default", "{controller}/{action}/{id?}", new { controller = "App", Action = "Index" });
            });
        //    app.UseMvc(cfg =>
        //    {
        //        cfg.MapRoute("Default", "{controller}/{action}/{id?}", new { controller = "App", Action = "Index" });
        ////        cfg.MapRoute(
        ////name: "default",
        ////template: "{controller=App}/{action=Index}/{id?}");

        ////        // when the user types in a link handled by client side routing to the address bar 
        ////        // or refreshes the page, that triggers the server routing. The server should pass 
        ////        // that onto the client, so Angular can handle the route
        ////        cfg.MapRoute(
        ////            name: "spa-fallback",
        ////            template: "{*url}",
        ////            defaults: new { controller = "App", action = "Index" }
        ////        );
        //    });


            if (env.IsDevelopment())
            {
                //Seed the database with scope creation
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<IMDBSeeder>();
                    seeder.Seed().Wait();
                }
            }
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
