using MarketAnalyst.Core.Data.General;
using Microsoft.EntityFrameworkCore;

namespace MarketAnalyst.Core.Data
{
    public class MarketContext : DbContext
    {
        string connection = "Data Source=localhost;Initial Catalog=Alborz;User ID=sa;Password=sa_12345;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public MarketContext(DbContextOptions<MarketContext> options)
        : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.

                //var connection = ""; 
                optionsBuilder.UseSqlServer(connection); 
            }

            //CreateFunctions();
        }

        public DbSet<StockGroup> StockGroups { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StocksDailyPrice> StocksDailyPrices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Property Configurations
            modelBuilder.ApplyConfiguration(new Core.Data.Configuration.StockMap());
        }
    }
}
