using Microsoft.EntityFrameworkCore;


namespace CollectionManager.API.Domain.Context
{
    public class CollectionManagerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Card> Cards { get; set; }

        public CollectionManagerContext(DbContextOptions<CollectionManagerContext> options) : base(options)
        {
            
        }

        public CollectionManagerContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<Account>(u => u.Account)
                .WithOne(a => a.User)
                .HasForeignKey<Account>(a => a.UserId);

            modelBuilder.Entity<Account>()
                .HasMany<Collection>(a => a.Collections)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Collection>()
                .HasMany<Card>(collection => collection.Cards)
                .WithOne(card => card.Collection)
                .HasForeignKey(card => card.CollectionId)
                .OnDelete(DeleteBehavior.Restrict);


        }

    }
}
