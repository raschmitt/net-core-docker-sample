using System;
using Domain.Interfaces.Repositories;
using Infra.Data;
using Infra.Data.Repositories;
using Infra.Data.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Registers
{
    public static class InfraRegister
    {
        public static void AddInfra(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<Context>(x => UseSqlServerDatabase(x, configuration));
            
            serviceCollection.AddScoped<IItemRepository, ItemRepository>();
        }
        
        public static void ConfigureInfra(this IApplicationBuilder applicationBuilder)
        {
            var serviceScope = applicationBuilder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<Context>();
            
            context.Database.Migrate();
            context.Seed();
        }

        private static void UseSqlServerDatabase(DbContextOptionsBuilder options, IConfiguration configuration) =>
            options.UseSqlServer(GetConnectionDefaultString(configuration), EnableRetryOnFailure);
        
        private static string GetConnectionDefaultString(IConfiguration configuration) => 
            configuration.GetConnectionString("Connection");
        
        private static void EnableRetryOnFailure(SqlServerDbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
    }
}