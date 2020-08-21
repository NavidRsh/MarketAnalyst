using MarketAnalyst.InfraStructure.Model.General;
using Microsoft.EntityFrameworkCore;

namespace MarketAnalyst.InfraStructure.Model
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
            modelBuilder.Entity<Stock>()
                    .Property(s => s.Name)
                    .HasMaxLength(300);

            modelBuilder.Entity<Stock>()
                .HasOne(e => e.StockGroup)
                .WithMany()
                .HasForeignKey(e => e.StockGroupId);

            modelBuilder.Entity<StocksDailyPrice>()
                .HasOne(e => e.Stock)
                .WithMany()
                .HasForeignKey(e => e.StockId); 
        }
    }
}
