using MarketAnalyst.Core.Data.General;
using Microsoft.EntityFrameworkCore;

namespace MarketAnalyst.Core.Data
{
    public class MarketContext : DbContext
    {
        string connection = "Data Source=localhost;Initial Catalog=Alborz;User ID=sa;Password=sa_12345;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //string connection = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Alborz;User ID=sa;Password=sa_12345;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public MarketContext(DbContextOptions<MarketContext> options)
        : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.

            //    //var connection = ""; 
            //    optionsBuilder.UseSqlServer(connection); 
            //}
           
            optionsBuilder.UseSqlServer("Server='localhost';Database=MarketAnalyst;User Id = sa; Password=sa_12345");
            
        }

        public DbSet<StockGroup> StockGroups { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StocksDailyPrice> StocksDailyPrices { get; set; }
        public DbSet<BuyingPower> BuyingPowers { get; set; }
        public DbSet<Test> Tests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Property Configurations
            modelBuilder.ApplyConfiguration(new Core.Data.Configuration.StockMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Configuration.StockGroupMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Configuration.StockDailyPriceMap());
            modelBuilder.ApplyConfiguration(new Core.Data.Configuration.BuyingPowerMap());
        }
    }
}
