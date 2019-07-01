using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Formatters;
using CityInfo.API.Services;
using Microsoft.Extensions.Configuration;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API
{
    public class Startup
    {
        public static IConfiguration _config { get; private set; }
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc()
                .AddMvcOptions(o =>
                {
                    o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                })
                .AddJsonOptions(o =>
                {
                    //// o.SerializerSettings.ContractResolver =new CamelCasePropertyNamesContractResolver();
                    //if (o.SerializerSettings.ContractResolver != null)
                    //{
                    //    var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
                    //    castedResolver.NamingStrategy = null;
                    //}
                });
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
                services.AddTransient<IMailService, CloudMailService>();

#endif
            var connectionString = _config["connectionStrings:cityInfoDBConnectionString"];
            services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionString));
            services.AddScoped<ICityInfoRepository, CityInfoRepository>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CityInfoContext cityInfoContext)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = async context =>
                    {
                        context.Response.ContentType = "text/plain";
                        await context.Response.WriteAsync("Welcome to the error page.");
                    }
                });
            }
            cityInfoContext.EnsureSeedDataForContext();
            app.UseStatusCodePages();
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>().ReverseMap();
                cfg.CreateMap<Entities.City, Models.CityDto>().ReverseMap();
                cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>().ReverseMap();
                cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestForCreationDto>().ReverseMap();
                cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDto>().ReverseMap();
            });

            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
