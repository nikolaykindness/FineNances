using System.Data.Entity;

namespace FineNances.Model.Data
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public ApplicationContext() : base("FineNancesDB")
        {
            
        }
    }
}
