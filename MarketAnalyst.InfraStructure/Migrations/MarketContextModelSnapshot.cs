﻿// <auto-generated />
using System;
using MarketAnalyst.InfraStructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MarketAnalyst.InfraStructure.Migrations
{
    [DbContext(typeof(MarketContext))]
    partial class MarketContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MarketAnalyst.InfraStructure.Model.General.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AverageMonthVolume");

                    b.Property<int>("BaseVolume");

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.Property<double>("FloatingStock");

                    b.Property<string>("Name")
                        .HasMaxLength(300);

                    b.Property<int>("StockGroupId");

                    b.Property<int>("StocksCount");

                    b.Property<int>("SupervisionLevel");

                    b.HasKey("Id");

                    b.HasIndex("StockGroupId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("MarketAnalyst.InfraStructure.Model.General.StockGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("StockGroups");
                });

            modelBuilder.Entity("MarketAnalyst.InfraStructure.Model.General.StocksDailyPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("DealsCount");

                    b.Property<int>("DealsVolume");

                    b.Property<double>("FinalPrice");

                    b.Property<double>("HighestPrice");

                    b.Property<double>("InitialPrice");

                    b.Property<double>("LastPrice");

                    b.Property<double>("LowestPrice");

                    b.Property<int>("StockId");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.ToTable("StocksDailyPrices");
                });

            modelBuilder.Entity("MarketAnalyst.InfraStructure.Model.General.Stock", b =>
                {
                    b.HasOne("MarketAnalyst.InfraStructure.Model.General.StockGroup", "StockGroup")
                        .WithMany()
                        .HasForeignKey("StockGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MarketAnalyst.InfraStructure.Model.General.StocksDailyPrice", b =>
                {
                    b.HasOne("MarketAnalyst.InfraStructure.Model.General.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
