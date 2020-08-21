using System;
using System.Collections.Generic;
using System.Text;

namespace MarketAnalyst.InfraStructure.Model.General
{
    public class StocksDailyPrice
    {
        public int Id { get; set; }

        public Stock Stock { get; set; }
        public int StockId { get; set; }
        public DateTime Date { get; set; }
        public double InitialPrice { get; set; }
        public double LastPrice { get; set; }
        public double FinalPrice { get; set; }
        public double LowestPrice { get; set; }
        public double HighestPrice { get; set; }
        public int DealsCount { get; set; }
        public int DealsVolume { get; set; }

    }
}
