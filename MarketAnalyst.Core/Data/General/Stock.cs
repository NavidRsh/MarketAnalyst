using MarketAnalyst.Core.Data.General;
using MarketAnalyst.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketAnalyst.Core.Data.General
{
    public class Stock
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string EnglishSign { get; set; }
        public string EnglishName { get; set; }
        public string PersianSign { get; set; }
        public string PersianName { get; set; }
        public StockGroup StockGroup { get; set; }
        public int StockGroupId { get; set; }
        public SupervisionLevelEnum SupervisionLevel { get; set; }
        public int StocksCount { get; set; }
        public int BaseVolume { get; set; }
        public double FloatingStock { get; set; }
        public int AverageMonthVolume { get; set; }
        public string InfoUrl { get; set; }
        public string Description { get; set; }
    }
}
namespace MarketAnalyst.Core.Data.Configuration
{
    public class StockMap : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.Property(s => s.PersianName)
                   .HasMaxLength(300);
            builder.Property(s => s.EnglishName)
                   .HasMaxLength(300);
            builder.Property(s => s.EnglishSign)
                   .HasMaxLength(300);
            builder.Property(s => s.PersianSign)
                   .HasMaxLength(300);
            builder.HasOne(e => e.StockGroup)
                .WithMany()
                .HasForeignKey(e => e.StockGroupId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
