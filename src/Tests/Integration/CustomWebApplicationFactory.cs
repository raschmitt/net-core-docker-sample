using System.Linq;
using Infra.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Integration
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    {
        private ServiceProvider _serviceProvider;
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<Context>));

                services.Remove(descriptor);

                var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
                var connection = new SqliteConnection(connectionStringBuilder.ToString());

                services.AddDbContext<Context>(options =>
                {
                    options.UseSqlite(connection);
                });

                _serviceProvider = services.BuildServiceProvider();

                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<Context>();

                    if (db.Database.GetDbConnection().State == System.Data.ConnectionState.Closed)
                    {
                        db.Database.OpenConnection();
                    }
                    
                    db.Database.Migrate();
                }
            });
        }

        public Context GetContext() 
        {
            return _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<Context>();
        }

        public void SeedData(params object[] data)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Context>();

                db.AddRange(data);
                db.SaveChanges();
            }
        }
    }
}