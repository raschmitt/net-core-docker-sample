namespace Infra.Data.Seed
{
    public static class DataSeed
    {
        public static void Seed(this Context dbContext)
        {
            if (IsSqlite(dbContext.Database.ProviderName))
                return;
            
            dbContext.AddItems();
            
            dbContext.SaveChanges();
        }
        
        private static bool IsSqlite(string providerName) => 
            providerName == "Microsoft.EntityFrameworkCore.Sqlite";
    }
}