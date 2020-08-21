using MarketAnalyst.Core.Data;
using System;
using System.Collections.Generic;
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

        public bool DeleteAll()
        {
            _context.Stocks.RemoveRange(_context.Stocks);
            return true; 
        }

    }
}
