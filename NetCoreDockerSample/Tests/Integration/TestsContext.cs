using System;
using System.Collections.Generic;
using Infra.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integration
{
    public abstract class IntegrationContext : IDisposable
    {
        private readonly Context _context;
        
        protected readonly Context ActContext;
        protected readonly Context AssertContext;

        protected IntegrationContext()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());

            var dbContextOptions = new DbContextOptionsBuilder<Context>()
                .UseSqlite(connection)
                .Options;
            
            _context = new Context(dbContextOptions);
            
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            
            ActContext = new Context(dbContextOptions);
            AssertContext = new Context(dbContextOptions);
        }

        protected void SeedData(IEnumerable<object> data)
        {
            foreach (var element in data)
            {
                _context.Add(element);
            }

            _context.SaveChanges();
        }
        
        public void Dispose()
        {
            _context.Dispose();
            
            ActContext.Dispose();
            AssertContext.Dispose();
        }
    }
}