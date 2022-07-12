using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using log4net;
using System.IO;
using System.Reflection;
using log4net.Config;
using Utilerias.Infraestructure;
using Utilerias.Application;
using Utilerias.WebApi.Extensions;

namespace Utilerias
{
    public class Startup
    {
        public static void ConfigureLogForNet()
        {
            string route = "log4net.config";
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            var file = new FileInfo(route);
            XmlConfigurator.Configure(logRepository, file);
        }
        public Startup(IConfiguration configuration)
        {
            ConfigureLogForNet();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddPersistence();
            services.AddApplication();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Utilerias.WebApi", Version = "v1" });
            });


            // Configuración de versionamiento de API
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            // Configuración, manejo de ModelState
            services.AddCustomBehavior();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Utilerias.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
