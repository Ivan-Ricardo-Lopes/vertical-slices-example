using IRL.VerticalSlices.APP.Common.Interfaces;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.Shared.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace IRL.VerticalSlices.APP.Common.Database.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in this.ChangeTracker
            .Entries<IObjectWithState>())
            {
                IObjectWithState stateInfo = entry.Entity;
                entry.State = ConvertState(stateInfo.State);
            }

            return (await base.SaveChangesAsync(true, cancellationToken));
        }

        private EntityState ConvertState(State state)
        {
            switch (state)
            {
                case State.Added:
                    return EntityState.Added;

                case State.Modified:
                    return EntityState.Modified;

                case State.Deleted:
                    return EntityState.Deleted;

                default:
                    return EntityState.Unchanged;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region FinanceAccounts

            modelBuilder.Entity<FinanceAccountDbModel>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<FinanceAccountDbModel>()
                .HasIndex(x => x.AccountCode)
                .IsUnique();

            modelBuilder.Entity<FinanceAccountDbModel>()
                .HasIndex(x => x.CustomerCode);

            modelBuilder.Entity<FinanceAccountDbModel>()
                .HasMany(x => x.FinanceTransactions)
                .WithOne()
                .HasForeignKey(x => x.AccountCode)
                .HasPrincipalKey(x => x.AccountCode);

            modelBuilder.Entity<FinanceAccountDbModel>()
              .Property(x => x.AccountCode)
              .IsRequired();

            modelBuilder.Entity<FinanceAccountDbModel>()
              .Property(x => x.CustomerCode)
              .IsRequired();
            modelBuilder.Entity<FinanceAccountDbModel>()
                .Ignore(x => x.State);

            modelBuilder.Entity<FinanceTransactionDbModel>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<FinanceTransactionDbModel>()
                .Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<FinanceTransactionDbModel>()
                .HasIndex(x => x.AccountCode);

            modelBuilder.Entity<FinanceTransactionDbModel>()
              .Property(x => x.AccountCode)
              .IsRequired();

            modelBuilder.Entity<FinanceTransactionDbModel>()
               .Property(x => x.Type)
               .IsRequired();

            modelBuilder.Entity<FinanceTransactionDbModel>()
              .Property(x => x.Description)
              .IsRequired();

            modelBuilder.Entity<FinanceTransactionDbModel>()
              .Property(x => x.CreatedDate)
              .IsRequired();

            modelBuilder.Entity<FinanceTransactionDbModel>()
                .Ignore(x => x.State);

            #endregion FinanceAccounts
        }

        public DbSet<FinanceAccountDbModel> FinanceAccounts { get; set; }

        public DbSet<FinanceTransactionDbModel> FinanceTransactions { get; set; }
    }
}