using FirstCoreApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace FirstCoreApi
{
    public class Startup
    {
        public static IConfiguration Config { get; private set; }

        public Startup(IConfiguration config)
        {
            Config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //services.AddMvc().
            //    AddMvcOptions(o => o.OutputFormatters.Add(
            //        new XmlDataContractSerializerOutputFormatter()));

            // turn of camel case json serialize strategy
            //services.AddMvc()
            //    .AddJsonOptions(o => {
            //        if(o.SerializerSettings.ContractResolver != null)
            //        {
            //            var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //            castedResolver.NamingStrategy = null;
            //        }
            //    });

            //services.AddSingleton<LocalMailService>(); //add concreate implementation
            //services.AddSingleton<IMailService, LocalMailService>();

#if DEBUG
            services.AddSingleton<IMailService, LocalMailService>();
#else
            services.AddSingleton<IMailService, CloudMailService>();
#endif

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My First Core API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            ILoggerFactory logger)
        {
            logger.AddConsole();
            logger.AddDebug();
            logger.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My First Core API");
            });


            app.UseStatusCodePages();

            app.UseMvc();

            app.Run(async (context) => { await context.Response.WriteAsync("No handler found"); });

            app.Run((context) =>
            {
                throw new System.Exception("Not handled by any middleware.");
            });


            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
