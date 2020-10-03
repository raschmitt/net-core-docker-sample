using System;
using Infra.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integration
{
    public abstract class IntegrationContext : IDisposable
    {
        protected readonly DbContextOptions<Context> _dbContextOptions;
        protected readonly Context _context;
        protected readonly Context _actContext;
        protected readonly Context _assertContext;

        protected IntegrationContext()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());

            _dbContextOptions = new DbContextOptionsBuilder<Context>()
                .UseSqlite(connection)
                .Options;
            
            _context = new Context(_dbContextOptions);
            
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            
            _actContext = new Context(_dbContextOptions);
            _assertContext = new Context(_dbContextOptions);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}