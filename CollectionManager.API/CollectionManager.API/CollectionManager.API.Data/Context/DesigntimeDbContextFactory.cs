using CollectionManager.API.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CollectionManager.API.Data.Context
{
    public class DesigntimeDbContextFactory : IDesignTimeDbContextFactory<CollectionManagerContext>
    {
        public CollectionManagerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CollectionManagerContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=CollectionManager;Trusted_Connection=True;TrustServerCertificate=True;",
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
