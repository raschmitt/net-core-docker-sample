using Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data
{
    public class Context : DbContext
    {
        public Context() { }
        
        public Context(DbContextOptions<Context> options) : base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ItemMapping());
        }
    }
}