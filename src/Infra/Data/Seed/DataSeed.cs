using AutoMapper;
using Domain.Mappers;

namespace Infra.Data.Seed
{
    public static class DataSeed
    {
        public static void Seed(this Context dbContext)
        {
            if (IsSqlite(dbContext.Database.ProviderName))
                return;
            
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ItemMapper>();
            })); 
            
            dbContext.AddItems(mapper);
            dbContext.SaveChanges();
        }
        
        private static bool IsSqlite(string providerName) => 
            providerName == "Microsoft.EntityFrameworkCore.Sqlite";
    }
}