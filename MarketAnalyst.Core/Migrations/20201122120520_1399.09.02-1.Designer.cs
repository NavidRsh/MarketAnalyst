﻿// <auto-generated />
using System;
using MarketAnalyst.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MarketAnalyst.Core.Migrations
{
    [DbContext(typeof(MarketContext))]
    [Migration("20201122120520_1399.09.02-1")]
    partial class _139909021
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MarketAnalyst.Core.Data.General.BuyingPower", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AverageLegalBuy");

                    b.Property<double>("AveragePersonBuy");

                    b.Property<int>("BuyLegalCount");

                    b.Property<long>("BuyLegalVolume");

                    b.Property<int>("BuyPersonCount");

                    b.Property<long>("BuyPersonVolume");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<TimeSpan>("EndTime");

                    b.Property<double>("FinalPrice");

                    b.Property<double>("FinalPriceChangePercent");

                    b.Property<double>("FirstPrice");

                    b.Property<double>("HighestPrice");

                    b.Property<bool>("IsHourly");

                    b.Property<double>("LastPrice");

                    b.Property<double>("LastPriceChangePercent");

                    b.Property<double>("LegalBuyingPower");

                    b.Property<double>("LowestPrice");

                    b.Property<double>("PersonBuyingPower");

                    b.Property<double>("PreviousDayPrice");

                    b.Property<DateTime>("RegisterDateTime");

                    b.Property<int>("SellLegalCount");

                    b.Property<long>("SellLegalVolume");

                    b.Property<int>("SellPersonCount");

                    b.Property<long>("SellPersonVolume");

                    b.Property<TimeSpan>("StartTime");

                    b.Property<int>("StockId");

                    b.Property<double>("TotalAverageLegalBuy");

                    b.Property<double>("TotalAveragePersonBuy");

                    b.Property<int>("TotalBuyLegalCount");

                    b.Property<long>("TotalBuyLegalVolume");

                    b.Property<int>("TotalBuyPersonCount");

                    b.Property<long>("TotalBuyPersonVolume");

                    b.Property<double>("TotalLegalBuyingPower");

                    b.Property<double>("TotalPersonBuyingPower");

                    b.Property<int>("TotalSellLegalCount");

                    b.Property<long>("TotalSellLegalVolume");

                    b.Property<int>("TotalSellPersonCount");

                    b.Property<long>("TotalSellPersonVolume");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.ToTable("BuyingPowers");
                });

            modelBuilder.Entity("MarketAnalyst.Core.Data.General.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AverageMonthVolume");

                    b.Property<int>("BaseVolume");

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.Property<double>("EPS");

                    b.Property<string>("EnglishName")
                        .HasMaxLength(300);

                    b.Property<string>("EnglishSign")
                        .HasMaxLength(300);

                    b.Property<double>("FloatingStock");

                    b.Property<string>("InfoUrl");

                    b.Property<int>("MarketType");

                    b.Property<double>("PE");

                    b.Property<string>("PersianName")
                        .HasMaxLength(300);

                    b.Property<string>("PersianSign")
                        .HasMaxLength(300);

                    b.Property<int>("StockGroupId");

                    b.Property<int>("StocksCount");

                    b.Property<int>("SupervisionLevel");

                    b.Property<string>("UniqueCode");

                    b.HasKey("Id");

                    b.HasIndex("StockGroupId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("MarketAnalyst.Core.Data.General.StockGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(300);

                    b.Property<double>("EPS");

                    b.Property<string>("InfoUrl");

                    b.Property<string>("Name")
                        .HasMaxLength(300);

                    b.Property<double>("PE");

                    b.HasKey("Id");

                    b.ToTable("StockGroups");
                });

            modelBuilder.Entity("MarketAnalyst.Core.Data.General.StocksDailyPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("DealsCount");

                    b.Property<decimal>("DealsValue");

                    b.Property<long>("DealsVolume");

                    b.Property<double>("FinalPrice");

                    b.Property<double>("FinalPriceChange");

                    b.Property<double>("FinalPriceChangePercent");

                    b.Property<double>("HighestPrice");

                    b.Property<double>("InitialPrice");

                    b.Property<double>("LastPrice");

                    b.Property<double>("LastPriceChange");

                    b.Property<double>("LastPriceChangePercent");

                    b.Property<double>("LowestPrice");

                    b.Property<double>("PreviousDayPrice");

                    b.Property<int>("StockId");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.ToTable("StocksDailyPrices");
                });

            modelBuilder.Entity("MarketAnalyst.Core.Data.General.BuyingPower", b =>
                {
                    b.HasOne("MarketAnalyst.Core.Data.General.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MarketAnalyst.Core.Data.General.Stock", b =>
                {
                    b.HasOne("MarketAnalyst.Core.Data.General.StockGroup", "StockGroup")
                        .WithMany()
                        .HasForeignKey("StockGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MarketAnalyst.Core.Data.General.StocksDailyPrice", b =>
                {
                    b.HasOne("MarketAnalyst.Core.Data.General.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
