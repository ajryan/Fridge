using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Fridge.Models;

namespace Fridge.Data
{
    public class FridgeContext : DbContext
    {
        public FridgeContext() : base("FridgeContext")
        {
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<StockItem> StockItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}