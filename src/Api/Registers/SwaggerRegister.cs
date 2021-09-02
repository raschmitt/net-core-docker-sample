using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api.Registers
{
    public static class SwaggerRegister
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.AssumeDefaultVersionWhenUnspecified = true;
                p.ReportApiVersions = true;
            });
            
            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            using var sp = services.BuildServiceProvider();
            var apiVersionProvider = sp.GetService<IApiVersionDescriptionProvider>();
            
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                
                foreach (var description in apiVersionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                        description.GroupName,
                        new OpenApiInfo
                        {
                            Title = ".Net Core - Docker Sample",
                            Version = description.GroupName,
                            Description = "Sample .Net Core API, with SQL Server Database, containerized with Docker and ready to play with.",
                            Contact = new OpenApiContact
                            {
                                Name = "Rogerio Schmitt",
                                Url = new Uri("https://github.com/raschmitt/net-core-docker-sample")
                            }
                        });
                }
                
                options.IncludeXmlComments(xmlPath);
            });
        }
        
        public static void ConfigureSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider apiVersionProvider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in apiVersionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName);
                }
            });


        }
    }
}