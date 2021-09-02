using Api.Registers;
using Domain.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwagger();
            services.AddHealthChecks();
            services.AddDomain();
            services.AddInfra(Configuration);
            services.AddAutoMapper(typeof(ItemMapper));
        }

        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.ConfigureInfra();
            app.ConfigureSwagger(provider);
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            { 
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}
