using CollectionManager.API.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionManager.API.Data.Context
{
    public class DesigntimeDbContextFactory : IDesignTimeDbContextFactory<CollectionManagerContext>
    {
        public CollectionManagerContext CreateDbContext(string[] args)
        {
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
