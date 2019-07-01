using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.Entities;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Library.API.Helpers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json.Serialization;
using AspNetCoreRateLimit;

namespace Library.API
{
    public class Startup
    {
        public static IConfiguration Configuration;
        private ILogger<Startup> _logger { get; set; }

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                opt.ReturnHttpNotAcceptable = true;
                opt.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                // opt.InputFormatters.Add(new XmlDataContractSerializerInputFormatter(opt));

                var xmlDataContractSerializerInputFormatter = new XmlDataContractSerializerInputFormatter(opt);

                xmlDataContractSerializerInputFormatter.SupportedMediaTypes.Add("application/vnd.marvin.authorwithdateofdeath.full+xml");
                xmlDataContractSerializerInputFormatter.SupportedMediaTypes.Add("application/vnd.marvin.author.full+xml");

                opt.InputFormatters.Add(xmlDataContractSerializerInputFormatter);

                var jsonInputFormatter = opt.InputFormatters.OfType<JsonInputFormatter>().FirstOrDefault();
                if (jsonInputFormatter != null)
                {
                    jsonInputFormatter.SupportedMediaTypes.Add("application/vnd.marvin.authorwithdateofdeath.full+json");

                    jsonInputFormatter.SupportedMediaTypes.Add("application/vnd.marvin.author.full+json");

                }




                var jsonOutputFormatter = opt.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();
                if (jsonOutputFormatter != null)
                {

                    jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.marvin.authorwithdateofdeath+json");
                    jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.marvin.hateoas+json");
                }

            })
            .AddXmlDataContractSerializerFormatters()
            .AddJsonOptions(option =>
            {
                option.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowOrigin", options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                });
            });


            var connectionString = Configuration["connectionStrings:libraryDBConnectionString"];
            services.AddDbContext<LibraryContext>(o => o.UseSqlServer(connectionString));

            // register the repository
            services.AddScoped<ILibraryRepository, LibraryRepository>();

            //generating prev and next routes 
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper, UrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
            services.AddTransient<ITypeHelperService, TypeHelperService>();

            //Caching & concurrency

            services.AddHttpCacheHeaders(expirationOptions =>
            {
                expirationOptions.MaxAge = 600;
            }, validationOptions =>
            {
                validationOptions.MustRevalidate = true;
            });

            //Caching & concurrency
            services.AddResponseCaching();



            // Rate Limiting
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules = new List<RateLimitRule>()
                {
                    new RateLimitRule() {
                        Endpoint = "*",
                        Limit =1000,
                        Period ="5m"
                    },
                    new RateLimitRule() {
                        Endpoint = "*",
                        Limit =200,
                        Period ="10s"
                    }
                };
            });
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, LibraryContext libraryContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                //app.UseExceptionHandler(new ExceptionHandlerOptions
                //{
                //    ExceptionHandler = async context =>
                //    {
                //        context.Response.ContentType = "text/plain";
                //        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                //    }
                //});
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        //getting exception details 
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            _logger.LogError(500, exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);
                        }
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/plain";
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");

                    });
                });
            }

            Mapper.Initialize(cfg =>
            {

                cfg.CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                 $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge(src.DateOfDeath)));
                cfg.CreateMap<Book, BookDto>().ReverseMap();
                cfg.CreateMap<AuthorForCreationDto, Author>();
                cfg.CreateMap<BookForCreationDto, Book>();
                cfg.CreateMap<BookForUpdateDto, Book>().ReverseMap();
                cfg.CreateMap<AuthorForCreationWithDateOfDeathDto, Author>().ReverseMap();
            });

            libraryContext.EnsureSeedDataForContext();
            app.UseCors("AllowOrigin");

            app.UseIpRateLimiting();

            app.UseResponseCaching();
            app.UseHttpCacheHeaders();
            app.UseMvc();
            app.UseStatusCodePages();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
