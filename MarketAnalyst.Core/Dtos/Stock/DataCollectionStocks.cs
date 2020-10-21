using System;
using System.Collections.Generic;
using System.Text;

namespace MarketAnalyst.Core.Dtos.DataCollection
{
    public class StockGeneralDto
    {
        public int Id { get; set; }
        public string PersianName { get; set; }
        public string Code { get; set; }
        public string UniqueCode { get; set; }
        public Enums.SupervisionLevelEnum SupervisionLevel { get; set; }
    }
}
