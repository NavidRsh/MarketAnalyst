using MarketAnalyst.Core.Data;
using MarketAnalyst.Core.Data.General;
using MarketAnalyst.Core.Dtos.DataCollection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAnalyst.Core.DataService.General
{
    public class StockService
    {
        private readonly MarketContext _context;
        public StockService(MarketContext context)
        {
            _context = context; 
        }

        public async Task Add(Data.General.Stock stock)
        {
            await _context.Stocks.AddAsync(stock);            
        }
        public async Task<Stock> FindAsync(string uniqueCode)
        {
            return await _context.Stocks
                .Where(a => a.UniqueCode.Equals(uniqueCode, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefaultAsync();
        }
        public bool DeleteAll(Enums.MarketTypeEnum? marketType)
        {
            var stocks = _context.Stocks.AsQueryable();
            if (marketType != null)
            {
                stocks = stocks.Where(a => a.MarketType == marketType);
            }
            _context.Stocks.RemoveRange(stocks);
            return true; 
        }

        public async Task<List<StockGeneralDto>> GetStocksGeneralInfo(List<Enums.SupervisionLevelEnum> supervisionLevels)
        {
            var query = _context.Stocks
                .Select(a => new StockGeneralDto()
                {
                    Id = a.Id,
                    Code = a.Code,
                    PersianName = a.PersianName,
                    UniqueCode = a.UniqueCode,
                    SupervisionLevel = a.SupervisionLevel
                });
            if (supervisionLevels != null)
            {
                query = query.Where(a => supervisionLevels.Contains(a.SupervisionLevel));
            }
            return await query.ToListAsync(); 
        }

    }
}
