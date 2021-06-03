using System;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Mappers;
using Domain.Services;
using Infra.Data;
using Infra.Data.Repositories;
using Infra.Data.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
            services.AddSwaggerGen();
            services.AddHealthChecks();
            
            services.AddDbContext<Context>(UseSqlServerDatabase);

            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IItemRepository, ItemRepository>();
            
            services.AddAutoMapper(typeof(ItemMapper));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<Context>();
            
            context.Database.Migrate();
            context.Seed();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ".Net Core Docker Sample - v1");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            { 
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
        
        private string GetConnectionDefaultString() => Configuration.GetConnectionString("Connection");

        private static void EnableRetryOnFailure(SqlServerDbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);

        private void UseSqlServerDatabase(DbContextOptionsBuilder options) =>
            options.UseSqlServer(GetConnectionDefaultString(), EnableRetryOnFailure);
    }
}
