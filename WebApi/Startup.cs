using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "CodacyChallengeApi",
                    Version = "v1",
                    Description = "This WebApi is the second part of the Codacy challenge"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
                uiSetupAction.SwaggerEndpoint($"/docs/api/v1/swagger.json", "v1");
                uiSetupAction.DocumentTitle = "CodacyChallenge WebAPI";
                uiSetupAction.RoutePrefix = "docs/api";
            });

            //CORS issues using swagger UI, otherwise works fine even with HttpsRedirect
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
