using MarketAnalyst.Core.Data.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketAnalyst.Core.DataService
{
    public class UnitOfWork : IUnitOfWork
    {
        Data.MarketContext _context { get; set; }
        public UnitOfWork(Data.MarketContext context)
        {
            _context = context;
        }
        public General.StockService StockService => new General.StockService(_context);
        public General.StockGroupService StockGroupService => new General.StockGroupService(_context);
        public General.StocksDailyPriceService StocksDailyPriceService => new General.StocksDailyPriceService(_context);
        public General.BuyingPowerService BuyingPowerService => new General.BuyingPowerService(_context);
        public async Task SaveAsync()
        {
            //try
            // {
            await _context.SaveChangesAsync();
            //}
            //catch (Exception  ex)
            //{
            //    throw new Exception(ex);
            //}
        }
        public async Task<int> SaveAsync(Entity<int> item)
        {
            //try
            // {
            await _context.SaveChangesAsync();
            return item.Id;
            //}
            //catch (Exception  ex)
            //{
            //    throw new Exception(ex);
            //}
        }
        public async Task<T> SaveAsync<T>(AddItemResult<T> item)
        {
            await _context.SaveChangesAsync();

            Type t = item.Domain.GetType();
            foreach (var propInfo in t.GetProperties())
            {
                if (propInfo.Name == "Id")
                    propInfo.SetValue(item.Domain, item.Result.Id, null);
            }

            return item.Domain;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}
