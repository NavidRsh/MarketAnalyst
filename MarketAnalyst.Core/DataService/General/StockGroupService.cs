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
    public class StockGroupService
    {
        private readonly MarketContext _context;
        public StockGroupService(MarketContext context)
        {
            _context = context; 
        }

        public async Task Add(Data.General.StockGroup StockGroup)
        {
            await _context.StockGroups.AddAsync(StockGroup);            
        }
        public async Task<StockGroup> FindAsync(string groupName)
        {
            return await _context.StockGroups.Where(a => a.Name.Equals(groupName, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefaultAsync(); 
        }
        public bool DeleteAll()
        {
            _context.StockGroups.RemoveRange(_context.StockGroups);
            return true; 
        }

    }
}
