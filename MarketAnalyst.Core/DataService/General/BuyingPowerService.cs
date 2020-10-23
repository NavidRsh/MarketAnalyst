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
    public class BuyingPowerService
    {
        private readonly MarketContext _context;
        public BuyingPowerService(MarketContext context)
        {
            _context = context; 
        }

        public async Task Add(Data.General.BuyingPower BuyingPower)
        {
            await _context.BuyingPowers.AddAsync(BuyingPower);            
        }

        public async Task<BuyingPower> GetBuyingPower(int stockId, DateTime date, TimeSpan endTime)
        {
            return await _context.BuyingPowers
                .Where(a => a.StockId == stockId && a.Date == date.Date && a.EndTime == endTime)
                .FirstOrDefaultAsync(); 
            
        }

        public async Task<BuyingPower> GetLastBuyingPowerOfDay(int stockId, DateTime date, TimeSpan endTime)
        {
            return await _context.BuyingPowers
                .Where(a => a.StockId == stockId && a.Date == date.Date)
                .OrderByDescending(a => a.Date).ThenBy(a => a.EndTime)
                .FirstOrDefaultAsync();

        }
    }
}
