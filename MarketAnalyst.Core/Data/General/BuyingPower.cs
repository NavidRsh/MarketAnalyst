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
        public long TotalBuyPersonVolume { get; set; }
        public long TotalBuyLegalVolume { get; set; }
        public int TotalSellPersonCount { get; set; }
        public int TotalSellLegalCount { get; set; }
        public long TotalSellPersonVolume { get; set; }
        public long TotalSellLegalVolume { get; set; }
        public double TotalPersonBuyingPower { get; set; }
        public double TotalLegalBuyingPower { get; set; }
        /// <summary>
        /// متوسط خرید حقیقی در طول روز
        /// </summary>
        public double TotalAveragePersonBuy { get; set; }
        /// <summary>
        /// متوسط خرید حقوقی در طول روز 
        /// </summary>
        public double TotalAverageLegalBuy { get; set; }
        /// <summary>
        /// اعداد تفاضلی به دست آمده از مقایسه با اعداد قبلی 
        /// </summary>
        public int BuyPersonCount { get; set; }
        public int BuyLegalCount { get; set; }
        public long BuyPersonVolume { get; set; } 
        public long BuyLegalVolume { get; set; }
        public int SellPersonCount { get; set; }
        public int SellLegalCount { get; set; }
        public long SellPersonVolume { get; set; }
        public long SellLegalVolume { get; set; }
        public double LastPrice { get; set; }
        public double FinalPrice { get; set; }
        public double PreviousDayPrice { get; set; }
        public double LastPriceChangePercent { get; set; }
        public double FinalPriceChangePercent { get; set; }
        public double FirstPrice { get; set; }
        public double HighestPrice { get; set; }
        public double LowestPrice { get; set; }
        public double PersonBuyingPower { get; set; }
        public double LegalBuyingPower { get; set; }
        /// <summary>
        /// متوسط خرید حقیقی در بازه زمانی رکورد
        /// </summary>
        public double AveragePersonBuy { get; set; }
        /// <summary>
        /// متوسط خرید حقوقی در بازه زمانی رکورد
        /// </summary>
        public double AverageLegalBuy { get; set; }
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

