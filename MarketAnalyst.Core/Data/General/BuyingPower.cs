using MarketAnalyst.Core.Data.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketAnalyst.Core.Data.General
{
    public class BuyingPower
    {
        public long Id { get; set; }
        public Stock Stock { get; set; }
        public int StockId { get; set; }
        public DateTime Date { get; set; }
        public bool IsHourly { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        /// <summary>
        /// اعداد نمایش داده شده بر روی تابلو در لحظه دریافت داده
        /// </summary>
        public int TotalBuyPersonCount { get; set; }
        public int TotalBuyLegalCount { get; set; }
        public int TotalBuyPersonVolume { get; set; }
        public int TotalBuyLegalVolume { get; set; }
        public int TotalSellPersonCount { get; set; }
        public int TotalSellLegalCount { get; set; }
        public int TotalSellPersonVolume { get; set; }
        public int TotalSellLegalVolume { get; set; }
        public double TotalPersonBuyingPower { get; set; }
        public double TotalLegalBuyingPower { get; set; }
        /// <summary>
        /// اعداد تفاضلی به دست آمده از مقایسه با اعداد قبلی 
        /// </summary>
        public int BuyPersonCount { get; set; }
        public int BuyLegalCount { get; set; }
        public int BuyPersonVolume { get; set; } 
        public int BuyLegalVolume { get; set; }
        public int SellPersonCount { get; set; }
        public int SellLegalCount { get; set; }
        public int SellPersonVolume { get; set; }
        public int SellLegalVolume { get; set; }
        public double LastPrice { get; set; }
        public double FinalPrice { get; set; }

        public double PersonBuyingPower { get; set; }
        public double LegalBuyingPower { get; set; }
        public DateTime RegisterDateTime { get; set; }

        
    }
}
namespace MarketAnalyst.Core.Data.Configuration
{
    public class BuyingPowerMap : IEntityTypeConfiguration<BuyingPower>
    {
        public void Configure(EntityTypeBuilder<BuyingPower> builder)
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

