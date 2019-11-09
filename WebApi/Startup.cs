using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommitViewer;
using CommitViewer.Utils;
using Domain.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Git.Setup();
            services.AddMvc();
            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "CommitViewer Web API",
                    Version = "v1",
                    Description = "This WebApi represents the second part of the Codacy challenge"
                });
            });

            services.AddSingleton<ICommitService, CommitService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //basic health check
            app.UseHealthChecks("/health");

            app.UseSwagger(setupAction =>
            {
                setupAction.RouteTemplate = "docs/api/{documentName}/swagger.json";
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwaggerUI(uiSetupAction =>
            {
                uiSetupAction.SwaggerEndpoint("/docs/api/v1/swagger.json", "v1");
                uiSetupAction.DocumentTitle = "CommitViewer Web API";
                uiSetupAction.RoutePrefix = "commitviewer/api";
            });

            //CORS issues using swagger UI, otherwise works fine even with HttpsRedirect
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
