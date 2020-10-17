using MarketAnalyst.Core.Data;
using MarketAnalyst.Core.Data.General;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAnalyst.Core.DataService.General
{
    public class StocksDailyPriceService
    {
        private readonly MarketContext _context;
        public StocksDailyPriceService(MarketContext context)
        {
            _context = context; 
        }

        public async Task AddRange(List<Data.General.StocksDailyPrice> priceList)
        {
            await _context.StocksDailyPrices.AddRangeAsync(priceList);            
        }
        public async Task<StocksDailyPrice> Get(int stockId, DateTime date)
        {
            return await _context.StocksDailyPrices
                .Where(a => a.StockId == stockId && a.Date == date)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> Any(int stockId, DateTime date)
        {
            return await _context.StocksDailyPrices
                .AnyAsync(a => a.StockId == stockId && a.Date == date);
                
        }

        public async Task<List<DateTime>> GetDates(int stockId)
        {
            return await _context.StocksDailyPrices
                .Where(a => a.StockId == stockId)
                .Select(a => a.Date).ToListAsync(); 
        }

    }
}
