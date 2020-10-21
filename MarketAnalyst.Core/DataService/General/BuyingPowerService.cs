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
        

    }
}
