using MarketAnalyst.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketAnalyst.InfraStructure.Model.General
{
    public class Stock
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public StockGroup StockGroup { get; set; }
        public int StockGroupId { get; set; }
        public SupervisionLevelEnum SupervisionLevel { get; set; }
        public int StocksCount { get; set; }
        public int BaseVolume { get; set; }
        public double FloatingStock { get; set; }
        public int AverageMonthVolume { get; set; }

        public string Description { get; set; }
    }
}
