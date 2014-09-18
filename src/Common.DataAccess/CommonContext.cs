using System.Data.Entity;

namespace Common.DataAccess
{
    public class CommonContext : DbContext
    {
        public CommonContext()
            : base("FilterContext")
        {

            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = false;

            /*
             * NOTE: To debug nuget update-database command, uncomment the next 2 lines
             */

            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();
        }

        /*
         * Lookups
         */

        //public DbSet<AccountType> AccountTypes { get; set; }

        /*
         * Domain
         */

        //public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Account>()
            //    .HasOptional(a => a.Reseller)
            //    .WithMany()
            //    .HasForeignKey(a => a.ResellerId)
            //    .WillCascadeOnDelete();
        }
    }
}