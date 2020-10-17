using MarketAnalyst.Core.Data.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MarketAnalyst.Core.Data.General
{
    public class StocksDailyPrice
    {
        public int Id { get; set; }

        public Stock Stock { get; set; }
        public int StockId { get; set; }
        public DateTime Date { get; set; }
        public double InitialPrice { get; set; }
        public double LastPrice { get; set; }
        /// <summary>
        /// میزان تغییر آخرین قیمت 
        /// </summary>
        public double LastPriceChange { get; set; }
        /// <summary>
        /// درصد تغییر آخرین قیمت
        /// </summary>
        public double LastPriceChangePercent { get; set; }
        public double FinalPrice { get; set; }
        public double FinalPriceChange { get; set; }
        public double FinalPriceChangePercent { get; set; }
        public double LowestPrice { get; set; }
        public double HighestPrice { get; set; }
        public int DealsCount { get; set; }
        public long DealsVolume { get; set; }
        /// <summary>
        /// ارزش معاملات
        /// </summary>
        public decimal DealsValue { get; set; }
        /// <summary>
        /// قیمت دیروز
        /// </summary>
        public double PreviousDayPrice { get; set; }
    }
}
namespace MarketAnalyst.Core.Data.Configuration
{
    public class StockDailyPriceMap : IEntityTypeConfiguration<StocksDailyPrice>
    {
        public void Configure(EntityTypeBuilder<StocksDailyPrice> builder)
        {
            builder.Property(s => s.Date)
                   .HasColumnType("date");

            builder.HasOne(e => e.Stock)
                .WithMany()
                .HasForeignKey(e => e.StockId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
