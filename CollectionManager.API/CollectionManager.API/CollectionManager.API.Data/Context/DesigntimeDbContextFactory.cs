using CollectionManager.API.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CollectionManager.API.Data.Context
{
    public class DesigntimeDbContextFactory : IDesignTimeDbContextFactory<CollectionManagerContext>
    {
        public CollectionManagerContext CreateDbContext(string[] args)
        {

            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory()) 
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<CollectionManagerContext>();
            optionsBuilder.UseSqlServer("",
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
            return new CollectionManagerContext(optionsBuilder.Options);
        }
    }
}
