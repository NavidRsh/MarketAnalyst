using MarketAnalyst.Core.Data.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketAnalyst.Core.Data.General
{
    public class StockGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string InfoUrl { get; set; }
    }
}
namespace MarketAnalyst.Core.Data.Configuration
{
    public class StockGroupMap : IEntityTypeConfiguration<StockGroup>
    {
        public void Configure(EntityTypeBuilder<StockGroup> builder)
        {
            builder.Property(s => s.Name)
                   .HasMaxLength(300);
            builder.Property(s => s.Code)
                   .HasMaxLength(300);

        }
    }
}

